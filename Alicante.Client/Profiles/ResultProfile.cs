using Alicante.Client.Models;
using AutoMapper;

namespace Alicante.Profiles
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<ResultViewModel, ResultModel>()
                .ReverseMap();
        }
    }
}
