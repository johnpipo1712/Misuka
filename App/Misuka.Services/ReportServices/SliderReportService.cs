using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class SliderReportService: ISliderReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly ISliderService _sliderService;
    private readonly IUserSession _userSession;

    public SliderReportService(ICommandExecutor executor, ISliderService sliderService)
    {
      _executor = executor;
      _sliderService = sliderService;
      _userSession = new UserSession();
    }

  
    public SliderDTO GetById(Guid sliderId)
    {
      var slider = _sliderService.Find(sliderId);
      return Mapper.Map<Domain.Entity.Slider, SliderDTO>(slider);
    }

    public List<SliderDTO> GetAll()
    {
      var sliders = _sliderService.Queryable().ToList();
      return sliders.Select(Mapper.Map<Domain.Entity.Slider, SliderDTO>).ToList();
    
    }
  }
}

