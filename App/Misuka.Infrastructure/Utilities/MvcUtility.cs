using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Misuka.Infrastructure.Utilities
{
  /// <summary>
  /// Used to create temporary controller context
  /// </summary>
  internal class DummyController : Controller
  {
  }

  /// <summary>
  /// MVC utilities for web form
  /// </summary>
  public static class MvcUtility
  {
    /// <summary>
    /// Get Html helper
    /// </summary>
    /// <returns>HtmlHelper</returns>
    public static HtmlHelper GetHtmlHelper()
    {
      var httpContext = new HttpContextWrapper(HttpContext.Current);
      var routeData = new RouteData();
      routeData.Values.Add("controller", typeof(DummyController).Name);
      var controllerContext = new ControllerContext(new RequestContext(httpContext, routeData), new DummyController());
      var viewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, "View"), new ViewDataDictionary(), new TempDataDictionary(), httpContext.Response.Output);

      var helper = new HtmlHelper(viewContext, new ViewPage());
      return helper;
    }

    /// <summary>
    /// Get Html helper with TModel
    /// </summary>
    /// <param name="model"></param>
    /// <returns>HtmlHelper with TModel</returns>
    public static HtmlHelper<TModel> GetHtmlHelper<TModel>(TModel model)
    {
      var httpContext = new HttpContextWrapper(HttpContext.Current);
      var routeData = new RouteData();
      routeData.Values.Add("controller", typeof(DummyController).Name);
      var controllerContext = new ControllerContext(new RequestContext(httpContext, routeData), new DummyController());
      var viewData = new ViewDataDictionary { Model = model };
      var viewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, "View"), viewData, new TempDataDictionary(), httpContext.Response.Output);

      var helper = new HtmlHelper<TModel>(viewContext, new ViewPage { ViewData = viewData });
      return helper;
    }
  }
}
