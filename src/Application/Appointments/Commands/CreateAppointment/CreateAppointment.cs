using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Domain.Entities;
using AppointmentSchedulingApi.Domain.Events;

namespace AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand : IRequest<int>
{
    public int PatientId { get; set; }

    public int TimeslotId { get; set; }
}

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Appointment
        {
            PatientId = request.PatientId,
            TimeslotId = request.TimeslotId,
        };

        entity.AddDomainEvent(new AppointmentCreatedEvent(entity));

        _context.Appointments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
