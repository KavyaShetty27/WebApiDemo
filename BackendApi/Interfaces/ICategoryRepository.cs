using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category>GetCategories();
        Category GetCategory(int id);
        //  why icollection - it tells give me all categories, give me this category , does this category exist , what pokemon belongs to this category
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);// here this interface tells which pokemon are in this category
        bool CategoryExists(int id);
    }
}