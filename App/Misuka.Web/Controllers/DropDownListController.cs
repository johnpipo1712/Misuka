using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Misuka.Services.CommandServices;
using Misuka.Services.ReportServices;

namespace Misuka.Web.Controllers
{
  public class DropDownListController : Controller
  {
    private readonly IExchangeRateReportService _exchangeRateReportService;

    public DropDownListController(IExchangeRateReportService exchangeRateReportService)
    {
      _exchangeRateReportService = exchangeRateReportService;
    }

    //
    // GET: /DropDownList/
    public ActionResult GetExchangeRates()
    {
      var result = _exchangeRateReportService.GetAll().Select(t=> new {value = t.ExchangeRateId,text = t.Name,price = t.Price}).ToList();
      return Json(result, JsonRequestBehavior.AllowGet);
    }

  }
}