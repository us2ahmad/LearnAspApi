using LearnAspApi.Dtos;
using LearnAspApi.Dtos.Character;
using LearnAspApi.Models;

namespace LearnAspApi.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>>  AddNewCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>>  UpdateCharacter(UpdateCharacterDto newCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteAllCharacter();

    }
}
