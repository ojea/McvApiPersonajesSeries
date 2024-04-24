using ApiPersonajes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajes.Data
{
    public class PersonajesSeriesContext: DbContext
    {
        public PersonajesSeriesContext(DbContextOptions<PersonajesSeriesContext> options): base (options) { }
        
        public DbSet<PersonajesSeries> PersonajesSeries { get; set; }
    }
}
