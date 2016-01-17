using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.Data.ADO;

namespace Misuka.Services.ReportServices.Sliders
{
  public class GetSliderDTOBySearchCriteriaDbCommand: ICommand<SearchResult<SliderDTO>>
  {
    private readonly int _pageSize;
    private readonly int _pageIndex;
    private static SliderSearchCriteria _criteria;

    public GetSliderDTOBySearchCriteriaDbCommand(SliderSearchCriteria criteria, int pageIndex, int pageSize)
    {
      _criteria = criteria;
      _pageIndex = pageIndex;
      _pageSize = pageSize;
    }

    private const string _sql = @"
             WITH PAGING AS (                
                   select lt.*, row_number() over (order BY {0} ) as rownumber
                      from Slider lt
                    {1}
            )
";

    public SearchResult<SliderDTO> Execute()
    {
      int totalCount = 0;
      var items = new List<SliderDTO>();
      ADO.ExecuteDataReader(CommandType.Text, BuildQuery(), dataReader =>
      {
        using (var reader = new SafeDataReader(dataReader))
        {
          if (!reader.Read()) return;
          totalCount = reader.GetInt32(0);
          reader.NextResult();

          while (reader.Read())
          {
            items.Add(GetDto(reader));
          }
        }
      },
          SQLParameter.CreateParameter("PageSize", DbType.Int32, _pageSize),
          SQLParameter.CreateParameter("PageIndex", DbType.Int32, _pageIndex)
      );

      return new SearchResult<SliderDTO>(items, totalCount);
    }

    private static SliderDTO GetDto(SafeDataReader reader)
    {
      var item = new SliderDTO
      {
        SliderId = reader.GetGuid("SliderId"),
        Name = reader.GetString("Name"),
        Description = reader.GetString("Description"),
        ImageURL = reader.GetString("ImageURL"),
        Type = reader.GetNullableInt32("Type")
      };
      return item;
    }

    private string BuildQuery()
    {
      var query = new StringBuilder();
      var sqlQuery = string.Format(_sql, BuildOrderClause(), BuildWhereClause());
      query.AppendFormat("{0} {1};{0} {2}", sqlQuery, @"SELECT COUNT(1) FROM paging", @" SELECT * FROM paging WHERE (@PageSize=0 or rownumber between @PageSize*@PageIndex+1 and @PageSize*(@PageIndex+1)) ORDER BY rownumber");
      return query.ToString();
    }

    private string BuildWhereClause()
    {
      string whereClause = "Where (1=1)";

      

      return whereClause;
    }

    private static string BuildOrderClause()
    {
      return " lt.Name ASC";
    }
  }
}
