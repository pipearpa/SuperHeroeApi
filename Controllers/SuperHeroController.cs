 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
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
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("dbHero not found.");

            dbHero.Name = request.Name;
            dbHero.FirsName = request.FirsName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")] 
        // Atributo que especifica que este método maneja solicitudes DELETE y toma un parámetro 'id' en la ruta
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        // Método del controlador que elimina un héroe
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            

            // Si se encuentra el héroe, elimínalo de la lista de héroes
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();  

            // Devuelve una respuesta Ok junto con la lista actualizada de héroes
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


    }
}
