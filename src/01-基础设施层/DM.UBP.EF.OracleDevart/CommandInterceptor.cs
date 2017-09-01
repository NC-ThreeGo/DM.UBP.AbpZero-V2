using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace DM.UBP.EF.OracleDevart
{
    internal class CommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            for (int i = command.Parameters.Count - 1; i >= 0; i--)
            {
                var param = command.Parameters[i];
                if (!command.CommandText.Contains(":" + param.ParameterName))
                    command.Parameters.Remove(param);
            }
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}
