namespace RenaStore.Shared.Commands
{
    public interface ICommandHandler<T>  where T : ICommand
    {
        ICommandResult Headle(T command);
    }
}