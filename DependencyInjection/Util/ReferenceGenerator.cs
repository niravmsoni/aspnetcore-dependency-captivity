namespace DependencyInjection.Util
{
    public class ReferenceGenerator : IReferenceGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IIncrementingCounter _incrementingCounter;

        public ReferenceGenerator(IDateTimeProvider dateTimeProvider,
            IIncrementingCounter incrementingCounter)
        {
            _dateTimeProvider = dateTimeProvider;
            _incrementingCounter = incrementingCounter;
        }

        public string GetReference()
        {
            var counter = _incrementingCounter.GetNext();
            var dateTime = _dateTimeProvider.GetUtcDateTime();

            var reference = $"{dateTime:yyyy-MM-ddTHH:mm:ss.FFF}-{counter:D4}";

            return reference;
        }
    }
}
