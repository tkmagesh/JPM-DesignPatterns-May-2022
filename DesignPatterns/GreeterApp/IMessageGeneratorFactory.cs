namespace GreeterApp
{
    public interface IMessageGeneratorFactory
    {
        IMessageGenerator GetGenerator();
    }
}