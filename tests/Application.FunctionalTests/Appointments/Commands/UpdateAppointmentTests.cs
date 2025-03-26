namespace AppointmentSchedulingApi.Application.FunctionalTests.Appointments.Commands;

using AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;
using AppointmentSchedulingApi.Application.Appointments.Commands.UpdateAppointment;
using AppointmentSchedulingApi.Application.Common.Exceptions;
using AppointmentSchedulingApi.Domain.Entities;
using AppointmentSchedulingApi.Domain.Enums;
using static Testing;

public class UpdateAppointmentTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateRequiredFields()
    {
        var command = new UpdateAppointmentCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateAppointment()
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

        var appointmentId = await SendAsync(new CreateAppointmentCommand
        {
            PatientId = patient.Id,
            TimeslotId = timeslot.Id,
        });

        var command = new UpdateAppointmentCommand
        {
            Id = appointmentId,
            PatientId = patient.Id,
            TimeslotId = timeslot.Id,
            Status = AppointmentStatus.Completed,
        };

        await SendAsync(command);

        var appointment = await FindAsync<Appointment>(appointmentId);

        appointment.Should().NotBeNull();
        appointment!.PatientId.Should().Be(command.PatientId);
        appointment.TimeslotId.Should().Be(command.TimeslotId);
        appointment.Status.Should().Be(command.Status);
        appointment.LastModifiedBy.Should().NotBeNull();
        appointment.LastModifiedBy.Should().Be(userId);
        appointment.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
