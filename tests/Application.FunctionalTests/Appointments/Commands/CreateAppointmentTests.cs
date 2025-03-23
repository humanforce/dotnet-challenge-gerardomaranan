using AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;
using AppointmentSchedulingApi.Application.Common.Exceptions;
using AppointmentSchedulingApi.Domain.Entities;
using AppointmentSchedulingApi.Domain.Enums;

namespace AppointmentSchedulingApi.Application.FunctionalTests.Appointments.Commands;

using static Testing;

public class CreateAppointmentTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateRequiredFields()
    {
        var command = new CreateAppointmentCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateAppointment()
    {
        var userId = await RunAsDefaultUserAsync();

        var doctor = await AddAsync(new Doctor
        {
            Name = "John Doe",
            Specialty = "Tester",
        });

        var patient = await AddAsync(new Patient
        {
            Name = "John Wick",
        });

        var start = DateTimeOffset.UtcNow;
        var end = start.AddHours(2);

        var timeslot = await AddAsync(new Timeslot
        {
            DoctorId = doctor.Id,
            Start = start,
            End = end,
        });

        var command = new CreateAppointmentCommand
        {
            PatientId = patient.Id,
            TimeslotId = timeslot.Id,
        };

        var appointmentId = await SendAsync(command);

        var appointment = await FindAsync<Appointment>(appointmentId);

        appointment.Should().NotBeNull();
        appointment!.PatientId.Should().Be(command.PatientId);
        appointment.TimeslotId.Should().Be(command.TimeslotId);
        appointment.Status.Should().Be(AppointmentStatus.Scheduled);
        appointment.CreatedBy.Should().Be(userId);
        appointment.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        appointment.LastModifiedBy.Should().Be(userId);
        appointment.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
