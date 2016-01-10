using System;
using System.ComponentModel;
using System.Reflection;

namespace Misuka.Infrastructure.Extensions
{
  public class LocalizedDescriptionAttribute : DescriptionAttribute
  {
    private PropertyInfo _resourceProperty;

    public string DescriptionResourceKey { get; set; }

    public Type DescriptionResourceType { get; set; }

    public override string Description
    {
      get
      {
        if (this._resourceProperty == (PropertyInfo)null)
          this._resourceProperty = this.DescriptionResourceType.GetProperty(this.DescriptionResourceKey, BindingFlags.Static | BindingFlags.Public);
        if (this._resourceProperty == (PropertyInfo)null)
          return base.Description;
        else
          return (string)this._resourceProperty.GetValue((object)this._resourceProperty.DeclaringType, (object[])null);
      }
    }

    public LocalizedDescriptionAttribute()
    {
    }

    public LocalizedDescriptionAttribute(string description)
      : base(description)
    {
    }
  }
}
