namespace AppointmentSchedulingApi.Domain.Entities;

public class Appointment : BaseAuditableEntity
{
    public int PatientId { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public int TimeslotId { get; set; }

    public Timeslot Timeslot { get; set; } = null!;
}
