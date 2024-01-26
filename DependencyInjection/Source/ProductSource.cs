using Microsoft.VisualBasic.FileIO;
using DependencyInjection.Model;
using DependencyInjection.Shared;

namespace DependencyInjection.Source;

public class ProductSource : IProductSource
{
    private readonly Configuration _configuration;
    private readonly IPriceParser _priceParser;
    private readonly IImportStatistics _importStatistics;
    private TextFieldParser? _textFieldParser;

    public ProductSource(Configuration configuration, 
        IPriceParser priceParser,
        IImportStatistics importStatistics)
    {
        _configuration = configuration;
        _priceParser = priceParser;
        _importStatistics = importStatistics;
    }

    public void Open()
    {
        _textFieldParser = new TextFieldParser(_configuration.SourceCsvPath);
        _textFieldParser.SetDelimiters(",");
    }

    public bool hasMoreProducts()
    {
        if (_textFieldParser == null)
            throw new InvalidOperationException("Cannot read from a source that is not yet open");

        return !_textFieldParser.EndOfData;
    }

    public Product GetNextProduct()
    {
        if (_textFieldParser == null)
            throw new InvalidOperationException("Cannot read from a source that is not yet open");

        var fields = _textFieldParser.ReadFields() ?? throw new InvalidOperationException("Could not read from source");

        var id = Guid.Parse(fields[0]);
        var name = fields[1];
        var price = _priceParser.Parse(fields[2]);
        var stock = int.Parse(fields[3]);

        var product = new Product(id, name, price, stock);
        _importStatistics.IncrementImportCount();
        return product;
    }

    public void Close()
    {
        if (_textFieldParser == null)
            throw new InvalidOperationException("Cannot close a source that is not yet open");

        _textFieldParser.Close();
        ((IDisposable)_textFieldParser).Dispose();
    }
}