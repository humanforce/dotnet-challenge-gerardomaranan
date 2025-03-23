using AppointmentSchedulingApi.Domain.Entities;

namespace AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;

public class TimeslotDto
{
    public int Id { get; set; }

    public DateTimeOffset Start { get; set; }

    public DateTimeOffset End { get; set; }

    public int DoctorId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Timeslot, TimeslotDto>();
        }
    }
}
