using Server.Database.Schema;

namespace Server.Database.Commands
{
    public interface IDatabaseCommand
    {
        void Execute(DatabaseDataContext context);
    }

    public interface IDatabaseCommand<out T>
    {
        T Execute(DatabaseDataContext context);
    }
}