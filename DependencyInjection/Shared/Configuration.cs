namespace DependencyInjection.Shared;
public class Configuration
{
    // We will deal with passing in configuration in a better way in a future module
    // For now hardcoding the values is enough to practice with the concepts from this module

    public string SourceCsvPath => @"C:\Data\Pet Projects\Pluralsight-HandsOn\Dependency-Injection\HenryBeen\Implementation\Code\DependencyInjection\product-input.csv";
    public string TargetCsvPath => @"C:\Data\Pet Projects\Pluralsight-HandsOn\Dependency-Injection\HenryBeen\Implementation\Code\DependencyInjection\product-output.csv";
}