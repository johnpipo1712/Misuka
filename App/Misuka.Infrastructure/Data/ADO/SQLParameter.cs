using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Misuka.Infrastructure.Data.ADO
{
  public static class SQLParameter
  {
    public static DbParameter CreateParameter(string name, object value)
    {
      SqlParameter parameter = CreateParameter(name);
      parameter.Value = value;
      return parameter;
    }

    public static DbParameter CreateParameter(string name, DbType type)
    {
      SqlParameter parameter = CreateParameter(name);
      parameter.DbType = type;
      parameter.Direction = ParameterDirection.Input;
      return parameter;
    }

    public static DbParameter CreateParameter(string name, DbType type, object value)
    {
      SqlParameter parameter = CreateParameter(name);
      parameter.DbType = type;
      parameter.Direction = ParameterDirection.Input;
      parameter.Value = value;
      return parameter;
    }

    public static DbParameter CreateParameter(string name, DbType type, ParameterDirection direction)
    {
      SqlParameter parameter = CreateParameter(name);
      parameter.DbType = type;
      parameter.Direction = direction;
      return parameter;
    }

    public static DbParameter CreateParameter(string name, DbType type, int size, ParameterDirection direction)
    {
      SqlParameter parameter = CreateParameter(name);
      parameter.DbType = type;
      parameter.Direction = direction;
      parameter.Size = size;
      return parameter;
    }


    private static SqlParameter CreateParameter(string parameterName)
    {
      if (parameterName.Trim().StartsWith("@"))
      {
        // Make sure the user doesnt try to name his parameters with "@", this is to ensure
        // the integrity of this database layer
        throw new ArgumentException("Invalid parameter name. Do not prefix parameter names with the '@' character");
      }
      string name = "@" + parameterName.Trim();	// Make it an Sql Server parameter

      SqlParameter parameter = new SqlParameter();
      parameter.ParameterName = name;
      return parameter;
    }

  }
}