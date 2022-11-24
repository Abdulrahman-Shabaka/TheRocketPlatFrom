using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheRocket.Dtos.UserDtos;
using TheRocket.Dtos.ProductDtos;
using TheRocket.Entities.Users;
using TheRocket.Repositories.RepoInterfaces;
using TheRocket.Shared;

namespace TheRocket.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize(Roles="Admin,Seller")]

    public class SizeController : ControllerBase
    {
        private readonly ISizeRepo repo;
        public SizeController(ISizeRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<SizeDto>> AllSizes(){
            SharedResponse<List<SizeDto>> response=await repo.GetAll();
            if(response.status==Status.notFound)return NotFound();
            return Ok(response.data);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<SizeDto>> GetSizeById([FromQuery]int id){
            SharedResponse<SizeDto> response=await repo.GetById(id);
            if(response.status==Status.notFound)return NotFound();
            return Ok(response.data);
        }

        [HttpPost]
        public async Task<ActionResult<SizeDto>> PostSize(SizeDto Size){
            SharedResponse<SizeDto> response=await repo.Create(Size);
            if(response.status==Status.problem)return Problem(response.message);
            if(response.status==Status.badRequest) return BadRequest(response.message);
            if(response.status==Status.found)return Ok(response);
            return Ok(response.data);
        }

        [HttpPut]

        [Authorize(Roles ="Admin")]

        public async Task<ActionResult<SizeDto>> PutSize([FromQuery]int id,SizeDto Size){
            SharedResponse<SizeDto> response=await repo.Update(id,Size);
            if(response.status==Status.badRequest)return BadRequest();
            else if(response.status==Status.notFound)return NotFound();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]

         public async Task<ActionResult<SizeDto>> DeleteSize([FromQuery]int id){
            SharedResponse<SizeDto> response=await repo.Delete(id);
            if(response.status==Status.notFound)return NotFound();
            return NoContent();
         } 
    }
}