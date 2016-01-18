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
using Misuka.Services.CommandServices.Persons;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
    public class PersonController : Controller
    {
      private readonly IPersonReportService _personReportService;
      private readonly IPersonCommandService _personCommandService;
      
      public PersonController(IPersonCommandService personCommandService, IPersonReportService personReportService)
      {
        _personReportService = personReportService;
        _personCommandService = personCommandService;
      }
      //
      // GET: /Person/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetPersons(JqGridRequest request, string keyword)
      {
        var searchCriteria = new PersonSearchCriteria();
        var result = _personReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var Person = new PersonModel();
        if (id != null)
          Person = Mapper.Map<PersonDTO, PersonModel>(_personReportService.GetById((Guid)id));
        return View("Edit", Person);
      }

      [HttpPost]
      public ActionResult Edit(PersonModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.PersonId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_person", ex.Message);
        }
        return ModelState.JsonValidation();
      }


      [HttpPost]
      public ActionResult DeletePersons(string ids)
      {

        ThrowError.WebResourceNotFound(string.IsNullOrEmpty(ids));
        var selectedIds = StringUtilities.ParseCommaSeparatedStringToList<Guid>(ids);

        if (selectedIds.Count > 0)
        {
          _personCommandService.DeletePerson(new DeletePersonCommand(selectedIds));
          return ModelState.JsonValidation(new { Success = true });
        }

        ModelState.AddModelError("DeletePersons", "Input parameters are invalid.");

        return ModelState.JsonValidation();

      }
      #region Save

      private void Save(PersonModel model)
      {
        //if (model.PersonId == Guid.Empty)
        //{
        //  var createCommand = new AddPersonCommand(model.FullName);
        //  model.PersonId = _personCommandService.AddPerson(createCommand);
        //}
        //else
        //{
        //  var updateCommand = new EditPersonCommand(model.PersonId, model.Name, model.Description, model.ImageURL);
        //  _personCommandService.EditPerson(updateCommand);
        //}
      }

      #endregion

	}
}