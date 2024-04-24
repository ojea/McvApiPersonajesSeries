using ApiPersonajes.Models;
using ApiPersonajes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesSeriesController : ControllerBase
    {
        private PersonajesSerieRepository repo;

        public PersonajesSeriesController(PersonajesSerieRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonajesSeriesController>>> GetPersonajes()
        {
            var personajes = await repo.GetPersonajesAsync();
            return Ok(personajes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajesSeries>>
      FindPersonaje(int id)
        {
            return await this.repo.FindPersonajeIdAsync(id);
        }

        [HttpGet]
        [Route("[action]/{serie}")]
        public async Task<ActionResult<List<PersonajesSeries>>> PeronajesSeries(string serie)
        {
            return await this.repo.GetPersonajesPorSerieAsync(serie);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Series()
        {
            return await this.repo.GetSerieAsync();
        }


        [HttpPost("InsertarPersonaje")]
        public async Task<ActionResult> PostPersonaje (PersonajesSeries personaje)
        {
            await this.repo.InsertPersonaje(personaje.Nombre, personaje.Imagen, personaje.Serie);
            return Ok();
        }

        [HttpPut("ModificarPersonaje")]
        public async Task<ActionResult> PutPersonaje(PersonajesSeries personaje)
        {
            await this.repo.UpdatePersonaje(personaje.IdPersonaje
                , personaje.Nombre, personaje.Imagen, personaje.Serie);
            return Ok();
        }

        [HttpDelete("DeletePersonaje")]
        public async Task<ActionResult> Delete(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindPersonajeIdAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeletePersonajeAsync(id);
                return Ok();
            }
        }
    }
}
