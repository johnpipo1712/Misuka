using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Misuka.Infrastructure.MVC
{
  public class Attributes : RouteValueDictionary
  {

    /// <summary>
    /// Configures this instance.
    /// </summary>
    /// <returns></returns>
    public static Attributes Configure()
    {
      return new Attributes();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public Attributes AddAttribute(String name, String value)
    {
      Add(name, value);
      return this;
    }

    /// <summary>
    /// Adds the type.
    /// </summary>
    /// <param name="value">The value.</param>
    public Attributes AddType(string value)
    {
      this.Add("type", value);
      return this;
    }

    /// <summary>
    /// Adds the name.
    /// </summary>
    /// <param name="value">The value.</param>
    public Attributes AddName(string value)
    {
      this.Add("name", value);
      return this;
    }

    /// <summary>
    /// Adds the id.
    /// </summary>
    /// <param name="value">The value.</param>
    public Attributes AddId(string value)
    {
      this.Add("id", value);
      return this;
    }

    /// <summary>
    /// Adds the value.
    /// </summary>
    /// <param name="value">The value.</param>
    public Attributes AddValue(string value)
    {
      this.Add("value", value);
      return this;
    }

    /// <summary>
    /// Adds the CSS class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Attributes AddCssClass(string value)
    {
      this.Add("class", value);
      return this;
    }
  }

  public class DialogSetting
  {
    public bool Enable { get; private set; }
    public string Url { get; private set; }
    public string Title { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }


    public DialogSetting()
    {

    }

    public DialogSetting(bool enable, string url, string title)
    {
      Enable = enable;
      Url = url;
      Title = title;
    }

    public static DialogSetting Configure()
    {
      return new DialogSetting();
    }

    public static DialogSetting Configure(bool enable, string url, string title)
    {
      return new DialogSetting(enable, url, title);
    }

    public DialogSetting SetEnable(bool enable)
    {
      this.Enable = enable;
      return this;
    }

    public DialogSetting SetUrl(string url)
    {
      this.Url = url;
      return this;
    }

    public DialogSetting SetTitle(string title)
    {
      this.Title = title;
      return this;
    }

    public DialogSetting SetWidth(int width)
    {
      this.Width = width;
      return this;
    }

    public DialogSetting SetHeight(int height)
    {
      this.Height = height;
      return this;
    }

  }
}
