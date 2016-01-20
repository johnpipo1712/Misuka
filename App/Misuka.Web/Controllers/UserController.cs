using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.MVC;
using System.Web.Security;
using AutoMapper;
using Lib.Web.Mvc.JQuery.JqGrid;
using Misuka.Domain.DTO;
using Misuka.Domain.Enum;
using Misuka.Domain.Providers;
using Misuka.Domain.SearchCriteria;
using Misuka.Infrastructure.Utilities;
using Misuka.Services.CommandServices;
using Misuka.Services.CommandServices.Persons;
using Misuka.Services.ReportServices;
using Misuka.Web.Models;

namespace Misuka.Web.Controllers
{
  public class UserController : Controller
  {
    private readonly IPersonReportService _personReportService;
    private readonly IPersonCommandService _personCommandService;

    public UserController(IPersonReportService personReportService, IPersonCommandService personCommandService)
    {
      _personReportService = personReportService;
      _personCommandService = personCommandService;
    }
    [HttpPost]
    public ActionResult LogOn(LogOnModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (CustomMembershipProvider.ValidateUser(model.UserName, model.Password,model.Type))
        {

          if (model.Type == (int) TypeUserEnum.Admin)
          {
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
              return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "ContentMenu");
          }
          else
          {
            var loggingUser = _personReportService.GetUserByUserName(model.UserName);
            Session["UserName"] = model.UserName;
            Session["Name"] = loggingUser.FullName;
            Session["Person"] = loggingUser.PersonId;
          }
        }

        ModelState.AddModelError("", "The user name or password provided is incorrect.");
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    public ActionResult LogOff()
    {
      Session.RemoveAll();
      Session.Abandon();
      return RedirectToAction("Index", "Home");
    }

    public ActionResult LogOffAdmin()
    {
      FormsAuthentication.SignOut();
      Session.RemoveAll();
      Session.Abandon();
      return RedirectToAction("Index", "ContentMenu");
    }
    public ActionResult Register()
    {
      return View();
    }

    //
    // POST: /Account/Register

    [HttpPost]
    public ActionResult Register(RegisterModel model)
    {
      // Attempt to register the user
      _personCommandService.Register(new RegisterCommand(model.FullName, model.Password, model.UserName, model.Email, (int)TypeUserEnum.Member));
        
      // If we got this far, something failed, redisplay form
      return RedirectToAction("Home","Index");
    }
  }
}