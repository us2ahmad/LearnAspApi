using LearnAspApi.Dtos.Character;
using LearnAspApi.Models;
using LearnAspApi.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace LearnAspApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController :ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok( await _characterService.GetAll());
        }
        
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int id)
        {
            var response = await _characterService.GetCharacterById(id);
           
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddNewCharacter(newCharacter));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = await _characterService.UpdateCharacter(updateCharacter);
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
          
            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteAllCharacter()
        {
            return Ok(await _characterService.DeleteAllCharacter());
        }
    }
}
