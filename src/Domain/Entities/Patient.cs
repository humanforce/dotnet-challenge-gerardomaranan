namespace AppointmentSchedulingApi.Domain.Entities
{
    public class Patient : BaseAuditableEntity
    {
        public required string Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }
    }
}
