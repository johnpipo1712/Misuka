namespace Misuka.Infrastructure.Data
{
    public class CommandExecutor : ICommandExecutor
    {
        public T Execute<T>(ICommand<T> cmd)
        {
            return cmd.Execute();
        }
    }
}