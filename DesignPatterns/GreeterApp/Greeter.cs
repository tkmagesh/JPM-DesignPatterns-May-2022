using System;
namespace GreeterApp
{
    public class Greeter
    {
        private ITimeService _timeService;
        private IMessageGenerator _messageGenerator;

        public Greeter(ITimeService timeService, IMessageGenerator messageGenerator)
        {
            _timeService = timeService;
            _messageGenerator = messageGenerator;
        }

        public string Greet(string userName)
        {
            
            var currentTime = _timeService.GetCurrent();
            return _messageGenerator.GenerateMessage(userName);
            
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

