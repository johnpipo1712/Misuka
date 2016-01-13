using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices;
using Misuka.Services.ReportServices;
using Misuka.Services.Services;

namespace Misuka.Services
{
  public class ServiceRegister
  {
    private readonly IUnityContainer _container;

    public ServiceRegister(IUnityContainer container)
    {
      _container = container;
    }

    public void Register()
    {
      _container
        .RegisterType<IPersonService, PersonService>()
        .RegisterType<IPersonReportService, PersonReportService>()
        .RegisterType<IPersonCommandService, PersonCommandService>()
        .RegisterType<IRepository<User>, Repository<User>>()
        .RegisterType<IRepositoryAsync<Person>, Repository<Person>>();

      _container
        .RegisterType<IContentMenuService, ContentMenuService>()
        .RegisterType<IContentMenuReportService, ContentMenuReportService>()
        .RegisterType<IContentMenuCommandService, ContentMenuCommandService>()
        .RegisterType<IRepositoryAsync<ContentMenu>, Repository<ContentMenu>>();

      _container
        .RegisterType<IExchangeRateService, ExchangeRateService>()
        .RegisterType<IExchangeRateReportService, ExchangeRateReportService>()
        .RegisterType<IExchangeRateCommandService, ExchangeRateCommandService>()
        .RegisterType<IRepositoryAsync<ExchangeRate>, Repository<ExchangeRate>>();


      _container
        .RegisterType<IOrderingService, OrderingService>()
        .RegisterType<IOrderingReportService, OrderingReportService>()
        .RegisterType<IOrderingCommandService, OrderingCommandService>()
        .RegisterType<IRepositoryAsync<Ordering>, Repository<Ordering>>();


      _container
        .RegisterType<IOrderingDetailService, OrderingDetailService>()
        .RegisterType<IOrderingDetailReportService, OrderingDetailReportService>()
        .RegisterType<IOrderingDetailCommandService, OrderingDetailCommandService>()
        .RegisterType<IRepositoryAsync<OrderingDetail>, Repository<OrderingDetail>>();

      _container
        .RegisterType<IOrderingHistoryService, OrderingHistoryService>()
        .RegisterType<IOrderingHistoryReportService, OrderingHistoryReportService>()
        .RegisterType<IOrderingHistoryCommandService, OrderingHistoryCommandService>()
        .RegisterType<IRepositoryAsync<OrderingHistory>, Repository<OrderingHistory>>();

      _container
        .RegisterType<ISliderService, SliderService>()
        .RegisterType<ISliderReportService, SliderReportService>()
        .RegisterType<ISliderCommandService, SliderCommandService>()
        .RegisterType<IRepositoryAsync<Slider>, Repository<Slider>>();

      _container
        .RegisterType<ITypeMemberService, TypeMemberService>()
        .RegisterType<ITypeMemberReportService, TypeMemberReportService>()
        .RegisterType<ITypeMemberCommandService, TypeMemberCommandService>()
        .RegisterType<IRepositoryAsync<TypeMember>, Repository<TypeMember>>();


      _container
        .RegisterType<IWebSiteLinkService, WebSiteLinkService>()
        .RegisterType<IWebSiteLinkReportService, WebSiteLinkReportService>()
        .RegisterType<IWebSiteLinkCommandService, WebSiteLinkCommandService>()
        .RegisterType<IRepositoryAsync<WebSiteLink>, Repository<WebSiteLink>>();

      _container.RegisterType<IUserSession, UserSession>();
      _container.RegisterType<IFileService, FileService>();
      _container.RegisterType<ICommandExecutor, CommandExecutor>();
      _container.RegisterType<IUnitOfWork, UnitOfWork>();
    }
  }
}