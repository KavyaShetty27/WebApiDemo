using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Interfaces
{
public interface IPokemonRepository
{
 ICollection<Pokemon>  GetPokemons(); //return the list not the indiviaual pokemon, hides implementation of link , hashset 

}
}