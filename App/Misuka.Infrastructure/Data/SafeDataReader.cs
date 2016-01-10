using System;
using System.Data;
using System.Xml;

namespace Misuka.Infrastructure.Data
{
    /// <summary>
    /// This is a DataReader that 'fixes' any null values before
    /// they are returned to our business code.
    /// </summary>
    public class SafeDataReader : IDataReader
    {
        protected IDataReader _dataReader;

        /// <summary>
        /// Initializes the SafeDataReader object to use data from
        /// the provided DataReader object.
        /// </summary>
        /// <param name="dataReader">The source DataReader object containing the data.</param>
        public SafeDataReader(IDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        /// <summary>
        /// Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns "" for null.
        /// </remarks>
        public string GetString(string name)
        {
            return GetString(_dataReader.GetOrdinal(name));
        }

        public string GetString(int i)
        {
            if (_dataReader.IsDBNull(i))
                return string.Empty;
            return _dataReader.GetString(i);
        }


        /// <summary>
        /// Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Nothing for null.
        /// </remarks>
        public object GetValue(string name)
        {
            return GetValue(_dataReader.GetOrdinal(name));
        }

        public object GetValue(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetValue(i);
        }

        /// <summary>
        /// Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        public int GetInt32(string name)
        {
            return GetInt32(_dataReader.GetOrdinal(name));
        }

        public int GetInt32(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetInt32(i);
        }

        public int? GetNullableInt32(string name)
        {
            return GetNullableInt32(_dataReader.GetOrdinal(name));
        }

        public int? GetNullableInt32(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetInt32(i);
        }

        /// <summary>
        /// Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns 0 for null.
        /// </remarks>
        public double GetDouble(string name)
        {
            return GetDouble(_dataReader.GetOrdinal(name));
        }

        public double GetDouble(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetDouble(i);
        }

        public double? GetNullableDouble(string name)
        {
            return GetNullableDouble(_dataReader.GetOrdinal(name));
        }

        public double? GetNullableDouble(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetDouble(i);
        }

        /// <summary>
        /// Gets a Guid value from the datareader.
        /// </summary>
        public Guid GetGuid(string name)
        {
            return GetGuid(_dataReader.GetOrdinal(name));
        }

        public Guid GetGuid(int i)
        {
            if (_dataReader.IsDBNull(i))
                return Guid.Empty;
            return _dataReader.GetGuid(i);
        }

        public Guid? GetNullableGuid(string name)
        {
            return GetNullableGuid(_dataReader.GetOrdinal(name));
        }

        public Guid? GetNullableGuid(int i)
        {
            if (_dataReader.IsDBNull(i))
            {
                return null;
            }
            return _dataReader.GetGuid(i);
        }

        /// <summary>
        /// Reads the next row of data from the datareader.
        /// </summary>
        public bool Read()
        {
            return _dataReader.Read();
        }

        /// <summary>
        /// Moves to the next result set in the datareader.
        /// </summary>
        public bool NextResult()
        {
            return _dataReader.NextResult();
        }

        /// <summary>
        /// Closes the datareader.
        /// </summary>
        public void Close()
        {
            _dataReader.Close();
        }

        /// <summary>
        /// Returns the depth property value from the datareader.
        /// </summary>
        public int Depth
        {
            get
            {
                return _dataReader.Depth;
            }
        }

        /// <summary>
        /// Returns the FieldCount property from the datareader.
        /// </summary>
        public int FieldCount
        {
            get
            {
                return _dataReader.FieldCount;
            }
        }

        /// <summary>
        /// Gets a boolean value from the datareader.
        /// </summary>
        public bool GetBoolean(string name)
        {
            return GetBoolean(_dataReader.GetOrdinal(name));
        }

        public bool GetBoolean(int i)
        {
            if (_dataReader.IsDBNull(i))
                return false;
            return _dataReader.GetBoolean(i);
        }

        public bool? GetNullableBoolean(string name)
        {
            return GetNullableBoolean(_dataReader.GetOrdinal(name));
        }

        public bool? GetNullableBoolean(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetBoolean(i);
        }

        /// <summary>
        /// Gets a byte value from the datareader.
        /// </summary>
        public byte GetByte(string name)
        {
            return GetByte(_dataReader.GetOrdinal(name));
        }

        public byte GetByte(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetByte(i);
        }

        /// <summary>
        /// Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        public Int64 GetBytes(string name, Int64 fieldOffset,
                              byte[] buffer, int bufferoffset, int length)
        {
            return GetBytes(_dataReader.GetOrdinal(name), fieldOffset, buffer, bufferoffset, length);
        }

