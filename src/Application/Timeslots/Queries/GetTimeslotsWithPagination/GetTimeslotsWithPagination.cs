using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Application.Common.Mappings;
using AppointmentSchedulingApi.Application.Common.Models;
using AppointmentSchedulingApi.Domain.Enums;

namespace AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;

public record GetTimeslotsWithPaginationQuery : IRequest<PaginatedList<TimeslotDto>>
{
    public int? DoctorId { get; init; }

    public DateTimeOffset? Start { get; set; }
    
    public DateTimeOffset? End { get; set; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}

public class GetTimeslotsWithPaginationQueryHandler : IRequestHandler<GetTimeslotsWithPaginationQuery, PaginatedList<TimeslotDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    private readonly List<AppointmentStatus> UnavalaibleAppointmentStatuses = 
        new List<AppointmentStatus> { 
            AppointmentStatus.Completed, 
            AppointmentStatus.Scheduled 
        };

    public GetTimeslotsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TimeslotDto>> Handle(GetTimeslotsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Timeslots
            .AsNoTracking()
            .OrderByDescending(t => t.Start)
            .Where(t => !t.Appointments.Any(a => UnavalaibleAppointmentStatuses.Contains(a.Status)))
            .AsQueryable();

        if (request.DoctorId.HasValue)
        {
            query = query
                .Where(t => t.DoctorId == request.DoctorId);
        }

        if (request.Start.HasValue)
        {
            query = query
                .Where(t => t.Start >= request.Start);
        }

        if (request.End.HasValue)
        {
            query = query
                .Where(t => t.Start <= request.End);
        }

        return await query
            .ProjectTo<TimeslotDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
            
    }
}
