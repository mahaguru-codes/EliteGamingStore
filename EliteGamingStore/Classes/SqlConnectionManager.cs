using System;
using System.Data;
using System.Data.SqlClient;

namespace EliteGamingStore.Classes
{
    public class SqlConnectionManager
    {
        private SqlConnection connection = null;
        public static string strConnectionString = $@"Data Source={GlobalVariables.HOST}; Initial Catalog={GlobalVariables.DATABASE}; User ID={GlobalVariables.DB_USER}; Password={GlobalVariables.DB_PASSWORD};";
        public SqlConnectionManager()
        {
            ConnectDB();
        }

        public SqlConnection GetDatabaseConnection()
        {
            connection = new SqlConnection(strConnectionString);
            return connection;
        }

        private bool ConnectDB()
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    GetDatabaseConnection().Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                }
            }
            catch(Exception err)
            {

            }

            return false;
        }

        public DataTable ExecuteSelectorQuery(SqlCommand command)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (ConnectDB())
                {
                    command.Connection = connection;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch(Exception err)
            {

            }
            finally
            {
                connection.Dispose();
            }

            return dataTable;
        }

        public int ExecuteDMLQuery(SqlCommand command)
        {
            int count = 0;
            try
            {
                if (ConnectDB())
                {
                    command.Connection = connection;
                    count = command.ExecuteNonQuery();
                }
            }
            catch(Exception err)
            {

            }
            finally
            {
                connection.Dispose();
            }

            return count;
        }
    }
}
