
namespace HomeBrewery.Application.Common.Exceptions
{
    public class UnsupportedMediaTypeException : Exception
    {
        public UnsupportedMediaTypeException(object file, params string[] supportedTypes)
            : base($"File '{file}' extension " +
                  $"is not in ({string.Join(", ", supportedTypes)}).")
        { }
    }
}
