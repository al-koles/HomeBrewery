namespace HomeBrewery.Domain.Interfaces;

public interface IEntity<TType>
{
    TType Id { get; set; }
}