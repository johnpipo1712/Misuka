using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Misuka.Infrastructure.Data.ADO
{
  public static class ADO
  {
    private static IADOHelper _ado;

    public static IADOHelper AdoHelper
    {
      get
      {
        if (_ado == null)
        {
          throw new ApplicationException(string.Format("{0} is not set on the {1}", typeof(IADOHelper).Name, typeof(ADOHelper).Name));
        }
        return _ado;
      }
      set
      {
        if (value != null)
        {
          _ado = value;
        }
      }
    }

    public static DbCommand CreateCommand(CommandType commandType, string commandText)
    {
      return AdoHelper.CreateCommand(commandType, commandText);
    }

    public static int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      return AdoHelper.ExecuteNonQuery(commandType, commandText, parameters);
    }

    public static object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      return AdoHelper.ExecuteScalar(commandType, commandText, parameters);
    }

    public static void ExecuteDataReader(CommandType commandType, string commandText, Action<IDataReader> processData, params DbParameter[] parameters)
    {
      AdoHelper.ExecuteDataReader(commandType, commandText, processData, parameters);
    }

    public static void ExecuteDataReader(CommandType commandType, string commandText, CommandBehavior commandBehavior, Action<IDataReader> processData, params DbParameter[] parameters)
    {
      AdoHelper.ExecuteDataReader(commandType, commandText, commandBehavior, processData, parameters);
    }

    public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      return AdoHelper.ExecuteDataTable(commandType, commandText, parameters);
    }

    public static StringDictionary ExecuteStringDictionary(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      return AdoHelper.ExecuteStringDictionary(commandType, commandText, parameters);
    }

    public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] parameters)
    {
      return AdoHelper.ExecuteDataSet(commandType, commandText, parameters);
    }
    
    public static void BulkCopyDataTable(DataTable dataTable, string destinationTableName, IList<SqlBulkCopyColumnMapping> columnMappings)
    {
      AdoHelper.BulkInsert(dataTable, destinationTableName, columnMappings);
    }
  }
}