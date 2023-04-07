namespace AISMartHome.Tests;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Smoke()
    {
        Assert.IsTrue((1+1 == 2), "Math has broken");
    }
}