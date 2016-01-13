using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
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
  }
}
