namespace Application.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public void TestSetUp()
    {
        //await Testing.ResetState();
    }
}
