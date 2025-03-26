using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Application.Common.Validators;

namespace AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateAppointmentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.PatientId)
            .NotEmpty()
            .MustAsync(IsPatientFound)
                .WithMessage("'{PropertyName}' could not be found.")
                .WithErrorCode(ErrorCodes.NotFound);

        RuleFor(v => v.TimeslotId)
            .NotEmpty()
            .MustAsync(IsTimeslotAvailable)
                .WithMessage("'{PropertyName}' is not available.")
                .WithErrorCode(ErrorCodes.NotAvailable);
    }

    private async Task<bool> IsPatientFound(int id, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AnyAsync(p => p.Id == id, cancellationToken);
    }

    private async Task<bool> IsDoctorFound(int id, CancellationToken cancellationToken)
    {
        return await _context.Doctors
            .AnyAsync(d => d.Id == id, cancellationToken);
    }

    private async Task<bool> IsTimeslotAvailable(int id, CancellationToken cancellationToken)
    {
        return await _context.Timeslots
            .AnyAsync(t => t.Id == id && !t.Appointments.Any(), cancellationToken);
    }
}
