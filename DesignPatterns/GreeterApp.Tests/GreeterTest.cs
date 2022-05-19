using NUnit.Framework;
using GreeterApp;

namespace GreeterApp.Tests;

public class MoriningTimeService : ITimeService
{
    public System.DateTime GetCurrent()
    {
        return new System.DateTime(2012, 5, 19, 10, 0, 0);
    }
}

public class AfternoonTimeService : ITimeService
{
    public System.DateTime GetCurrent()
    {
        return new System.DateTime(2012, 5, 19, 15, 0, 0);
    }
}

public class GreeterTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Greeted_GoodMorning_When_Greeter_Before_12()
    {
        var timeService = new MoriningTimeService();
        var greeter = new Greeter(timeService);
        var userName = "Magesh";
        var message = greeter.Greet(userName);
        var expected_message = "Hi Magesh, Good Morning!";
        Assert.AreEqual(expected_message, message);

    }

    [Test]
    public void Greeted_GoodMorning_When_Greeter_After_12()
    {
        var timeService = new AfternoonTimeService();
        var greeter = new Greeter(timeService);
        var userName = "Magesh";
        var message = greeter.Greet(userName);
        var expected_message = "Hi Magesh, Good Afternoon!";
        Assert.AreEqual(expected_message, message);

    }
}
