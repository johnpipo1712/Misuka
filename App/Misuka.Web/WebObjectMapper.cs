using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Web.Helpers;
using Misuka.Web.Models;

namespace Misuka.Web
{
  public class WebObjectMapper
  {
    private bool _mapped;

    public void Map()
    {
      if (!_mapped)
      {
        Mapper.CreateMap<UserDTO, UserModel>();
        Mapper.CreateMap<PersonDTO, PersonModel>();
        Mapper.CreateMap<ContentMenuDTO, ContentMenuModel>();
        Mapper.CreateMap<ExchangeRateDTO, ExchangeRateModel>();
        Mapper.CreateMap<OrderingDTO, OrderingModel>();
        Mapper.CreateMap<OrderingDetailDTO, OrderingDetailModel>();
        Mapper.CreateMap<SliderDTO, SliderModel>();
        Mapper.CreateMap<TypeMemberDTO, TypeMemberModel>();
        Mapper.CreateMap<WebSiteLinkDTO, WebSiteLinkModel>();
        _mapped = true;
      }
    }
  }
}