using GreeterApp;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Enter you name :");
var userName = Console.ReadLine();

/*
 DI.register<ITimeService, TimeService>()
 DI.register<IMessageGeneratorFactory, MessageGeneratorFactory>()
 DI.register<Greeter, Greeter>()

 var greeter = DI.Get<Greeter>()
 */

var timeService = new TimeService();
var generatorFactory = new MessageGeneratorFactory(timeService);
var messageGenerator = generatorFactory.GetGenerator();
var greeter = new Greeter(messageGenerator);
var message = greeter.Greet(userName);
Console.WriteLine(message);


