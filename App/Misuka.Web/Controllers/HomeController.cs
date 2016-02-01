using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.MVC;
using Lib.Web.Mvc.JQuery.JqGrid;
using Misuka.Domain.Enum;
using Misuka.Domain.SearchCriteria;
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
       return RedirectToAction("Home");
     }
     [Route("TrangChu")]
    public ActionResult Home()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.TrangChu);
      home.Sliders = _sliderReportService.GetAll();
      home.WebSiteLink = _webSiteLinkReportService.GetAll();
      return View("Index",home);
    }
    [Route("HuongDanDatHang")]
    public ActionResult About()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.HuongDanDatHang);
      return View(home);
    }
    [Route("CacWebSite")]
    public ActionResult Contact()
    {
      var home = new HomeModel();
      home.ContentMenu = _contentMenuReportService.GetById(ContentMenuObject.CacWebsite);
      return View(home);
    }
    [HttpGet]
    [Route("DatHang")]
    public ActionResult OrderPlace()
    {
      if (Session["Person"] == null)
      {
        Session["Person"] = Guid.Empty;
      }
       return View(new OrderingModel(){PersonId = (Guid)Session["Person"]});
    }
    [HttpPost]
    public ActionResult AddOrder(OrderingModel model)
    {
      try
      {
        if (model.TotalDiscuss == null)
        {
          model.TotalDiscuss = 0;
        }
        if (model.Price == null)
        {
          model.Price = 0;
        }
        if (model.Quantity == null)
        {
          model.Quantity = 1;
        }
          var createCommand = new AddOrderingCommand((Guid)model.PersonId, model.ExchangeRateId, model.Phone, model.Address, model.Note, (double)model.TotalDiscuss);
          model.OrderingId = _orderingCommandService.AddOrdering(createCommand);
          _orderingDetailCommandService.AddOrderingDetail(new AddOrderingDetailCommand(model.ProductCode, model.Name, model.Brand, (decimal)model.Price, (int)model.Quantity, model.Note, model.Link, model.LinkUrl, model.Color, model.Size,model.OrderingId));
          return ModelState.JsonValidation(new { Success = true, model });

      }
      catch (Exception ex)
      {

        return ModelState.JsonValidation(new { Success = false,Error = ex.Message });
      }
     
    
    }
    [Route("LichSu")]
    public ActionResult OrderHistory()
    {
      return View();
    }
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult GetOrders(JqGridRequest request, string keyword)
    {
      var searchCriteria = new OrderingSearchCriteria(){PersonId = (Guid)Session["Person"]};
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
  }
}
