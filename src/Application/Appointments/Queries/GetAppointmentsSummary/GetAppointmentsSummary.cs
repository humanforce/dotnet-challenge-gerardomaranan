using AppointmentSchedulingApi.Application.Common.Interfaces;

namespace AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointmentsSummary;

public class GetAppointmentsSummaryQuery : IRequest<List<AppointmentsSummaryDto>>
{
    public DateTimeOffset? Start { get; init; }

    public DateTimeOffset? End { get; init; }
}

public class GetAppointmentsSummaryQueryHandler : IRequestHandler<GetAppointmentsSummaryQuery, List<AppointmentsSummaryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentsSummaryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentsSummaryDto>> Handle(GetAppointmentsSummaryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .GroupBy(a => a.Status)
            .Select(t => new AppointmentsSummaryDto
            {
                Status = t.Key,
                Count = t.Count()
            })
            .ToListAsync(cancellationToken);
    }
}
