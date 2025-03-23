
using AppointmentSchedulingApi.Application.Appointments.Commands.CancelAppointment;
using AppointmentSchedulingApi.Application.Appointments.Commands.CreateAppointment;
using AppointmentSchedulingApi.Application.Appointments.Commands.UpdateAppointment;
using AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointments;
using AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointmentsSummary;
using AppointmentSchedulingApi.Application.Common.Models;
using AppointmentSchedulingApi.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppointmentSchedulingApi.Web.Endpoints;

public class Appointments : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAppointmentsWithPagination)
            .MapGet(GetAppointmentsSummary, "summary")
            .MapPost(CreateAppointment)
            .MapPost(CancelAppointment, "{id}/cancel")
            .MapPut(UpdateAppointment, "{id}");
    }

    public async Task<Ok<PaginatedList<AppointmentDto>>> GetAppointmentsWithPagination(ISender sender, [AsParameters] GetAppointmentsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public Ok<AppointmentDto> GetAppointment(ISender sender, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Created<int>> CreateAppointment(ISender sender, CreateAppointmentCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"{nameof(Appointments)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> CancelAppointment(ISender sender, int id, CancelAppointmentCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, BadRequest>> UpdateAppointment(ISender sender, int id, UpdateAppointmentCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Ok<List<AppointmentsSummaryDto>>> GetAppointmentsSummary(ISender sender, [AsParameters] GetAppointmentsSummaryQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
}
