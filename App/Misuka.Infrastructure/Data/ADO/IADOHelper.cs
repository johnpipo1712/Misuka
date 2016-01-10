using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace Misuka.Infrastructure.Data.ADO
{
  public interface IADOHelper
  {
    DbCommand CreateCommand(CommandType commandType, string commandText);
    int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parameters);
    object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] parameters);
    void ExecuteDataReader(CommandType commandType, string commandText, Action<IDataReader> processData, params DbParameter[] parameters);
    void ExecuteDataReader(CommandType commandType, string commandText, CommandBehavior commandBehavior, Action<IDataReader> processData, params DbParameter[] parameters);
    DataTable ExecuteDataTable(CommandType commandType, string commandText, params DbParameter[] parameters);
    StringDictionary ExecuteStringDictionary(CommandType commandType, string commandText, params DbParameter[] parameters);
    DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] parameters);
    void BulkInsert(DataTable sourceDataTable, string destinationTableName, IList<SqlBulkCopyColumnMapping> columnMappings);
  }
}
