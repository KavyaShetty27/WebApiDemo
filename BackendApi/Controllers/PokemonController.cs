using AutoMapper;
using     Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WEBAPIDEMO.Dto;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Models;

//using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]// defines the url pattern 
        [ApiController]// API specific validation , a mandatory for webapis                                //--- these are the attributes , controller setup 
            public class PokemonController : Controller// this is an mvc controller 
                {// controller inherits from the controller giving access to ok badreq et
                    private readonly IPokemonRepository _pokemonRepository;
                    private readonly IMapper _mapper;// This controller has a private, safe reference to something that knows how to work with Pokémon data.

      

        public PokemonController(IPokemonRepository pokemonRepository,IMapper mapper)// this is a constructor, showing dependecy inkection
                {
                    _pokemonRepository = pokemonRepository;
                    _mapper = mapper;// The injected object (pokemonRepository),Is stored in the private field (_pokemonRepository),So it can be used in all controller methods
                }
            
                    [HttpGet] //this helps in http get request
                    // reponse metadata 
                    // helps swagger /openapi , documents the response type 
                     [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
// iaction is a type of controller method - here the controller calls the pokemon.getrepo
                public IActionResult GetPokemons()// controller calls repository , repository fetcches data from db , controller doesnt know how the data has been fetched 
                {
                    //** validates the request model , most useful for post/put 
          var pokemon =_mapper.Map<List<PokemonDto>>( _pokemonRepository.GetPokemons()); 
                    if(!ModelState.IsValid)
                     return BadRequest(ModelState);

                     return Ok(pokemon);// if we dont write this swagger will not know that we need to get the response type 
             }
             [HttpGet("{pokeId}")]
             [ProducesResponseType(200, Type = typeof(Pokemon))]
             [ProducesResponseType(400)]// why these three lines are important - it shpw sthe patha and it is required field 
            
            public IActionResult GetPokemon(int pokeId)
        {
            if(!_pokemonRepository.PokemonExists(pokeId))
            return NotFound();

            var pokemon =_mapper.Map<PokemonDto>( _pokemonRepository.GetPokemon(pokeId));

            if(!ModelState.IsValid)
            return BadRequest(ModelState);
            return Ok(pokemon);
        }

            [HttpGet("{pokeId}/rating")]
            [ProducesResponseType(200, Type = typeof(decimal))]
            [ProducesResponseType(400)]
public IActionResult GetPokemonRating(int pokeId)
        {
        if(!_pokemonRepository.PokemonExists(pokeId))
        return NotFound();
        var rating = _pokemonRepository.GetPokemonRating(pokeId); 
         if(!ModelState.IsValid)// its an other form of validation
            return BadRequest(ModelState);
            return Ok(rating);   
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate);

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

      
            if (!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

[HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            // var reviewsToDelete = _reviewRepository.GetReviewsOfAPokemon(pokeId);
            var pokemonToDelete = _pokemonRepository.GetPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            // {
            //     ModelState.AddModelError("", "Something went wrong when deleting reviews");
            // }

            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

       }
}
// Request->Routing->Controller->Repository-Database->Repository->AutoMapper->DTO->HTTP Response
