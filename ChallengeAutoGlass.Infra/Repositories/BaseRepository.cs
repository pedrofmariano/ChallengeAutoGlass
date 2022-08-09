using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Infra.Repositories
{
    public abstract class BaseRepository
    {
        private string ConnectionString => Environment.GetEnvironmentVariable("DefaultConnection");

        protected async Task<dynamic> WrapConnection(Func<IDbConnection, Task<dynamic>> callback, CancellationToken ctx)
        {
            ctx.ThrowIfCancellationRequested();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return await callback(connection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ConnectionString, ex);
            }
        }
    }
}
