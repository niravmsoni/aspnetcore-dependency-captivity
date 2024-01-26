using DependencyInjection.Model;
using System.Globalization;
using System.Text;

namespace DependencyInjection.Target;

public class ProductFormatter : IProductFormatter
{
    private const string HeaderLine = "Id,Name,Currency,Price,Stock";
    public string Format(Product product)
    {
        var stringBuilder = new StringBuilder();

        appendItem(stringBuilder, product.Id.ToString(), true);
        appendItem(stringBuilder, product.Name, false);
        appendItem(stringBuilder, product.Price.IsoCurrency, false);
        appendItem(stringBuilder, product.Price.Amount.ToString(CultureInfo.InvariantCulture), false);
        appendItem(stringBuilder, product.Stock.ToString(), false);

        return stringBuilder.ToString();
    }

    public string GetHeaderLine()
    {
        return HeaderLine;
    }

    private void appendItem(StringBuilder stringBuilder, string item, bool isFirst)
    {
        if (!isFirst)
        {
            stringBuilder.Append(",");
        }

        if (item.Any(c => char.IsWhiteSpace(c)))
        {
            stringBuilder.Append("\"");
            stringBuilder.Append(item);
            stringBuilder.Append("\"");
        }
        else
        {
            stringBuilder.Append(item);
        }
    }
}