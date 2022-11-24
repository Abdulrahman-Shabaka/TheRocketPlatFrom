using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheRocket.Dtos.UserDtos;
using TheRocket.Entities.Users;
using TheRocket.Repositories.RepoInterfaces;
using TheRocket.Shared;

namespace TheRocket.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize]

    public class PhoneController : ControllerBase
    {
        private readonly IPhoneRepo repo;
        public PhoneController(IPhoneRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<PhoneDto>>> GetPhoneByUserId([FromQuery] String userId)
        {
            SharedResponse<List<PhoneDto>> response = await repo.GetPhonesByUserId(userId);
            if (response.status == Status.notFound) return NotFound();
            return response.data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<List<PhoneDto>>> GetAllPhones()
        {
            SharedResponse<List<PhoneDto>> response = await repo.GetAll();
            if (response.status == Status.notFound) return NotFound();
            return response.data;
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<PhoneDto>> GetPhoneById([FromQuery] int Id)

        {
            SharedResponse<PhoneDto> response = await repo.GetById(Id);
            if (response.status == Status.notFound) return NotFound();
            return response.data;
        }
        [HttpPost]
        public async Task<ActionResult<PhoneDto>> PostPhone(PhoneDto Phone)
        {
            SharedResponse<PhoneDto> response = await repo.Create(Phone);
            if (response.status == Status.badRequest) return BadRequest(response.message);
            if (response.status == Status.problem) return Problem(response.message);
            return Ok(response.data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PhoneDto>> PutPhone([FromQuery] int id, PhoneDto Phone)
        {
            SharedResponse<PhoneDto> response = await repo.Update(id, Phone);
            if (response.status == Status.badRequest) return BadRequest();
            else if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PhoneDto>> DeletePhone([FromQuery] int id)
        {
            SharedResponse<PhoneDto> response = await repo.Delete(id);
            if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
    }
}