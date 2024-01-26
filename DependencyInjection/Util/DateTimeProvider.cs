namespace DependencyInjection.Util
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _currentDateTime;

        public DateTimeProvider()
        {
            _currentDateTime = DateTime.UtcNow;
        }

        public DateTime GetUtcDateTime()
        {
            return _currentDateTime;
        }
    }
}
