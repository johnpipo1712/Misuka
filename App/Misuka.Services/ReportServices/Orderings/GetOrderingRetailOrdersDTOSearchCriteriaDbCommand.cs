﻿using System;
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

namespace Misuka.Services.ReportServices.Orderings
{
  class GetOrderingRetailOrdersDTOSearchCriteriaDbCommand: ICommand<SearchResult<OrderingDTO>>
  {
    private readonly int _pageSize;
    private readonly int _pageIndex;
    private static OrderingSearchCriteria _criteria;

    public GetOrderingRetailOrdersDTOSearchCriteriaDbCommand(OrderingSearchCriteria criteria, int pageIndex, int pageSize)
    {
      _criteria = criteria;
      _pageIndex = pageIndex;
      _pageSize = pageSize;
    }

    private const string _sql = @"
             WITH PAGING AS (                
                   SELECT o.*,od.ProductCode,od.Name,od.Brand,od.Price,od.Quantity,od.Link,od.LinkUrl,od.Color,od.Size, row_number() over (order BY {0} ) as rownumber
                     FROM Ordering o
                LEFT JOIN OrderingDetail od
                       ON o.OrderingId = od.OrderingId 
                    {1}
            )
";

    public SearchResult<OrderingDTO> Execute()
    {
      int totalCount = 0;
      var items = new List<OrderingDTO>();
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

      return new SearchResult<OrderingDTO>(items, totalCount);
    }

    private static OrderingDTO GetDto(SafeDataReader reader)
    {
      var item = new OrderingDTO
      {
        OrderingId = reader.GetGuid("OrderingId"),
        Address = reader.GetString("Address"),
        CreatedBy = reader.GetGuid("CreatedBy"),
        CreatedByName = reader.GetString("CreatedByName"),
        CreatedDate = reader.GetNullableDateTime("CreatedDate"),
        ApprovedDate = reader.GetNullableDateTime("ApprovedDate"),
        CompleteDate = reader.GetNullableDateTime("CompleteDate"),
        ExchangeRateId = reader.GetGuid("ExchangeRateId"),
        IsDelivered = reader.GetNullableBoolean("IsDelivered"),
        IsDeposit = reader.GetNullableBoolean("IsDeposit"),
        IsPaid = reader.GetNullableBoolean("IsPaid"),
        IsPayAtHome = reader.GetNullableBoolean("IsPayAtHome"),
        Note = reader.GetString("Note"),
        NoteApproved = reader.GetString("NoteApproved"),
        NoteCustomer = reader.GetString("NoteCustomer"),
        OrderingCode = reader.GetString("OrderingCode"),
        PersonId = reader.GetGuid("PersonId"),
        Phone = reader.GetString("Phone"),
        TotalAmount = reader.GetNullableDecimal("TotalAmount"),
        TotalCount = reader.GetNullableInt64("TotalCount"),
        TotalCustomFees = reader.GetNullableDecimal("TotalCustomFees"),
        TotalDiscuss = reader.GetNullableDouble("TotalDiscuss"),
        TotalDomesticCharges = reader.GetNullableDecimal("TotalDomesticCharges"),
        TotalDownPayment = reader.GetNullableDecimal("TotalDownPayment"),
        TotalPrice = reader.GetNullableDecimal("TotalPrice"),
        TotalQuantity = reader.GetNullableInt32("TotalQuantity"),
        TotalShipAbroad = reader.GetNullableDecimal("TotalShipAbroad"),
        TotalShipInternal = reader.GetNullableDecimal("TotalShipInternal"),
        TotalVat = reader.GetNullableDouble("TotalVat"),
        TotalWage = reader.GetNullableDouble("TotalWage"),
        UsdOfDate = reader.GetNullableDateTime("UsdOfDate"),
        VndOfDate = reader.GetNullableDateTime("VndOfDate"),
        Type = reader.GetNullableInt32("Type"),
        Color = reader.GetString("Color"),
        Price = reader.GetNullableDecimal("Price"),
        Name = reader.GetString("Name"),
        Link = reader.GetString("Link"),
        LinkUrl = reader.GetString("LinkUrl"),
        ProductCode = reader.GetString("ProductCode"),
        Quantity = reader.GetNullableInt32("Quantity"),
        Size = reader.GetString("Size"),
        Brand = reader.GetString("Brand"),
        IsDownPayment = reader.GetNullableBoolean("IsDownPayment"),
        TransportFee = reader.GetNullableDouble("TransportFee"),
        WeightFee = reader.GetNullableDecimal("WeightFee"),
        Status = reader.GetInt32("Status")
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
      if (!string.IsNullOrEmpty(_criteria.Keyword))
      {
        whereClause += string.Format(" And (o.OrderingCode like '%{0}%')", _criteria.Keyword);
      }
      if (_criteria.PersonId != null)
      {
        whereClause += string.Format(" And (o.PersonId = '{0}')", _criteria.PersonId);
      }
      if (_criteria.Status != null)
      {
        whereClause += string.Format(" And (o.Status = {0})", _criteria.Status);
      }
      if (_criteria.IsDownPayment != null)
      {
        whereClause += string.Format(" And (o.IsDownPayment = {0})", Convert.ToInt32(_criteria.IsDownPayment));
      }
      return whereClause;
    }

    private static string BuildOrderClause()
    {
      return " o.CreatedDate DESC";
    }
  }
}