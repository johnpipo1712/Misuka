using System;
using System.Collections.Generic;
using System.Linq;
using Misuka.Domain.Context;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.EntityFramework.Factories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;

namespace Misuka.Domain.Security
{
  [Serializable]
  public class SessionData
  {
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string MiddleName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public string ImageCoverUrl { get; set; }
    public bool IsAuthenticated { get; set; }
    public bool IsAdministrator { get; set; }
    public IList<Guid> RoleIds { get; set; }
    public string FileVirtualPath
    {
      get
      {
        if (!String.IsNullOrEmpty(ImageUrl))
        {
          return string.Format("{0}/{1}", SystemConfiguration.Instance.GeneralSettings.UploadDocumentFolder, ImageUrl);
        }
        else
        {
          return "/Content/img/none-img.png";
        }

      }
    }
    public string FileCoverVirtualPath
    {
      get
      {
        if (!String.IsNullOrEmpty(ImageCoverUrl))
        {
          return string.Format("{0}/{1}", SystemConfiguration.Instance.GeneralSettings.UploadDocumentFolder, ImageCoverUrl);
        }
        else
        {
          return "/Content/img/default_cover.jpg";
        }

      }
    }
    public bool Saved { get; set; }

    public SessionData()
    {
      UserId = Guid.Empty;
      Username = string.Empty;
      MiddleName = string.Empty;
      FirstName = string.Empty;
      LastName = string.Empty;
      Email = string.Empty;
      ImageUrl = string.Empty;
      IsAuthenticated = false;
      IsAdministrator = false;
      Saved = false;
    }

    internal SessionData(User loggingUser)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
  
      UserId = loggingUser.PersonId;
      Username = loggingUser.UserName;

      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      var personInfo = unitofWork.RepositoryAsync<Person>()
         .Query(m => m.PersonId == loggingUser.PersonId)
         .Select()
         .FirstOrDefault();

      if (personInfo != null)
      {
        FirstName = personInfo.FullName;
        Email = personInfo.Email;
        ImageUrl = personInfo.ImageUrl;
        ImageCoverUrl = personInfo.ImageCoverUrl;
      }

      IsAuthenticated = true;
      Saved = false;
    }
    public IList<int> AccessiblePermissions
    {
      get;
      set;
    }
  }
}