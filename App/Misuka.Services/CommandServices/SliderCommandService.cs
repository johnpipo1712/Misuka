using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.Sliders;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class SliderCommandService : ISliderCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly ISliderService _sliderService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public SliderCommandService(ISliderService sliderService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _sliderService = sliderService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
    public void EditSlider(EditSliderCommand command)
    {

      var data = _sliderService.Find(command.SliderId);
      data.Name = command.Name;
      data.Description = command.Description;
      data.Type = 1;
      data.ImageURL = command.ImageUrl;
      _sliderService.Update(data);
      _unitOfWork.SaveChanges();
    }

    public Guid AddSlider(AddSliderCommand command)
    {
      var data = new Slider()
      {
        SliderId = Guid.NewGuid(),
        Name = command.Name,
        Description = command.Description,
        Type = 1,
        ImageURL = command.ImageURL
      };

      _sliderService.Insert(data);
      _unitOfWork.SaveChanges();
      return data.SliderId;
    }

    public void DeleteSlider(DeleteSliderCommand command)
    {
      if(command.SelectedIds.Count() > 0)
      {
        foreach (var id in command.SelectedIds)
        {
          _sliderService.Delete(id);
          _unitOfWork.SaveChanges();
        }
      }
    }
  }
}
