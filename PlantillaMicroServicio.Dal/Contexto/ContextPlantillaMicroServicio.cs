using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Dal.Contexto
{
    public class ContextPlantillaMicroServicio : DbContext
    {
        public ContextPlantillaMicroServicio(DbContextOptions<ContextPlantillaMicroServicio> options) : base(options) { }
        
    }
}
