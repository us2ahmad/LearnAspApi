using AutoMapper;
using LearnAspApi.Dtos.Character;
using LearnAspApi.Models;

namespace LearnAspApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            CreateMap<UpdateCharacterDto,Character>();
        }
    }
}
