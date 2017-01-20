using Server.Database.Commands;
using Server.Database.Schema;

namespace Server.Database
{
    /// <summary>
    ///     Database handler for database calls
    /// </summary>
    internal class DatabaseHandler
    {
        private static readonly object LockObject = new object();

        /// <summary>
        ///     Execute database command inside lock
        /// </summary>
        /// <typeparam name="T">command return type</typeparam>
        /// <param name="command">database command</param>
        /// <returns>return object</returns>
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

        /// <summary>
        ///     Execute database command inside lock
        /// </summary>
        /// <param name="command">database command</param>
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