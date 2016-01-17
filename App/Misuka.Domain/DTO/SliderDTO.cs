using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Infrastructure.Configuration;

namespace Misuka.Domain.DTO
{
  public class SliderDTO
  {
    public Guid SliderId { get; set; }

    public string ImageURL { get; set; }
    public string FileVirtualPath
    {
      get
      {
        if (!String.IsNullOrEmpty(ImageURL))
        {
          return string.Format("{0}/{1}", SystemConfiguration.Instance.GeneralSettings.UploadDocumentFolder, ImageURL);
        }
        else
        {
          return "/Content/img/none-img.png";
        }

      }
    }
    public string Name { get; set; }

    public string Description { get; set; }

    public int? Type { get; set; }
  }
}
