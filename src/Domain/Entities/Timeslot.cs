namespace AppointmentSchedulingApi.Domain.Entities;

public class Timeslot : BaseAuditableEntity
{
    public DateTimeOffset Start { get; set; }

    public DateTimeOffset End { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; private set; } = null!;

    public IList<Appointment> Appointments { get; private set; } = new List<Appointment>();
}

