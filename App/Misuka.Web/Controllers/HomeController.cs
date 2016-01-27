using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Misuka.Services.CommandServices;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
  public class HomeController : Controller
  {
     private readonly ISliderReportService _sliderReportService;
     private readonly IContentMenuReportService _contentMenuReportService;
     private readonly IWebSiteLinkReportService _webSiteLinkReportService;
     private readonly IOrderingReportService _orderingReportService;
     private readonly IOrderingCommandService _orderingCommandService;

     public HomeController(IContentMenuReportService contentMenuReportService
       , IWebSiteLinkReportService webSiteLinkReportService
       , IOrderingReportService orderingReportService
       , IOrderingCommandService orderingCommandService
       , ISliderReportService sliderReportService)
     {
       _contentMenuReportService = contentMenuReportService;
       _webSiteLinkReportService = webSiteLinkReportService;
       _orderingReportService = orderingReportService;
       _orderingCommandService = orderingCommandService;
        _sliderReportService = sliderReportService;
      }
    public ActionResult Index()
    {
      var home = new HomeModel();
      return View(home);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}
