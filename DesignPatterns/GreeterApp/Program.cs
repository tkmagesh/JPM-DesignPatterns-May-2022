using GreeterApp;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Enter you name :");
var userName = Console.ReadLine();

var currentHour = DateTime.Now.Hour;

var greeter = new Greeter(new TimeService());
var message = greeter.Greet(userName);
Console.WriteLine(message);


