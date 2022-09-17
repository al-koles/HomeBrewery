namespace HomeBrewery.Application.Common.Exceptions;

public class AccessForbiddenException : Exception
{
    public AccessForbiddenException(string name, object key)
        : base($"Your access to entity '{name}' ({key}) is forbidden.")
    {
    }
}