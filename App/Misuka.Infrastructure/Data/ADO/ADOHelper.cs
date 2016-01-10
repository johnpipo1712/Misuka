using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Misuka.Infrastructure.Configuration;

namespace Misuka.Infrastructure.Data.ADO
{
  public class ADOHelper : IADOHelper
  {
    public DbCommand CreateCommand(CommandType commandType, string commandText)
    {
      SqlCommand command = new SqlCommand(commandText) { CommandType = commandType };

      return command;
    }

    public int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      int result = 0;
      var connectionName = SystemConfiguration.Instance.GeneralSettings.ConnectionString;
      using (SqlConnection conn = new SqlConnection(connectionName))
      {
        conn.Open();
        using (var command = new SqlCommand(commandText, conn))
        {
          command.CommandType = commandType;
          //   command.CommandText = commandText;
          if (parameters != null)
          {
            foreach (var parameter in parameters)
            {
              command.Parameters.Add(parameter);
            }
          }
          result = command.ExecuteNonQuery();
        }
      }
      return result;
    }

    public object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      object result = null;
      var connectionName = SystemConfiguration.Instance.GeneralSettings.ConnectionString;
      using (var conn = new SqlConnection(connectionName))
      {
        conn.Open();
        using (var command = new SqlCommand(commandText, conn))
        {
          command.CommandType = commandType;
          if (parameters != null)
          {
            foreach (var parameter in parameters)
            {
              command.Parameters.Add(parameter);
            }
          }

          result = command.ExecuteScalar();
        }
      }
      return result;
    }

    public DataTable ExecuteDataTable(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      var result = new DataTable();
      var connectionName = SystemConfiguration.Instance.GeneralSettings.ConnectionString;
      using (var conn = new SqlConnection(connectionName))
      {
        conn.Open();
        using (var command = new SqlCommand(commandText, conn))
        {
          command.CommandType = commandType;
          command.CommandText = commandText;
          if (parameters != null)
          {
            foreach (var parameter in parameters)
            {
              command.Parameters.Add(parameter);
            }
          }

          using (var adapter = new SqlDataAdapter(command))
          {
            adapter.Fill(result);
          }
        }
      }
      
      return result;
    }

    public StringDictionary ExecuteStringDictionary(CommandType commandType, string commandText,
      params DbParameter[] parameters)
    {
      StringDictionary result = new StringDictionary();

      using (DataTable dataTable = ExecuteDataTable(commandType, commandText, parameters))
      {
        if (dataTable.Rows.Count > 0)
        {
          DataRow currentRow = dataTable.Rows[0];
          foreach (DataColumn col in dataTable.Columns)
          {
            result.Add(col.ColumnName, currentRow[col.ColumnName].ToString());
          }
        }
      }
      return result;
    }

    public DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      var result = new DataSet();
      var connectionName = SystemConfiguration.Instance.GeneralSettings.ConnectionString;
      using (var conn = new SqlConnection(connectionName))
      {
        conn.Open();
        using (var command = new SqlCommand(commandText, conn))
        {
          command.CommandType = commandType;
          command.CommandText = commandText;
          if (parameters != null)
          {
            foreach (var parameter in parameters)
            {
              command.Parameters.Add(parameter);
            }
          }

          using (var adapter = new SqlDataAdapter(command))
          {
            adapter.Fill(result);
          }
        }
      }


      return result;
    }


    public void BulkInsert(DataTable sourceDataTable, string destinationTableName,
      IList<SqlBulkCopyColumnMapping> columnMappings)
    {
      using (SqlConnection connection = new SqlConnection(SystemConfiguration.Instance.GeneralSettings.ConnectionString))
      {
        connection.Open();
        using (var copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, null))
        {
          copy.BulkCopyTimeout = 300; //5 minutes
          //copy.BatchSize = 4000;

          foreach (var columnMapping in columnMappings)
          {
            copy.ColumnMappings.Add(columnMapping);
          }
          copy.DestinationTableName = destinationTableName;
          copy.WriteToServer(sourceDataTable);
        }
        connection.Close();
      }
    }


    public void ExecuteDataReader(CommandType commandType, string commandText, Action<IDataReader> processData,
      params DbParameter[] parameters)
    {
      ExecuteDataReader(commandType, commandText, CommandBehavior.Default, processData, parameters);
    }


    public void ExecuteDataReader(CommandType commandType, string commandText, CommandBehavior commandBehavior,
      Action<IDataReader> processData, params DbParameter[] parameters)
    {
      var connectionName = SystemConfiguration.Instance.GeneralSettings.ConnectionString;
      using (SqlConnection conn = new SqlConnection(connectionName))
      {
        conn.Open();
        using (SqlCommand cmd = new SqlCommand())
        {
          cmd.Connection = conn;
          cmd.CommandText = commandText;
          cmd.CommandType = commandType;
          if (parameters != null)
          {
            foreach (var parameter in parameters)
            {
              cmd.Parameters.Add(parameter);
            }
          }

          using (IDataReader dataReader = cmd.ExecuteReader(commandBehavior))
          {
            processData(dataReader);
          }

        }
        conn.Close();
      }
    }
  }
}