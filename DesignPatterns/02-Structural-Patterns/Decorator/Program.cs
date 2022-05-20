using Decorator;

Console.Title = "Decorator";

// instantiate mail services
var cloudMailService = new CloudMailService();
cloudMailService.SendMail("Hi there.");

var onPremiseMailService = new OnPremiseMailService();
onPremiseMailService.SendMail("Hi there.");

// add behavior
var statisticsDecorator = new StatisticsDecorator(cloudMailService);
statisticsDecorator.SendMail($"Hi there via {nameof(StatisticsDecorator)} wrapper.");

// add behavior
var messageDatabaseDecorator = new MessageDatabaseDecorator(onPremiseMailService);
messageDatabaseDecorator.SendMail(
    $"Hi there via {nameof(MessageDatabaseDecorator)} wrapper, message 1.");
messageDatabaseDecorator.SendMail(
    $"Hi there via {nameof(MessageDatabaseDecorator)} wrapper, message 2.");

foreach (var message in messageDatabaseDecorator.SentMessages)
{
    Console.WriteLine($"Stored message: \"{message}\"");
}


//Functional Decorators
Action<int, int> add = (x, y) => Console.WriteLine($"Add result = {x + y}");
Action<int, int> subtract = (x, y) => Console.WriteLine($"Subtract result = {x + y}");

Func<Action<int, int>, Action<int, int>> decorator = fn => (int x, int y) =>
{
    Console.WriteLine("Invocation started");
    fn(x, y);
    Console.WriteLine("Invocation completed");
};

var logAdd = decorator(add);
var logSub = decorator(subtract);

logAdd(100, 200);
logSub(100, 200);
Console.ReadKey();