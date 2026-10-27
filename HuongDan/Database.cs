using Microsoft.Data.SqlClient;
using System.Data;

namespace HuongDan;

internal static class Database
{
    internal static SqlDataReader ExecuteQuery(string query)
    {
        var connection = new SqlConnection(Program.ConnectionString);
        var command = new SqlCommand(query, connection);
        try
        {
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            command.Dispose();
            connection.Dispose();
            throw;
        }
    }

    internal static int ExecuteNonQuery(string statement)
    {
        using var connection = new SqlConnection(Program.ConnectionString);
        using var command = new SqlCommand(statement, connection);
        connection.Open();
        return command.ExecuteNonQuery();
    }

    internal static int ExecuteNonQuery(string statement, SqlConnection connection, SqlTransaction transaction)
    {
        using var command = new SqlCommand(statement, connection, transaction);
        return command.ExecuteNonQuery();
    }
}
