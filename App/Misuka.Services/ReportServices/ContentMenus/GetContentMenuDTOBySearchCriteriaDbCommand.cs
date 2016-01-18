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

namespace Misuka.Services.ReportServices.ContentMenus
{
  public class GetContentMenuDTOBySearchCriteriaDbCommand: ICommand<SearchResult<ContentMenuDTO>>
  {
    private readonly int _pageSize;
    private readonly int _pageIndex;
    private static ContentMenuSearchCriteria _criteria;

    public GetContentMenuDTOBySearchCriteriaDbCommand(ContentMenuSearchCriteria criteria, int pageIndex, int pageSize)
    {
      _criteria = criteria;
      _pageIndex = pageIndex;
      _pageSize = pageSize;
    }

    private const string _sql = @"
             WITH PAGING AS (                
                   select lt.*, row_number() over (order BY {0} ) as rownumber
                      from ContentMenu lt
                    {1}
            )
";

    public SearchResult<ContentMenuDTO> Execute()
    {
      int totalCount = 0;
      var items = new List<ContentMenuDTO>();
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

      return new SearchResult<ContentMenuDTO>(items, totalCount);
    }

    private static ContentMenuDTO GetDto(SafeDataReader reader)
    {
      var item = new ContentMenuDTO
      {
        ContentMenuId = reader.GetGuid("ContentMenuId"),
        Title = reader.GetString("Title"),
        Description = reader.GetString("Description"),
        Image = reader.GetString("Image"),
        MetaDescription = reader.GetString("MetaDescription"),
        MetaKeywork = reader.GetString("MetaKeywork")
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
      return " lt.Title ASC";
    }
  }
}
