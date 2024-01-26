using DependencyInjection.Model;

namespace DependencyInjection.Source;

public interface IPriceParser
{
    Money Parse(string price);
}
