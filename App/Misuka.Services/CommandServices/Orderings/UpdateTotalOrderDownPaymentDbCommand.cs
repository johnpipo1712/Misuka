using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.Data.ADO;

namespace Misuka.Services.CommandServices.Orderings
{
  public class UpdateTotalOrderDownPaymentDbCommand: ICommand<bool>
  {
    public UpdateTotalOrderDownPaymentDbCommand(Guid orderingId)
    {
      OrderingId = orderingId;
    }
    public Guid OrderingId { get; private set; }

    private const string SQL = @" 
                                 DECLARE @TotalAmount DECIMAL(18,2),@PercentDownpayment FLOAT


                                SELECT @TotalAmount = SUM(ISNULL(Price,0)*ISNULL(Quantity,0)) FROM OrderingDetail WHERE OrderingId = @OrderingId

                                 SELECT  @PercentDownpayment = tm.PercentDownPayment 
                                   FROM Ordering o 
                                LEFT JOIN Person p ON o.PersonId = p.PersonId 
                                LEFT JOIN TypeMember tm ON tm.TypeMemberId = p.TypeMemberId
                                  WHERE OrderingId = @OrderingId
  
                                UPDATE OrderingDetail
                                    SET TotalDownPayment =  @TotalAmount*(@PercentDownpayment)
                                  WHERE OrderingId = @OrderingId";
    public bool Execute()
    {
      ADO.ExecuteNonQuery(CommandType.Text, SQL,
        SQLParameter.CreateParameter("OrderingId", DbType.Guid, OrderingId));
      return true;
    }

  }

}