        public Int64 GetBytes(int i, Int64 fieldOffset,
                              byte[] buffer, int bufferoffset, int length)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Gets a char value from the datareader.
        /// </summary>
        public char GetChar(string name)
        {
            return GetChar(_dataReader.GetOrdinal(name));
        }

        public char GetChar(int i)
        {
            if (_dataReader.IsDBNull(i))
                return char.MinValue;
            char[] myChar = new char[1];
            _dataReader.GetChars(i, 0, myChar, 0, 1);
            return myChar[0];
        }

        /// <summary>
        /// Invokes the GetChars method of the underlying datareader.
        /// </summary>
        public Int64 GetChars(string name, Int64 fieldoffset,
                              char[] buffer, int bufferoffset, int length)
        {
            return GetChars(_dataReader.GetOrdinal(name), fieldoffset, buffer, bufferoffset, length);
        }

        public Int64 GetChars(int i, Int64 fieldoffset,
                              char[] buffer, int bufferoffset, int length)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Invokes the GetData method of the underlying datareader.
        /// </summary>
        public IDataReader GetData(string name)
        {
            return GetData(_dataReader.GetOrdinal(name));
        }

        public IDataReader GetData(int i)
        {
            return _dataReader.GetData(i);
        }

        /// <summary>
        /// Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        public string GetDataTypeName(string name)
        {
            return GetDataTypeName(_dataReader.GetOrdinal(name));
        }

        public string GetDataTypeName(int i)
        {
            return _dataReader.GetDataTypeName(i);
        }

        /// <summary>
        /// Gets a date value from the datareader.
        /// </summary>
        public DateTime GetDateTime(string name)
        {
            return GetDateTime(_dataReader.GetOrdinal(name));
        }

        public DateTime GetDateTime(int i)
        {
            if (_dataReader.IsDBNull(i))
                return DateTime.MinValue;
            return _dataReader.GetDateTime(i);
        }

        public DateTime? GetNullableDateTime(string name)
        {
            return GetNullableDateTime(_dataReader.GetOrdinal(name));
        }

        public DateTime? GetNullableDateTime(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetDateTime(i);
        }

        public decimal GetDecimal(string name)
        {
            return GetDecimal(_dataReader.GetOrdinal(name));
        }

        public decimal GetDecimal(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetDecimal(i);
        }

        public decimal? GetNullableDecimal(string name)
        {
            return GetNullableDecimal(_dataReader.GetOrdinal(name));
        }

        public decimal? GetNullableDecimal(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetDecimal(i);
        }

        /// <summary>
        /// Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        public Type GetFieldType(string name)
        {
            return GetFieldType(_dataReader.GetOrdinal(name));
        }

        public Type GetFieldType(int i)
        {
            return _dataReader.GetFieldType(i);
        }

        /// <summary>
        /// Gets a Single value from the datareader.
        /// </summary>
        public float GetFloat(string name)
        {
            return GetFloat(_dataReader.GetOrdinal(name));
        }

        public float GetFloat(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetFloat(i);
        }

        /// <summary>
        /// Gets a Short value from the datareader.
        /// </summary>
        public short GetInt16(string name)
        {
            return GetInt16(_dataReader.GetOrdinal(name));
        }

