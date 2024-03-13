 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
               
                new SuperHero {
                    Id = 2,
                    Name = "Ironman",
                    FirsName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"
                }

            };
        private readonly DataContext _context;

        // Constructor del controlador SuperHeroController
        public SuperHeroController(DataContext context)
        {
            // Almacenar la instancia de DataContext proporcionada por el sistema de inyección de dependencias
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = heroes.Find(h => h.Id == request.Id);
            if (hero == null)
                return BadRequest("Hero not found.");

            hero.Name = request.Name;
            hero.FirsName = request.FirsName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            return Ok(heroes);
        }
        [HttpDelete("{id}")] 
        // Atributo que especifica que este método maneja solicitudes DELETE y toma un parámetro 'id' en la ruta
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        // Método del controlador que elimina un héroe
        {
            // Busca el héroe con el ID proporcionado en la lista de héroes
            var hero = heroes.Find(h => h.Id == id);

            // Si no se encuentra el héroe, devuelve una respuesta BadRequest con un mensaje
            if (hero == null)
                return BadRequest("Hero not found.");

            // Si se encuentra el héroe, elimínalo de la lista de héroes
            heroes.Remove(hero);

            // Devuelve una respuesta Ok junto con la lista actualizada de héroes
            return Ok(heroes);
        }


    }
}
