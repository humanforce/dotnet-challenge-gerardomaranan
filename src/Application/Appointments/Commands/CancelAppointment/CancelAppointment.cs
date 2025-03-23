using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Domain.Enums;
using AppointmentSchedulingApi.Domain.Events;

namespace AppointmentSchedulingApi.Application.Appointments.Commands.CancelAppointment;

public record CancelAppointmentCommand(int Id) : IRequest;


public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
{
    private readonly IApplicationDbContext _context;

    private enum CancellableAppointmentStatus
    {
        Scheduled = AppointmentStatus.Scheduled,
    }

    public CancelAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        Guard.Against.EnumOutOfRange<CancellableAppointmentStatus>((int)entity.Status);

        entity.Status = AppointmentStatus.Cancelled;

        _context.Appointments.Update(entity);

        entity.AddDomainEvent(new AppointmentCancelledEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
