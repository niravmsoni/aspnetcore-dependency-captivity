using System.Text;

namespace DependencyInjection.Shared
{
    /// <summary>
    /// Not thread-safe. Need to use Interlocked class
    /// </summary>
    public class ImportStatistics : IImportStatistics
    {
        private int _productsImportedCount;
        private int _productsOutputtedCount;
        private int _productsTransformedCount;

        public void IncrementImportCount()
        {
            _productsImportedCount++;
        }

        public void IncrementOutputCount()
        {
            _productsOutputtedCount++;
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Read a total of {0} products from source", _productsImportedCount);
            sb.AppendLine();
            sb.AppendFormat("Transformed a total of {0} products", _productsTransformedCount);
            sb.AppendLine();
            sb.AppendFormat("Written a total of {0} products to target", _productsOutputtedCount);

            return sb.ToString();
        }

        public void IncrementTransformationCount()
        {
            _productsTransformedCount++;
        }
    }
}
