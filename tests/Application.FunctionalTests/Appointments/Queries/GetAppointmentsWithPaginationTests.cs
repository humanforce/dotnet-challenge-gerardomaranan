namespace AppointmentSchedulingApi.Application.FunctionalTests.Appointments.Queries;

using AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;
using AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointments;
using AppointmentSchedulingApi.Application.Common.Exceptions;
using AppointmentSchedulingApi.Domain.Entities;
using static Testing;

public class GetAppointmentsWithPaginationTests
{
    [Test]
    public async Task ShouldValidateRequiredFields()
    {
        var query = new GetAppointmentsWithPaginationQuery();

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnAppointmentsWithPagination()
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

        var query = new GetAppointmentsWithPaginationQuery
        {
            DoctorId = doctor.Id,
            PatientId = patient.Id,
            Start = start.AddHours(-1),
            End = end.AddHours(1),
            PageNumber = 1,
            PageSize = 10,
        };

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(1);
        result.Items.First().Should().NotBeNull();
        result.Items.First().Should().BeOfType<AppointmentDto>();
        result.Items.First().PatientId.Should().Be(query.PatientId);
        result.Items.First().TimeslotId.Should().Be(timeslot.Id);
        result.PageNumber.Should().Be(1);
        result.TotalCount.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
