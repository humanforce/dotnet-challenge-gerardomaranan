using System.Text.Json.Serialization;
using AppointmentSchedulingApi.Application.Appointments.Commands.CancelAppointment;
using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Domain.Enums;
using AppointmentSchedulingApi.Domain.Events;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace AppointmentSchedulingApi.Application.Appointments.Commands.UpdateAppointment;

public record UpdateAppointmentCommand : IRequest
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int TimeslotId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AppointmentStatus Status { get; set; }
}

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Status = request.Status;
        entity.PatientId = request.PatientId;
        entity.TimeslotId = request.TimeslotId;

        _context.Appointments.Update(entity);

        entity.AddDomainEvent(new AppointmentCancelledEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
