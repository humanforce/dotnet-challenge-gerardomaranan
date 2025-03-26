namespace AppointmentSchedulingApi.Application.FunctionalTests.Appointments.Commands;

using AppointmentSchedulingApi.Application.Appointments.Commands.CancelAppointment;
using AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;
using AppointmentSchedulingApi.Domain.Entities;
using AppointmentSchedulingApi.Domain.Enums;
using static Testing;

public class CancelAppointmentTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCancelAppointment()
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

        var command = new CancelAppointmentCommand(appointmentId);

        await SendAsync(command);

        var appointment = await FindAsync<Appointment>(appointmentId);

        appointment.Should().NotBeNull();
        appointment!.Status.Should().Be(AppointmentStatus.Cancelled);
        appointment.LastModifiedBy.Should().NotBeNull();
        appointment.LastModifiedBy.Should().Be(userId);
        appointment.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
