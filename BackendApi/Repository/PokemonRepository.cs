// DATA- ACCESS TO DATABASE CONTEXT
//DTO- DATA TRANFER OBJECTS
//INTERFACES- CONTRACT(IPOKEMONREPOSITORY)
//MODDELS-ENTITY CLASSES 
using WEBAPIDEMO.Models;
using WEBAPIDEMO.Interfaces;
using PokemonReviewApp.Data;

namespace WEBAPIDEMO.Repository
{
    public class PokemonRepository : IPokemonRepository// THIS IS A CONSTRUCOR INJECTION  
{
  private readonly DataContext _context;//_CONTEXT- DATABASE CONNECTION, READONLY- CANNOT BE REASSIGNED ACCIDENTALLLY 

  public  PokemonRepository(DataContext context)
{// ASP.NET core injects datacontext 
    _context= context;
}
public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemon.OrderBy(p => p.Id).ToList();
    }
}
}