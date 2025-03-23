namespace AppointmentSchedulingApi.Domain.Events
{
    public class AppointmentCreatedEvent : BaseEvent
    {
        public AppointmentCreatedEvent(Appointment item)
        {
            Item = item;
        }

        public Appointment Item { get; }
    }
}
