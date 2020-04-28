using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SedekahKita.Web.Helpers
{
    public class DateHelper
    {
        public static DateTime GetLocalTimeNow()
        {
            var localTimeZoneId = "SE Asia Standard Time";
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);
        }
    }
}
