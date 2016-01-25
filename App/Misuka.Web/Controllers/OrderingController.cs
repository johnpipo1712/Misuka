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
      private readonly IOrderingReportService _sliderReportService;
      private readonly IOrderingCommandService _sliderCommandService;
      
      public OrderingController(IOrderingCommandService sliderCommandService, IOrderingReportService sliderReportService)
      {
        _sliderReportService = sliderReportService;
        _sliderCommandService = sliderCommandService;
      }
      //
      // GET: /Ordering/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetOrderings(JqGridRequest request, string keyword)
      {
        var searchCriteria = new OrderingSearchCriteria();
        var result = _sliderReportService.OrderingRetailOrders(searchCriteria, request.RecordsCount, request.PageIndex);
        var jsonData = new
        {
          total = (result.Count + request.RecordsCount - 1) / request.RecordsCount,
          page = request.PageIndex + 1,
          records = result.Count,
          rows = result.Items
        };
        return Json(jsonData, JsonRequestBehavior.AllowGet);
      }

      [HttpGet]
      public ActionResult Edit(Guid? id)
      {
        var Ordering = new OrderingModel();
        if (id != null)
          Ordering = Mapper.Map<OrderingDTO, OrderingModel>(_sliderReportService.GetById((Guid)id));
        return View("Edit", Ordering);
      }

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
          ModelState.AddModelError("Edit_slider", ex.Message);
        }
        return ModelState.JsonValidation();
      }


     
      #region Save

      private void Save(OrderingModel model)
      {
        //if (model.OrderingId == Guid.Empty)
        //{
        //  var createCommand = new AddOrderingCommand(model.Name,model.Description,model.ImageURL);
        //  model.OrderingId = _sliderCommandService.AddOrdering(createCommand);
        //}
        //else
        //{
        //  var updateCommand = new EditOrderingCommand(model.OrderingId, model.Name, model.Description, model.ImageURL);
        //  _sliderCommandService.EditOrdering(updateCommand);
        //}
      }

      #endregion

	}
}