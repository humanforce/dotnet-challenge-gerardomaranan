namespace AppointmentSchedulingApi.Domain.Entities
{
    public class Doctor : BaseAuditableEntity
    {
        public required string Name { get; set; }

        public required string Specialty { get; set; }

        public IList<Timeslot> Timeslots { get; private set; } = new List<Timeslot>();
    }
}
