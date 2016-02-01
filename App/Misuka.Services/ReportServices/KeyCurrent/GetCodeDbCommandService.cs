using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.Data.ADO;

namespace Misuka.Services.ReportServices.KeyCurrent
{
  class GetCodeDbCommandService: ICommand<KeyGenerationDTO>
  {
    private readonly string _keyType;

    public GetCodeDbCommandService(string  keyType)
    {
      _keyType = keyType;
    }

    private const string _sql = @"
                  UPDATE KeyGeneration SET CurrentIndex = CurrentIndex + 1 WHERE KeyType = @KeyType
                  SELECT *,CodeNew = ISNULL(Prefix + '-','') + right('0' + convert(varchar(20), DAY(GETDATE())), 2)
                   + right('0' + convert(varchar(20), MONTH(GETDATE())), 2)  
                   + right('0' + convert(varchar(20), YEAR(GETDATE())), 2)  + '-' + CurrentIndexString
                  FROM (
	                  SELECT KeyType,CurrentIndex,CurrentIndexString =
	                  CASE 
	                   WHEN CurrentIndex <= 9999 THEN right('0000' + convert(varchar(20), CurrentIndex), 4)
	                   ELSE convert(varchar(20),CurrentIndex)
	                  END ,Prefix
	                  FROM KeyGeneration WHERE KeyType = @KeyType
                  )tmp
";

    public KeyGenerationDTO Execute()
    {
      var item = new KeyGenerationDTO();
      ADO.ExecuteDataReader(CommandType.Text, _sql, dataReader =>
      {
        using (var reader = new SafeDataReader(dataReader))
        {
          while (reader.Read())
          {
            item = GetDto(reader);
          }
        }
      },
          SQLParameter.CreateParameter("KeyType", DbType.String, _keyType)
      );

      return item;
    }

    private static KeyGenerationDTO GetDto(SafeDataReader reader)
    {
      var item = new KeyGenerationDTO
      {
        CodeNew = reader.GetString("CodeNew"),
        CurrentIndex = reader.GetInt32("CurrentIndex"),
        KeyType = reader.GetString("KeyType"),
        Prefix = reader.GetString("Prefix"),
       
      };
      return item;
    }

  

 

   
  }
}
