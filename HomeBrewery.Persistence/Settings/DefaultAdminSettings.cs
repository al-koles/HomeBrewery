namespace HomeBrewery.Persistence.Settings;

public class DefaultAdminSettings
{
    public const string SectionName = "Admin";

    public string Email { get; set; }
    public string Password { get; set; }
}