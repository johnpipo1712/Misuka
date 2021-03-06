﻿using System;
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
using Misuka.Services.CommandServices.Sliders;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
   [Authorize]
    public class SliderController  : Controller
    {
      private readonly ISliderReportService _sliderReportService;
      private readonly ISliderCommandService _sliderCommandService;
      
      public SliderController(ISliderCommandService sliderCommandService, ISliderReportService sliderReportService)
      {
        _sliderReportService = sliderReportService;
        _sliderCommandService = sliderCommandService;
      }
      //
      // GET: /Slider/
       public ActionResult Index()
      {
         return View("Index");
      }
      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult GetSliders(JqGridRequest request, string keyword)
      {
        var searchCriteria = new SliderSearchCriteria();
        var result = _sliderReportService.Search(searchCriteria, request.RecordsCount, request.PageIndex);
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
        var Slider = new SliderModel();
        if (id != null)
          Slider = Mapper.Map<SliderDTO, SliderModel>(_sliderReportService.GetById((Guid)id));
        return View("Edit", Slider);
      }

      [HttpPost]
      public ActionResult Edit(SliderModel model)
      {
        if (!ModelState.IsValid)
        {
          return ModelState.JsonValidation();
        }

        try
        {
          Save(model);
          return ModelState.JsonValidation(new { Success = true, model.SliderId });
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("Edit_slider", ex.Message);
        }
        return ModelState.JsonValidation();
      }


      [HttpPost]
      public ActionResult DeleteSliders(string ids)
      {

        ThrowError.WebResourceNotFound(string.IsNullOrEmpty(ids));
        var selectedIds = StringUtilities.ParseCommaSeparatedStringToList<Guid>(ids);

        if (selectedIds.Count > 0)
        {
          _sliderCommandService.DeleteSlider(new DeleteSliderCommand(selectedIds));
          return ModelState.JsonValidation(new { Success = true });
        }

        ModelState.AddModelError("DeleteSliders", "Input parameters are invalid.");

        return ModelState.JsonValidation();

      }
      #region Save

      private void Save(SliderModel model)
      {
        if (model.SliderId == Guid.Empty)
        {
          var createCommand = new AddSliderCommand(model.Name,model.Description,model.ImageURL);
          model.SliderId = _sliderCommandService.AddSlider(createCommand);
        }
        else
        {
          var updateCommand = new EditSliderCommand(model.SliderId, model.Name, model.Description, model.ImageURL);
          _sliderCommandService.EditSlider(updateCommand);
        }
      }

      #endregion

	}
}