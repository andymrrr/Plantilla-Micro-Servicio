﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Dal.Nucleo.Interfaces;
using PlantillaMicroServicio.Dal.Seguridad;
using PlantillaMicroServicio.Modelos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Dal.Nucleo.Repositorios
{
    public class Autenticacion : IAutenticacion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConfiguracionJWT _ConfiguracionJwt;

        public Autenticacion(IHttpContextAccessor httpContextAccessor, IOptions<ConfiguracionJWT> configuracionJwt)
        {
            _httpContextAccessor = httpContextAccessor;
            _ConfiguracionJwt = configuracionJwt.Value;
        }

        public string ObtenerUsuarioSesion()
        {
            var nombreUsuario = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return nombreUsuario ?? string.Empty;
        }

        public string? CrearToken(Usuario usuario, IList<string>? roles)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }

            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, usuario.NombreUsuario!),
                    new Claim("IdUsuario", usuario.UsuarioID.ToString()),
                    new Claim("Correo", usuario.Email!)
                };


                if (roles?.Any() == true)
                {
                    claims.AddRange(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));
                }

                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_ConfiguracionJwt.Llave!));
                var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha512Signature);

                var descripcionToken = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(_ConfiguracionJwt.TiempoExpira),
                    SigningCredentials = credenciales
                };

                var tokenManipulador = new JwtSecurityTokenHandler();
                var token = tokenManipulador.CreateToken(descripcionToken);
                return tokenManipulador.WriteToken(token);
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Error de seguridad al crear el token: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando el token: {ex.Message}");
                return null;
            }
        }

    }
}
