
namespace MyLibrary.File;

public static class DrinkByProfession
{
    public static string GetDrink(string profession)
    {
        return profession.Trim().ToLowerInvariant() switch
        {
            "jabroni" => "Patron Tequila",
            "school counselor" => "Anything with Alcohol",
            "programmer" => "Hipster Craft Beer",
            "bike gang member" => "Moonshine",
            "politician" => "Your tax dollars",
            "rapper" => "Cristal",
            _ => "Beer"
        };
    }
}
