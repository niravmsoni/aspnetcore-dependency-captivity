namespace DependencyInjection.Util
{
    public class ReferenceGenerator : IReferenceGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private int counter = -1;

        public ReferenceGenerator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public string GetReference()
        {
            counter++;
            var dateTime = _dateTimeProvider.GetUtcDateTime();

            var reference = $"{dateTime:yyyy-MM-ddTHH:mm:ss.FFF}-{counter:D4}";

            return reference;
        }
    }
}
