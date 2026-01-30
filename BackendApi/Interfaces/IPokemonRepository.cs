using WEBAPIDEMO.Dto;
using WEBAPIDEMO.Models;
// GET POKEMONS- bulk retrivel used for list , dashboards . 
// getpokemon(id) - exact lookup used for detials and updates 
//getpokemons(name)- search 
//getpokemonrating- derived data used for buisness logic 
//pokemonexists- validation - used for safety checks 
namespace WEBAPIDEMO.Interfaces
{
public interface IPokemonRepository
{//it basically says give me colection of pokemons and i dont care how you fetch them 
 ICollection<Pokemon>  GetPokemons();// show the list of products,//return the list not the indiviaual pokemon, hides implementation of link , hashset 
//Fetch one Pokémon by primary key

//Common REST endpoint: GET /pokemon/{id}P
Pokemon GetPokemon(int id );
 Pokemon GetPokemon(string name);// we overload instaed of one method bcoz id-> fast and indexed , name-> user friendly and search based 
 decimal GetPokemonRating(int pokeId);// this is used to pull only rating data , since rating is derived data , it belongs to buisness logic not entity state , oulling the full pokemon is inefficeint 
 bool PokemonExists(int pokeId); // check if the pokemon exists in the database , before acting confirms its existence 
 bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
 bool DeletePokemon(Pokemon pokemon);
 bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);

 bool save();

}
}