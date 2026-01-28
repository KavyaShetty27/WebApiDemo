// DATA- ACCESS TO DATABASE CONTEXT
//DTO- DATA TRANFER OBJECTS
//INTERFACES- CONTRACT(IPOKEMONREPOSITORY)
//MODDELS-ENTITY CLASSES 
using WEBAPIDEMO.Models;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Data;

namespace WEBAPIDEMO.Repository
{
    public class PokemonRepository : IPokemonRepository// THIS IS A CONSTRUCOR INJECTION  
{
  private readonly DataContext _context;//_CONTEXT- DATABASE CONNECTION, READONLY- CANNOT BE REASSIGNED ACCIDENTALLLY 

  public  PokemonRepository(DataContext context)
{// ASP.NET core injects datacontext 
    _context= context;
}

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }
// we are calculating the averag erating of a pokemon based n its review
        public decimal GetPokemonRating(int pokeId)
        {// give me all the reviews belongs to pokemon
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
// prevent division by 0 
            if (review.Count() <= 0)
                return 0;
// sum(r => r.Rating) → total of all ratings
//review.Count() → number of reviews

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemon.OrderBy(p => p.Id).ToList();
    }

        public bool PokemonExists(int pokeId)
        {
             return _context.Pokemon.Any(p => p.Id == pokeId);
        }
    }
}