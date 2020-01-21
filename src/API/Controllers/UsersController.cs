using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[Controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET
        [Route("me")]
        public async Task<IActionResult> GetCurrentUser([FromServices]IUserRepository userRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var user = await userRepository.GetById(userId);
            
            return Ok(user);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UserModel user, [FromServices]IUserRepository userRepository)
        {
            await userRepository.UpdateUser(id, user.name, user.surname);
            return Ok();
        }

        public class UserModel
        {
            public string name { get; set; }
            public string surname { get; set; }
        }
    }
}