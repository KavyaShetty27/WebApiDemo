using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]// defines the url pattern 
        [ApiController]// API specific validation , a mandatory for webapis                                //--- these are the attributes , controller setup 
            public class PokemonController: Controller // this is an mvc controller 
                {
                    private readonly IPokemonRepository _pokemonRepository;
                public PokemonController(IPokemonRepository pokemonRepository)
                {
                    _pokemonRepository = pokemonRepository;
                }
                    [HttpGet] //this helps in http get request
                    // reponse metadata 
                    // helps swagger /openapi , documents the response type 
                     [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]

                public IActionResult GetPokemons()// controller calls repository , repository fetcches data from db , controller doesnt know how the data has been fetched 
                {
                    // validates the request model , most useful for post/put 
                    var pokemon = _pokemonRepository.GetPokemons(); 
                    if(!ModelState.IsValid)
                     return BadRequest(ModelState);

                     return Ok(pokemon);
             }

       }
}