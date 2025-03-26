using AppointmentSchedulingApi.Domain.Entities;

namespace AppointmentSchedulingApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Appointment> Appointments { get; }

    DbSet<Doctor> Doctors { get; }

    DbSet<Patient> Patients { get; }

    DbSet<Timeslot> Timeslots { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
