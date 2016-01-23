using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
using Misuka.Domain.Enum;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.Orderings;
using Misuka.Services.ReportServices;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class OrderingCommandService : IOrderingCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingService _orderingService;
    private readonly IKeyGenerationReportService _keyGenerationReportService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public OrderingCommandService(IOrderingService orderingService, IKeyGenerationReportService keyGenerationReportService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _orderingService = orderingService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _keyGenerationReportService = keyGenerationReportService;
      _userSession = new UserSession();
    }

    public Guid AddOrdering(AddOrderingCommand command)
    {
      string orderCode = _keyGenerationReportService.GetCode(KeyTypeObjects.Order).CodeNew;
      var ordering = new Ordering()
      {
        OrderingId = Guid.NewGuid(),
        Address = command.Address,
        Phone = command.Phone,
        ExchangeRateId = command.ExchangeRateId,
        Note = command.Note,
        TotalDiscuss = command.TotalDiscuss,
        PersonId = command.PersonId,
        CreatedDate = DateTime.Now,
        Status = (int)StatusOrderingEnum.New,
        OrderingCode = orderCode
      };
      _orderingService.Insert(ordering);
      _unitOfWork.SaveChanges();
      return ordering.OrderingId;
    }

    public void EditOrdering(EditOrderingCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.Phone = command.Phone;
      order.Address = command.Address;
      order.Note = command.Note;
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }

    public void EditOrderingFollowingDone(EditOrderingFollowingDoneCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.IsPaid = command.IsPaid; 
      order.IsDelivered = command.IsDelivered;
      if (order.IsPaid == true && order.IsDelivered == true)
      {
        order.Status = (int) StatusOrderingEnum.Done;
      }
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
    public void EditOrderingFollowingOrder(EditOrderingFollowingOrderCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.Status = (int) StatusOrderingEnum.InProcess;
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
    public void EditOrderingFollowingUSD(EditOrderingFollowingUSDCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.TransportFee = command.TransportFee;
      order.WeightFee = command.WeightFee;
      order.Status = (int)StatusOrderingEnum.ComeUsd;
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
    public void EditOrderingFollowingVN(EditOrderingFollowingVNCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.Status = (int)StatusOrderingEnum.ComeVn;
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
    public void EditStatusDownPayment(EditStatusDownPaymentCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.IsDownPayment = command.IsDownPayment;
      if (command.IsDownPayment)
      {
        order.TotalDownPayment = 0;
      }
      else
      {
        order.TotalDownPayment = 0; 
      }
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
    public void EditStatusReject(EditStatusRejectCommand command)
    {
      var order = _orderingService.Find(command.OrderingId);
      order.Status = (int)StatusOrderingEnum.Reject;
      _orderingService.Update(order);
      _unitOfWork.SaveChanges();
    }
  }
}