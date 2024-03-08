using Newtonsoft.Json;

namespace oneapp.Models
{
	public class DateTimeHelperModel
	{
        [JsonIgnore]
        public DateTimeOffset FullDateTime { get; set; }
        public string DateTime
        {
            get
            {
                return Utilities.SystemHelper.ConvertDateTimeToString(FullDateTime);
            }
        }
        public string DateTimeDifference
        {
            get
            {
                return Utilities.SystemHelper.CalculateTimeDifference(FullDateTime, DateTimeOffset.Now);
            }
        }
    }
}