        public short GetInt16(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetInt16(i);
        }

        public short? GetNullableInt16(string name)
        {
            return GetNullableInt16(_dataReader.GetOrdinal(name));
        }

        public short? GetNullableInt16(int i)
        {
            if (_dataReader.IsDBNull(i))
                return null;
            return _dataReader.GetInt16(i);
        }

        /// <summary>
        /// Gets a Long value from the datareader.
        /// </summary>
        public Int64 GetInt64(string name)
        {
            return GetInt64(_dataReader.GetOrdinal(name));
        }

        public Int64 GetInt64(int i)
        {
            if (_dataReader.IsDBNull(i))
                return 0;
            return _dataReader.GetInt64(i);
        }

        public long? GetNullableInt64(string name)
        {
          return GetNullableInt64(_dataReader.GetOrdinal(name));
        }


        public long? GetNullableInt64(int i)
        {
          if (_dataReader.IsDBNull(i))
            return null;
          return _dataReader.GetInt64(i);
        }

        /// <summary>
        /// Gets an XML document from the datareader
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlDocument GetXML(string name)
        {
            return GetXml(_dataReader.GetOrdinal(name));
        }
        // <summary>
        /// Gets an XML document from the datareader
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetXml(int i)
        {
            XmlDocument doc = new XmlDocument();
            if (_dataReader.IsDBNull(i))
            {
                return doc;
            }
            doc.LoadXml(_dataReader.GetString(i));
            return doc;
        }

        /// <summary>
        /// Invokes the GetName method of the underlying datareader.
        /// </summary>
        public string GetName(int i)
        {
            return _dataReader.GetName(i);
        }

        /// <summary>
        /// Gets an ordinal value from the datareader.
        /// </summary>
        public int GetOrdinal(string name)
        {
            return _dataReader.GetOrdinal(name);
        }

        /// <summary>
        /// Determines if a column exists in the result set
        /// This method will loop through the fields which can have a small performance hit if you use it a lot and you may want to consider caching the results
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool HasColumn(string columnName)
        {
            for (int i = 0; i < _dataReader.FieldCount; i++)
            {
                if (_dataReader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase)) return true;
            }
            return false;
        }

        /// <summary>
        /// Invokes the GetSchemaTable method of the underlying datareader.
        /// </summary>
        public DataTable GetSchemaTable()
        {
            return _dataReader.GetSchemaTable();
        }


        /// <summary>
        /// Invokes the GetValues method of the underlying datareader.
        /// </summary>
        public int GetValues(object[] values)
        {
            return _dataReader.GetValues(values);
        }

        /// <summary>
        /// Returns the IsClosed property value from the datareader.
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return _dataReader.IsClosed;
            }
        }

        /// <summary>
        /// Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        public bool IsDBNull(int i)
        {
            return _dataReader.IsDBNull(i);
        }

        public bool IsDBNull(string name)
        {
            return IsDBNull(GetOrdinal(name));
        }
        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Nothing if the value is null.
        /// </remarks>
        public object this[string name]
        {
            get
            {
                object val = _dataReader[name];
                if (DBNull.Value.Equals(val))
                    return null;
                return val;
            }
        }

        /// <summary>
        /// Returns a value from the datareader.
        /// </summary>
        /// <remarks>
        /// Returns Nothing if the value is null.
        /// </remarks>
        public object this[int i]
        {
            get
            {
                if (_dataReader.IsDBNull(i))
                    return null;
                return _dataReader[i];
            }
        }
        /// <summary>
        /// Returns the RecordsAffected property value from the underlying datareader.
        /// </summary>
        public int RecordsAffected
        {
            get
            {
                return _dataReader.RecordsAffected;
            }
        }

        #region IDisposable

        /// <summary>
        /// Calls the Dispose method on the underlying datareader.
        /// </summary>
        public void Dispose()
        {
            _dataReader.Dispose();
        }

        #endregion

    }
}
