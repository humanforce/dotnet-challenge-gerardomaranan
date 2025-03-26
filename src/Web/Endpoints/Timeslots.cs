
using AppointmentSchedulingApi.Application.Common.Models;
using AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppointmentSchedulingApi.Web.Endpoints;

public class Timeslots : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTimeslots);
    }

    public async Task<Ok<PaginatedList<TimeslotDto>>> GetTimeslots(ISender sender, [AsParameters] GetTimeslotsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
}
