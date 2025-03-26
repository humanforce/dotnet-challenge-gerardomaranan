using AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;
using AppointmentSchedulingApi.Domain.Entities;
using AppointmentSchedulingApi.Domain.Enums;

namespace AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointments;

public class AppointmentDto
{
    public int Id { get; init; }

    public int PatientId { get; init; }

    public AppointmentStatus Status { get; set; }

    public int TimeslotId { get; set; }

    public TimeslotDto Timeslot { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
