//DATA- ACCESS TO DATABASE CONTEXT
//DTO- DATA TRANFER OBJECTS
//INTERFACES- CONTRACT(IPOKEMONREPOSITORY)
//MODDELS-ENTITY CLASSES  

// bool → Can I proceed?
// decimal → What is the calculated value?
// Entity → Give me data
// void → Just do it
using WEBAPIDEMO.Models;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Data;
using WEBAPIDEMO.Dto;

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
        public decimal GetPokemonRating(int pokeId) // they define a value a method sends back to the caller andhow that value is used in the programs control flow and logic 
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
public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)// creates a new pokemon and correctly link it to an owner and category
        { // if you have many to many relationsship you need to fetch the relationship before hand
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();// fetching the existent owner 
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();// fetching the exisiting category 
            // when ever there is a relationship using joins then we need to efine it like this 
            var PokemonOwner = new PokemonOwner()// this creates a pokemon owner relationship .this creates a join object 
            {
                Owner = pokemonOwnerEntity,
                Pokemon= pokemon,
            };

                _context.Add(PokemonOwner);// add realtionship to the ef core , tracks the relationship and save it to the datbase 
                var PokemonCategory = new PokemonCategory()
                {
                    Category = category,
                    Pokemon= pokemon,
                };
                _context.Add(PokemonCategory);
                _context.Add(pokemon);
                return save();
        }
        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true :false;
        }

        

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
           _context.Remove(pokemon);
           return save();
        }
    }
}