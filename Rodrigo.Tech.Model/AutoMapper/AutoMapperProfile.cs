using AutoMapper;
using Rodrigo.Tech.Model.Requests;
using Rodrigo.Tech.Model.Responses;
using Rodrigo.Tech.Repository.Tables;

namespace Rodrigo.Tech.Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Requests
            CreateMap<ItemRequest, Item>();
            #endregion

            #region Responses
            CreateMap<Item, ItemResponse>();
            #endregion
        }
    }
}
