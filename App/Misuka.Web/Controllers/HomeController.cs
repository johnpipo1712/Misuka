using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.MVC;
using Misuka.Domain.Enum;
using Misuka.Services.CommandServices;
using Misuka.Services.CommandServices.OrderingDetails;
using Misuka.Services.CommandServices.Orderings;
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
     private readonly IOrderingDetailCommandService _orderingDetailCommandService;
     public HomeController(IContentMenuReportService contentMenuReportService
       , IWebSiteLinkReportService webSiteLinkReportService
       , IOrderingReportService orderingReportService
       , IOrderingCommandService orderingCommandService
       , ISliderReportService sliderReportService
       , IOrderingDetailCommandService orderingDetailCommandService)
     {
       _orderingDetailCommandService = orderingDetailCommandService;
       _contentMenuReportService = contentMenuReportService;
       _webSiteLinkReportService = webSiteLinkReportService;
       _orderingReportService = orderingReportService;
       _orderingCommandService = orderingCommandService;
        _sliderReportService = sliderReportService;
      }
    public ActionResult Index()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.TrangChu);
      home.Sliders = _sliderReportService.GetAll();
      home.WebSiteLink = _webSiteLinkReportService.GetAll();
      return View(home);
    }

    public ActionResult About()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.HuongDanDatHang);
      return View(home);
    }

    public ActionResult Contact()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.CacWebsite);
      return View(home);
    }
    [HttpGet]
    public ActionResult OrderPlace()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.DatHang);
      return View();
    }
    [HttpPost]
    public ActionResult AddOrder(OrderingModel model)
    {
      if (model.OrderingId == Guid.Empty)
      {
        var createCommand = new AddOrderingCommand((Guid)Session["Person"], model.ExchangeRateId, model.Phone, model.Address, model.Note, (double)model.TotalDiscuss);
        model.OrderingId = _orderingCommandService.AddOrdering(createCommand);
        _orderingDetailCommandService.AddOrderingDetail(new AddOrderingDetailCommand(model.ProductCode, model.Name, model.Brand, (decimal)model.Price, (int)model.Quantity, model.Note, model.Link, model.LinkUrl, model.Color,model.Size));
        return ModelState.JsonValidation(new { Success = true, model });
  
      }
      return ModelState.JsonValidation(new { Success = false });
    
    }
    public ActionResult OrderHistory()
    {
      return View();
    }
  }
}
