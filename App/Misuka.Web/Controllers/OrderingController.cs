using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.MVC;
using AutoMapper;
using Lib.Web.Mvc.JQuery.JqGrid;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Infrastructure.Utilities;
using Misuka.Services.CommandServices;
using Misuka.Services.CommandServices.Orderings;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
    public class OrderingController: Controller
    {
      private readonly IOrderingReportService _orderingReportService;
      private readonly IOrderingCommandService _orderingCommandService;
      
      public OrderingController(IOrderingCommandService orderingCommandService, IOrderingReportService orderingReportService)
      {
        _orderingReportService = orderingReportService;
        _orderingCommandService = orderingCommandService;
      }
      //
      // GET: /Ordering/
       [Authorize]
       public ActionResult Index()
      {
         return View();
      }

       [Authorize]
      [AcceptVerbs(HttpVerbs.Get)]
       public ActionResult GetOrders(JqGridRequest request, string keyword)
      {
        var searchCriteria = new OrderingSearchCriteria();
        var result = _orderingReportService.OrderingRetailOrders(searchCriteria, request.RecordsCount, request.PageIndex);
        var jsonData = new
        {
          total = (result.Count + request.RecordsCount - 1) / request.RecordsCount,
          page = request.PageIndex + 1,
          records = result.Count,
          rows = result.Items
        };
        return Json(jsonData, JsonRequestBehavior.AllowGet);
      }

       [Authorize]
      [HttpGet]
      public ActionResult Edit(Guid? id)
      {
        var ordering = new OrderingModel();
        if (id != null)
          ordering = Mapper.Map<OrderingDTO, OrderingModel>(_orderingReportService.GetById((Guid)id));
        return PartialView("_Edit", ordering);
      }


       [Authorize]
      [HttpPost]
      public ActionResult Edit(OrderingModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.OrderingId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_ordering", ex.Message);
        }
        return ModelState.JsonValidation();
      }

       [Authorize]
      [HttpGet]
      public ActionResult EditOrderingFollowingDone(Guid? id)
      {
        var ordering = new OrderingModel();
        if (id != null)
          ordering = Mapper.Map<OrderingDTO, OrderingModel>(_orderingReportService.GetById((Guid)id));
        return PartialView("_EditOrderingFollowingDone", ordering);
      }

       [Authorize]
      [HttpGet]
      public ActionResult EditOrderingFollowingOrder(Guid? id)
      {
        var ordering = new OrderingModel();
        if (id != null)
          ordering = Mapper.Map<OrderingDTO, OrderingModel>(_orderingReportService.GetById((Guid)id));
        return PartialView("_EditOrderingFollowingOrder", ordering);
      }

       [Authorize]
      [HttpGet]
      public ActionResult EditOrderingFollowingUSD(Guid? id)
      {
        var ordering = new OrderingModel();
        if (id != null)
          ordering = Mapper.Map<OrderingDTO, OrderingModel>(_orderingReportService.GetById((Guid)id));
        return PartialView("_EditOrderingFollowingUSD", ordering);
      }

       [Authorize]
      [HttpPost]
      public ActionResult EditOrderingFollowingDone(OrderingModel model)
      {
        _orderingCommandService.EditOrderingFollowingDone(new EditOrderingFollowingDoneCommand(model.OrderingId, (bool)model.IsPaid, (bool)model.IsDelivered));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });

      }

       [Authorize]
      [HttpPost]
      public ActionResult EditOrderingFollowingOrder(OrderingModel model)
      {
        _orderingCommandService.EditOrderingFollowingOrder(new EditOrderingFollowingOrderCommand(model.OrderingId,model.NoteApproved,model.TotalCustomFees,model.TotalDomesticCharges,model.TotalShipInternal,model.TotalShipInternal,model.TotalVat,model.TotalWage));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });

      }


       [Authorize]
      [HttpPost]
      public ActionResult EditOrderingFollowingUSD(OrderingModel model)
      {
        _orderingCommandService.EditOrderingFollowingUSD(new EditOrderingFollowingUSDCommand(model.OrderingId, (double)model.TransportFee, (decimal)model.WeightFee));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });

      }

       [Authorize]
      [HttpPost]
      public ActionResult EditOrderingFollowingVN(OrderingModel model)
      {
        _orderingCommandService.EditOrderingFollowingVN(new EditOrderingFollowingVNCommand(model.OrderingId));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });
       
      }


      [HttpPost]
      public ActionResult EditStatusDownPayment(OrderingModel model)
      {
        _orderingCommandService.EditStatusDownPayment(new EditStatusDownPaymentCommand(model.OrderingId));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });

      }
      [HttpPost]
      public ActionResult EditStatusReject(OrderingModel model)
      {
        _orderingCommandService.EditStatusReject(new EditStatusRejectCommand(model.OrderingId));
        return ModelState.JsonValidation(new { Success = true, model.OrderingId });

      }
     
     
      #region Save

      private void Save(OrderingModel model)
      {
        //if (model.OrderingId == Guid.Empty)
        //{
        //  var createCommand = new AddOrderingCommand(model.Name,model.Description,model.ImageURL);
        //  model.OrderingId = _orderingCommandService.AddOrdering(createCommand);
        //}
        //else
        //{
        //  var updateCommand = new EditOrderingCommand(model.OrderingId, model.Name, model.Description, model.ImageURL);
        //  _orderingCommandService.EditOrdering(updateCommand);
        //}
      }

      #endregion

	}
}