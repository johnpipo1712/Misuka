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
using Misuka.Services.CommandServices.ContentMenus;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
    public class ContentMenuController : Controller
    {
      private readonly IContentMenuReportService _contentMenuReportService;
      private readonly IContentMenuCommandService _contentMenuCommandService;
      
      public ContentMenuController(IContentMenuCommandService contentMenuCommandService, IContentMenuReportService contentMenuReportService)
      {
        _contentMenuReportService = contentMenuReportService;
        _contentMenuCommandService = contentMenuCommandService;
      }
      //
      // GET: /ContentMenu/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetContentMenus(JqGridRequest request, string keyword)
      {
        var searchCriteria = new ContentMenuSearchCriteria();
        var result = _contentMenuReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var ContentMenu = new ContentMenuModel();
        if (id != null)
          ContentMenu = Mapper.Map<ContentMenuDTO, ContentMenuModel>(_contentMenuReportService.GetById((Guid)id));
        return View("Edit", ContentMenu);
      }

      [HttpPost]
      public ActionResult Edit(ContentMenuModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.ContentMenuId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_contentMenu", ex.Message);
        }
        return ModelState.JsonValidation();
      }
      #region Save

      private void Save(ContentMenuModel model)
      {
        if (model.ContentMenuId == Guid.Empty)
        {
          var createCommand = new AddContentMenuCommand(model.Title,model.Image,model.MetaKeywork,model.MetaKeywork,model.Description);
          model.ContentMenuId = _contentMenuCommandService.AddContentMenu(createCommand);
        }
        else
        {
          var updateCommand = new EditContentMenuCommand(model.ContentMenuId, model.Title, model.Image, model.MetaKeywork, model.MetaKeywork, model.Description);
          _contentMenuCommandService.EditContentMenu(updateCommand);
        }
      }

      #endregion

	}
}