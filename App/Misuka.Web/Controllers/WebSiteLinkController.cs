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
using Misuka.Services.CommandServices.WebSiteLinks;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
    public class WebSiteLinkController: Controller
    {
      private readonly IWebSiteLinkReportService _webSiteLinkReportService;
      private readonly IWebSiteLinkCommandService _webSiteLinkCommandService;
      
      public WebSiteLinkController(IWebSiteLinkCommandService webSiteLinkCommandService, IWebSiteLinkReportService webSiteLinkReportService)
      {
        _webSiteLinkReportService = webSiteLinkReportService;
        _webSiteLinkCommandService = webSiteLinkCommandService;
      }
      //
      // GET: /WebSiteLink/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetWebSiteLinks(JqGridRequest request, string keyword)
      {
        var searchCriteria = new WebSiteLinkSearchCriteria();
        var result = _webSiteLinkReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var WebSiteLink = new WebSiteLinkModel();
        if (id != null)
          WebSiteLink = Mapper.Map<WebSiteLinkDTO, WebSiteLinkModel>(_webSiteLinkReportService.GetById((Guid)id));
        return View("Edit", WebSiteLink);
      }

      [HttpPost]
      public ActionResult Edit(WebSiteLinkModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.WebSiteLinkId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_webSiteLink", ex.Message);
        }
        return ModelState.JsonValidation();
      }


      [HttpPost]
      public ActionResult DeleteWebSiteLinks(string ids)
      {

        ThrowError.WebResourceNotFound(string.IsNullOrEmpty(ids));
        var selectedIds = StringUtilities.ParseCommaSeparatedStringToList<Guid>(ids);

        if (selectedIds.Count > 0)
        {
          _webSiteLinkCommandService.DeleteWebSiteLink(new DeleteWebSiteLinkCommand(selectedIds));
          return ModelState.JsonValidation(new { Success = true });
        }

        ModelState.AddModelError("DeleteWebSiteLinks", "Input parameters are invalid.");

        return ModelState.JsonValidation();

      }
      #region Save

      private void Save(WebSiteLinkModel model)
      {
        if (model.WebSiteLinkId == Guid.Empty)
        {
          var createCommand = new AddWebSiteLinkCommand(model.Name,model.Link,model.ImageUrl);
          model.WebSiteLinkId = _webSiteLinkCommandService.AddWebSiteLink(createCommand);
        }
        else
        {
          var updateCommand = new EditWebSiteLinkCommand(model.WebSiteLinkId, model.Name, model.Link, model.ImageUrl);
          _webSiteLinkCommandService.EditWebSiteLink(updateCommand);
        }
      }

      #endregion

	}
}