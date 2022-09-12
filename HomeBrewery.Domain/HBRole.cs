using Microsoft.AspNetCore.Identity;

namespace HomeBrewery.Domain;

public class HBRole : IdentityRole<int>
{
    public HBRole()
    {
        UserRoles = new List<HBUserRole>();
    }
    
    public virtual List<HBUserRole> UserRoles { get; set; }
}