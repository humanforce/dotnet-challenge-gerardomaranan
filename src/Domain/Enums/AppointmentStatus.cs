using System.Text.Json.Serialization;

namespace AppointmentSchedulingApi.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppointmentStatus
    {
        Cancelled = -1,
        Scheduled,
        Completed,
    }
}
