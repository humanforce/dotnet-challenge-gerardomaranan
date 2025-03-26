using AppointmentSchedulingApi.Application.Common.Exceptions;
using AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;
using AppointmentSchedulingApi.Domain.Entities;

namespace AppointmentSchedulingApi.Application.FunctionalTests.Timeslots.Queries;

using static Testing;

public class GetTimeslotsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldValidateRequiredFields()
    {
        var query = new GetTimeslotsWithPaginationQuery();

        await FluentActions.Invoking(() =>
            SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldReturnTimeslotsWithPagination()
    {
        var userId = await RunAsDefaultUserAsync();

        var doctor = await AddAsync(new Doctor
        {
            Name = "John Doe",
            Specialty = "Tester",
        });

        var start = DateTimeOffset.UtcNow;
        var end = start.AddHours(2);

        var timeslot = await AddAsync(new Timeslot
        {
            DoctorId = doctor.Id,
            Start = start,
            End = end,
        });

        var query = new GetTimeslotsWithPaginationQuery
        {
            DoctorId = doctor.Id,
            Start = start.AddHours(-1),
            End = end.AddHours(1),
            PageNumber = 1,
            PageSize = 10,
        };

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(1);
        result.Items.First().Should().NotBeNull();
        result.Items.First().Should().BeOfType<TimeslotDto>();
        result.Items.First().DoctorId.Should().Be(query.DoctorId);
        result.Items.First().Start.Should().BeCloseTo(start, TimeSpan.FromMilliseconds(10000));
        result.Items.First().End.Should().BeCloseTo(end, TimeSpan.FromMilliseconds(10000));
        result.PageNumber.Should().Be(1);
        result.TotalCount.Should().Be(1);
        result.TotalPages.Should().Be(1);
    }
}
