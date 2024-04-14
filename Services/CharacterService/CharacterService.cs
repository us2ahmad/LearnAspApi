using AutoMapper;
using LearnAspApi.Data;
using LearnAspApi.Dtos.Character;
using LearnAspApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnAspApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext  _context;
       
        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
              response.Data = _mapper.Map<GetCharacterDto>(await _context.Characters.FirstAsync(c => c.Id == id));
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message =ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newCharacter)
        {
            Character character = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(_context.Characters)
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var respone = new ServiceResponse<GetCharacterDto>();
            try
            { 
                var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id.Equals(updateCharacter.Id));
                character.CreatedDate = DateTime.Now;
                _mapper.Map(updateCharacter,character);

                await _context.SaveChangesAsync();
                respone.Data = _mapper.Map<GetCharacterDto>(character);
            } 
            catch(Exception ex)
            {
                respone.Success = false;
                respone.Message = ex.Message;
            }
            return respone;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                _context.SaveChangesAsync();
                response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteAllCharacter()
        {
            var response =new ServiceResponse<List<GetCharacterDto>>();
            await  _context.Characters.ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return response;
        }
    }
}