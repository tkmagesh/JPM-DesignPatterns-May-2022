using System;
namespace GreeterApp
{
    public class Greeter
    {
        private readonly IMessageGenerator messageGenerator;

        public Greeter(IMessageGenerator messageGenerator)
        {
            this.messageGenerator = messageGenerator;
        }

        public string Greet(string userName)
        {
            return messageGenerator.GenerateMessage(userName);   
        }
    }

    public class MorningMessageGenerator : IMessageGenerator
    {
        public string GenerateMessage(string userName)
        {
            return $"Hi {userName}, Good Morning!";
        }
    }

    public class AfternoonMessageGenerator : IMessageGenerator
    {
        public string GenerateMessage(string userName)
        {
            return $"Hi {userName}, Good Afternoon!";
        }
    }

    public class EveningMessageGenerator : IMessageGenerator
    {
        public string GenerateMessage(string userName)
        {
            return $"Hi {userName}, Good Evening!";
        }
    }

    public class MessageGeneratorFactory : IMessageGeneratorFactory
    {
        private readonly ITimeService timeService;

        public MessageGeneratorFactory(ITimeService timeService)
        {
            this.timeService = timeService;
        }
        public IMessageGenerator GetGenerator()
        {
            var currentHour = timeService.GetCurrent().Hour;
            if (currentHour < 12)
            {
                return new MorningMessageGenerator();
            }
            if (currentHour >= 12 && currentHour < 17)
            {
                return new AfternoonMessageGenerator();
            }
            return new EveningMessageGenerator();
        }
    }

    public class TimeService : ITimeService
    {
        public TimeService()
        {

        }

        public DateTime GetCurrent()
        {
            return DateTime.Now;
        }
    }
}

