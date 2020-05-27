using RecebaFacil.Infrastructure.DataAccess.Core;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public abstract class RepositoryBase
    {
        private readonly ISqlAccess _sqlAcess;

        protected RepositoryBase(ISqlAccess databaseHandler)
        {
            _sqlAcess = databaseHandler;
        }

        public DataSet ExecuteCommand(string commandText, SqlParameter[] parameters)
        {
            using (SqlConnection connection = _sqlAcess.CreateConnection())
            {
                connection.Open();
                using SqlCommand command = _sqlAcess.CreateCommand(commandText, CommandType.StoredProcedure, connection);
                AddParametersToCommand(parameters, command);

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = _sqlAcess.CreateAdapter(command);
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        public object ExecuteScalar(string commandText, SqlParameter[] parameters)
        {
            object result = null;
            using (SqlConnection connection = _sqlAcess.CreateConnection())
            {
                connection.Open();
                using SqlCommand command = _sqlAcess.CreateCommand(commandText, CommandType.StoredProcedure, connection);
                AddParametersToCommand(parameters, command);
                try
                {
                    result = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public int ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        {
            int rowcount = 0;
            using (SqlConnection connection = _sqlAcess.CreateConnection())
            {
                connection.Open();
                using SqlCommand command = _sqlAcess.CreateCommand(commandText, CommandType.StoredProcedure, connection);
                AddParametersToCommand(parameters, command);

                try
                {
                    rowcount = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return rowcount;
        }

        private static void AddParametersToCommand(SqlParameter[] parameters, SqlCommand command)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                    command.Parameters.Add(parameter);
            }
        }
    }
}
