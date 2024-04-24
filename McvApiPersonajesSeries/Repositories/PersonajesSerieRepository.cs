using ApiPersonajes.Data;
using ApiPersonajes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajes.Repositories
{
    public class PersonajesSerieRepository
    {
        private PersonajesSeriesContext context;

        public PersonajesSerieRepository (PersonajesSeriesContext context)
        {
            this.context = context;
        }

        //GET ALL PERSONAJES

        public async Task<List<PersonajesSeries>> GetPersonajesAsync()
        {
            return await context.PersonajesSeries.ToListAsync();
        }
        public async Task<List<PersonajesSeries>> GetPersonajesPorSerieAsync(string serie)
        {
            return await this.context.PersonajesSeries.Where(p => p.Serie == serie).ToListAsync();
            
        }
        public async Task<List<string>> GetSerieAsync()
        {
            var consulta = (from datos in this.context.PersonajesSeries
                            select datos.Serie).Distinct();
            return await consulta.ToListAsync();
        }

        //GET PERSONAJES POR ID
        public async Task<PersonajesSeries> FindPersonajeIdAsync(int id)
        {
            var personajesSeries = await context.PersonajesSeries.FirstOrDefaultAsync(p => p.IdPersonaje == id);
            return personajesSeries;
        }

        //INSERTAR PERSONAJE
        public async Task InsertPersonaje(string nombre, string imagen, string serie)
        {
            PersonajesSeries personaje = new PersonajesSeries();
            personaje.IdPersonaje = await this.GetMaxIdPersonajeAsync();
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;

            this.context.PersonajesSeries.Add(personaje);
            await this.context.SaveChangesAsync();
        }

        //UPDATE PERSONAJE
        public async Task UpdatePersonaje(int id, string nombre, string imagen, string serie)
        {
            PersonajesSeries personaje = await this.FindPersonajeIdAsync(id);
            personaje.IdPersonaje = await this.GetMaxIdPersonajeAsync();
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;

            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int id)
        {
            PersonajesSeries personaje = await this.FindPersonajeIdAsync(id);
            this.context.PersonajesSeries.Remove(personaje);
            await this.context.SaveChangesAsync();
        }

        //GET MAX ID
        private async Task<int> GetMaxIdPersonajeAsync()
        {
            if (this.context.PersonajesSeries.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.PersonajesSeries.MaxAsync(z => z.IdPersonaje) + 1;
            }
        }
    }
}
