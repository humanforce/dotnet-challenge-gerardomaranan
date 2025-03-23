using System.Reflection;
using System.Runtime.CompilerServices;
using AppointmentSchedulingApi.Application.Appointments.Queries.GetAppointments;
using AppointmentSchedulingApi.Application.Common.Interfaces;
using AppointmentSchedulingApi.Application.Timeslots.Queries.GetTimeslotsWithPagination;
using AppointmentSchedulingApi.Domain.Entities;
using AutoMapper;
using NUnit.Framework;

namespace AppointmentSchedulingApi.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Appointment), typeof(AppointmentDto))]
    [TestCase(typeof(Timeslot), typeof(TimeslotDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
