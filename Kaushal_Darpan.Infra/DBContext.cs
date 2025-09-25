using Kaushal_Darpan.Core.Helper;
using Microsoft.Data.SqlClient;

namespace Kaushal_Darpan.Infra
{
    public class DBContext : IAsyncDisposable
    {
        private SqlConnection? _connection;
        private SqlTransaction? _transaction;
        private bool disposedValue;

        public async Task<SqlCommand> CreateCommandAsync(bool withTransactionControl = false)
        {
            await CreateObjectAsync(withTransactionControl); // ensure connection
            var command = _connection!.CreateCommand(); // create command
            if (withTransactionControl)
            {
                command.Transaction = _transaction;
            }
            //command.CommandTimeout = 2 * 60; // 2 minutes
            return command;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    _transaction = null;
                }
            }
            catch
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    _transaction = null;
                }
                //throw; // rethrow so caller knows commit failed
            }
        }

        private async Task CreateObjectAsync(bool withTransactionControl)
        {
            if (_connection == null)
            {
                var connectionString = ConfigurationHelper.ConnectionString;
                _connection = new SqlConnection(connectionString);
                await _connection.OpenAsync();

                if (withTransactionControl)
                {
                    _transaction = (SqlTransaction)await _connection.BeginTransactionAsync();
                }
            }
        }

        protected virtual async Task DisposeAsyncCore()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }

                if (_connection != null)
                {
                    await _connection.CloseAsync();
                    await _connection.DisposeAsync();
                    _connection = null;
                }
            }
            catch
            {
                _transaction = null;
                _connection = null;
            }
        }

        // IAsyncDisposable
        public async ValueTask DisposeAsync()
        {
            //if (!disposedValue)
            {
                await DisposeAsyncCore();
                disposedValue = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
