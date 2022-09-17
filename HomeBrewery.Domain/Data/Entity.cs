using HomeBrewery.Domain.Interfaces;

namespace HomeBrewery.Domain.Data;

public class Entity : IEntity<int>
{
    public int Id { get; set; }
}