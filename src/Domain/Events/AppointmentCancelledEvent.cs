namespace AppointmentSchedulingApi.Domain.Events
{
    public class AppointmentCancelledEvent : BaseEvent
    {
        public AppointmentCancelledEvent(Appointment item)
        {
            Item = item;
        }

        public Appointment Item { get; }
    }
}
