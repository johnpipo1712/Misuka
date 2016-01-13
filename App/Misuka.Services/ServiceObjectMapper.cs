using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.Entity;

namespace Misuka.Services
{
  public class ServiceObjectMapper
  {
    private bool _mapped;

    public void Map()
    {
      if (!_mapped)
      {
        Mapper.CreateMap<User, UserDTO>();
        Mapper.CreateMap<Person, PersonDTO>();
        Mapper.CreateMap<ContentMenu, ContentMenuDTO>();
        Mapper.CreateMap<ExchangeRate, ExchangeRateDTO>();
        Mapper.CreateMap<Ordering, OrderingDTO>();
        Mapper.CreateMap<OrderingDetail, OrderingDetailDTO>();
        Mapper.CreateMap<OrderingHistory, OrderingHistoryDTO>();
        Mapper.CreateMap<Slider, SliderDTO>();
        Mapper.CreateMap<TypeMember, TypeMemberDTO>();
        Mapper.CreateMap<WebSiteLink, WebSiteLinkDTO>();
        _mapped = true;
      }
    }
  }
}