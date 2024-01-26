using DependencyInjection.Shared;
using DependencyInjection.Source;
using DependencyInjection.Target;
using DependencyInjection.Transformation;

namespace DependencyInjection;
public class ProductImporter
{
    private readonly IProductSource _productSource;
    private readonly IProductTransformer _productTransformer;
    private readonly IProductTarget _productTarget;
    private readonly IImportStatistics _importStatistics;

    public ProductImporter(IProductSource productSource, 
        IProductTransformer productTransformer,
        IProductTarget productTarget,
        IImportStatistics importStatistics)
    {
        _productSource = productSource;
        _productTransformer = productTransformer;
        _productTarget = productTarget;
        _importStatistics = importStatistics;
    }

    public void Run()
    {
        _productSource.Open();
        _productTarget.Open();

        while (_productSource.hasMoreProducts())
        {
            var product = _productSource.GetNextProduct();

            //Calling this method to apply transformations per product
            var transformedProduct = _productTransformer.ApplyTransformations(product);

            _productTarget.AddProduct(transformedProduct);
        }

        _productSource.Close();
        _productTarget.Close();

        Console.WriteLine("Importing complete");
        Console.WriteLine(_importStatistics.GetStatistics());
    }
}