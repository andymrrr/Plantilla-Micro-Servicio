using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Models;
using PlantillaMicroServicio.Models.Configuracion;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlantillaMicroServicio.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConfiguracionJWT _configuracionJwt;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtService(IHttpContextAccessor httpContextAccessor, IOptions<ConfiguracionJWT> configuracionJwt)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuracionJwt = configuracionJwt.Value;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string? CrearToken(Usuario usuario, IList<string>? roles)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(_configuracionJwt.Llave))
            {
                throw new InvalidOperationException("La llave JWT no está configurada.");
            }

            try
            {
                var claims = CrearClaims(usuario, roles);
                var tokenDescriptor = CrearTokenDescriptor(claims);
                var token = _tokenHandler.CreateToken(tokenDescriptor);
                return _tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando el token: {ex.Message}");
                return null;
            }
        }

        public string? RenovarToken(string tokenActual)
        {
            try
            {
                var claimsPrincipal = ObtenerClaimsDelToken(tokenActual);
                if (claimsPrincipal == null) return null;

                var usuarioId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var email = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
                var nombreUsuario = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;
                var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

                if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(nombreUsuario))
                {
                    return null;
                }

                var usuario = new Usuario
                {
                    UsuarioID = int.Parse(usuarioId),
                    Email = email,
                    NombreUsuario = nombreUsuario
                };

                return CrearToken(usuario, roles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renovando el token: {ex.Message}");
                return null;
            }
        }

        public bool ValidarToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuracionJwt.Llave!);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = _configuracionJwt.ValidarIssuer,
                    ValidIssuer = _configuracionJwt.Asunto,
                    ValidateAudience = _configuracionJwt.ValidarAudience,
                    ValidAudience = _configuracionJwt.Audiencia,
                    ClockSkew = TimeSpan.FromMinutes(_configuracionJwt.ClockSkewMinutos),
                    RequireExpirationTime = _configuracionJwt.RequerirExpirationTime,
                    ValidateLifetime = _configuracionJwt.ValidarLifetime
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ClaimsPrincipal? ObtenerClaimsDelToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuracionJwt.Llave!);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = _configuracionJwt.ValidarIssuer,
                    ValidIssuer = _configuracionJwt.Asunto,
                    ValidateAudience = _configuracionJwt.ValidarAudience,
                    ValidAudience = _configuracionJwt.Audiencia,
                    ClockSkew = TimeSpan.FromMinutes(_configuracionJwt.ClockSkewMinutos),
                    RequireExpirationTime = _configuracionJwt.RequerirExpirationTime,
                    ValidateLifetime = _configuracionJwt.ValidarLifetime
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string ObtenerUsuarioSesion()
        {
            var nombreUsuario = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return nombreUsuario ?? string.Empty;
        }

        public DateTime? ObtenerFechaExpiracion(string token)
        {
            try
            {
                var jwtToken = _tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo;
            }
            catch
            {
                return null;
            }
        }

        private List<Claim> CrearClaims(Usuario usuario, IList<string>? roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.NombreUsuario!),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            if (roles?.Any() == true)
            {
                claims.AddRange(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));
            }

            return claims;
        }

        private SecurityTokenDescriptor CrearTokenDescriptor(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracionJwt.Llave!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_configuracionJwt.TiempoExpira),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Issuer = _configuracionJwt.Asunto,
                Audience = _configuracionJwt.Audiencia,
                SigningCredentials = credentials
            };
        }
    }
}