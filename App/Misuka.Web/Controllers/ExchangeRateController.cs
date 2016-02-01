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
using Misuka.Services.CommandServices.ExchangeRates;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
     [Authorize]
    public class ExchangeRateController : Controller
    {
      private readonly IExchangeRateReportService _exchangeRateReportService;
      private readonly IExchangeRateCommandService _exchangeRateCommandService;
      
      public ExchangeRateController(IExchangeRateCommandService exchangeRateCommandService, IExchangeRateReportService exchangeRateReportService)
      {
        _exchangeRateReportService = exchangeRateReportService;
        _exchangeRateCommandService = exchangeRateCommandService;
      }
      //
      // GET: /ExchangeRate/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetExchangeRates(JqGridRequest request, string keyword)
      {
        var searchCriteria = new ExchangeRateSearchCriteria();
        var result = _exchangeRateReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var ExchangeRate = new ExchangeRateModel();
        if (id != null)
          ExchangeRate = Mapper.Map<ExchangeRateDTO, ExchangeRateModel>(_exchangeRateReportService.GetById((Guid)id));
        return View("Edit", ExchangeRate);
      }

      [HttpPost]
      public ActionResult Edit(ExchangeRateModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.ExchangeRateId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_exchangeRate", ex.Message);
        }
        return ModelState.JsonValidation();
      }


      [HttpPost]
      public ActionResult DeleteExchangeRates(string ids)
      {

        ThrowError.WebResourceNotFound(string.IsNullOrEmpty(ids));
        var selectedIds = StringUtilities.ParseCommaSeparatedStringToList<Guid>(ids);

        if (selectedIds.Count > 0)
        {
          _exchangeRateCommandService.DeleteExchangeRate(new DeleteExchangeRateCommand(selectedIds));
          return ModelState.JsonValidation(new { Success = true });
        }

        ModelState.AddModelError("DeleteExchangeRates", "Input parameters are invalid.");

        return ModelState.JsonValidation();

      }
      #region Save

      private void Save(ExchangeRateModel model)
      {
        if (model.ExchangeRateId == Guid.Empty)
        {
          var createCommand = new AddExchangeRateCommand(model.Name,model.Price);
          model.ExchangeRateId = _exchangeRateCommandService.AddExchangeRate(createCommand);
        }
        else
        {
          var updateCommand = new EditExchangeRateCommand(model.ExchangeRateId,model.Name, model.Price);
          _exchangeRateCommandService.EditExchangeRate(updateCommand);
        }
      }

      #endregion

	}
}