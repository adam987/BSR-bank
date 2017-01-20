using Server.Database.Schema;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Database command interface
    /// </summary>
    public interface IDatabaseCommand
    {
        void Execute(DatabaseDataContext context);
    }

    /// <summary>
    ///     Generic database command interface
    /// </summary>
    /// <typeparam name="T">command return type</typeparam>
    public interface IDatabaseCommand<out T>
    {
        T Execute(DatabaseDataContext context);
    }
}