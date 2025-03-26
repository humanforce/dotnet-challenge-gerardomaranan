using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Application.Common.Mappings;
using AppointmentSchedulingApi.Application.Common.Models;

namespace AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointments;

public record GetAppointmentsWithPaginationQuery : IRequest<PaginatedList<AppointmentDto>>
{
    public int? PatientId { get; init; }

    public int? DoctorId { get; init; }

    public DateTimeOffset? Start { get; init; }

    public DateTimeOffset? End { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsWithPaginationQuery, PaginatedList<AppointmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAppointmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointmentsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
            .AsNoTracking()
            .AsQueryable();

        if (request.PatientId.HasValue)
        {
            query = query
                .Where(a => a.PatientId == request.PatientId);
        }

        if (request.DoctorId.HasValue)
        {
            query = query
                .Where(a => a.Timeslot.DoctorId == request.DoctorId);
        }

        if (request.Start.HasValue)
        {
            query = query
                .Where(a => a.Timeslot.Start >= request.Start);
        }

        if (request.End.HasValue)
        {
            query = query
                .Where(a => a.Timeslot.End <= request.End);
        }

        return await query
            .OrderByDescending(a => a.Created)
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
