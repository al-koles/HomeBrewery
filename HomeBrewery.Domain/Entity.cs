using HomeBrewery.Domain.Interfaces;

namespace HomeBrewery.Domain;

public class Entity : IEntity<int>
{
    public int Id { get; set; }
}