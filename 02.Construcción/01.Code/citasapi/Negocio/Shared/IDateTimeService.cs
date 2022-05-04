using System; 
using TimeZoneConverter;

namespace Negocio.Shared
{
    public interface IDateTimeService
    {
        DateTime Now();
        DateTime Today();
        DateTime ToLocalTime(DateTime date);
        DateTime ToUtc(DateTime date);
    }

    public class DateTimeService : IDateTimeService
    {
        public static IDateTimeService Current { get; set; } = new DateTimeService();

        public DateTime Now()
        {
            try
            {
                var cstTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.TimeZoneInfo);
                return cstTime;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }

        }

        public DateTime Today()
        {
            try
            {
                var cstTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.TimeZoneInfo).Date;
                return cstTime;
            }
            catch (Exception)
            {
                return DateTime.Today;
            }
        }

        public DateTime ToLocalTime(DateTime date)
        {
            var cstTime = TimeZoneInfo.ConvertTimeFromUtc(date, this.TimeZoneInfo);
            return cstTime;
        }

        public DateTime ToUtc(DateTime date)
        {
            var cstTime = TimeZoneInfo.ConvertTimeToUtc(date, this.TimeZoneInfo);
            return cstTime;
        }

        private TimeZoneInfo _timeZoneInfo;
        private TimeZoneInfo TimeZoneInfo
        {
            get
            {
                if (_timeZoneInfo != null) return _timeZoneInfo;

                try
                {
                    _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
                }
                catch
                {
                    var tz = TZConvert.WindowsToIana("Central Standard Time (Mexico)");
                    try
                    {
                        _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(tz);
                    }
                    catch
                    {
                        _timeZoneInfo = TimeZoneInfo.Local;
                    }
                }

                return _timeZoneInfo;
            }
        }

    }
}
