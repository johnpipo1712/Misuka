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
using Misuka.Services.CommandServices.TypeMembers;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
   [Authorize]
    public class TypeMemberController : Controller
    {
      private readonly ITypeMemberReportService _typeMemberReportService;
      private readonly ITypeMemberCommandService _typeMemberCommandService;
      
      public TypeMemberController(ITypeMemberCommandService typeMemberCommandService, ITypeMemberReportService typeMemberReportService)
      {
        _typeMemberReportService = typeMemberReportService;
        _typeMemberCommandService = typeMemberCommandService;
      }
      //
      // GET: /TypeMember/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetTypeMembers(JqGridRequest request, string keyword)
      {
        var searchCriteria = new TypeMemberSearchCriteria();
        var result = _typeMemberReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var TypeMember = new TypeMemberModel();
        if (id != null)
          TypeMember = Mapper.Map<TypeMemberDTO, TypeMemberModel>(_typeMemberReportService.GetById((Guid)id));
        return View("Edit", TypeMember);
      }

      [HttpPost]
      public ActionResult Edit(TypeMemberModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.TypeMemberId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_typeMember", ex.Message);
        }
        return ModelState.JsonValidation();
      }


      [HttpPost]
      public ActionResult DeleteTypeMembers(string ids)
      {

        ThrowError.WebResourceNotFound(string.IsNullOrEmpty(ids));
        var selectedIds = StringUtilities.ParseCommaSeparatedStringToList<Guid>(ids);

        if (selectedIds.Count > 0)
        {
          _typeMemberCommandService.DeleteTypeMember(new DeleteTypeMemberCommand(selectedIds));
          return ModelState.JsonValidation(new { Success = true });
        }

        ModelState.AddModelError("DeleteTypeMembers", "Input parameters are invalid.");

        return ModelState.JsonValidation();

      }
      #region Save

      private void Save(TypeMemberModel model)
      {
        if (model.TypeMemberId == Guid.Empty)
        {
          var createCommand = new AddTypeMemberCommand(model.Name,model.ScoresFrom,model.ScoresTo,model.PercentDownPayment);
          model.TypeMemberId = _typeMemberCommandService.AddTypeMember(createCommand);
        }
        else
        {
          var updateCommand = new EditTypeMemberCommand(model.TypeMemberId, model.Name, model.ScoresFrom, model.ScoresTo, model.PercentDownPayment);
          _typeMemberCommandService.EditTypeMember(updateCommand);
        }
      }

      #endregion

	}
}