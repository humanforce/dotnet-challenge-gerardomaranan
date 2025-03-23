using AppointmentSchedulingApi.Domain.Enums;

namespace AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointmentsSummary
{
    public class AppointmentsSummaryDto
    {
        public AppointmentStatus Status { get; init; }
        
        public int Count { get; init; }
    }
}
