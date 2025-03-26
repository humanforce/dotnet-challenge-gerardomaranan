using AppointmentSchedulingApi.Domain.Events;
using Microsoft.Extensions.Logging;

namespace AppointmentSchedulingApi.Application.Appointments.EventHandlers;

public class AppointmentCreatedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<AppointmentCreatedEventHandler> _logger;

    public AppointmentCreatedEventHandler(ILogger<AppointmentCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AppointmentSchedulingApi Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
