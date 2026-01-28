using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Interfaces
{
public interface IPokemonRepository
{
 ICollection<Pokemon>  GetPokemons();// show the list of products,//return the list not the indiviaual pokemon, hides implementation of link , hashset 

 Pokemon GetPokemon(int id );//
 Pokemon GetPokemon(string name);
 decimal GetPokemonRating(int pokeId);// this is used to pull only rating data 
 bool PokemonExists(int pokeId); // check if the pokemon exists in the database 
}
}