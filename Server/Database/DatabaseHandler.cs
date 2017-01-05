using Server.Database.Commands;
using Server.Database.Schema;

namespace Server.Database
{
    internal class DatabaseHandler
    {
        private static readonly object LockObject = new object();

        public static T Execute<T>(IDatabaseCommand<T> command)
        {
            lock (LockObject)
            {
                using (var context = new DatabaseDataContext())
                {
                    return command.Execute(context);
                }
            }
        }

        public static void Execute(IDatabaseCommand command)
        {
            lock (LockObject)
            {
                using (var context = new DatabaseDataContext())
                {
                    command.Execute(context);
                }
            }
        }
    }
}