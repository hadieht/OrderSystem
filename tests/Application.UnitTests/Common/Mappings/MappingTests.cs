using API.Mapper;
using AutoMapper;
using NUnit.Framework;


namespace OrderSystem.Application.UnitTests.Common.Mappings;
public class MappingTests
{
    private readonly IConfigurationProvider configuration;
 

    public MappingTests() => configuration = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MappingProfile>();
    });

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        configuration.AssertConfigurationIsValid();
    }

}
