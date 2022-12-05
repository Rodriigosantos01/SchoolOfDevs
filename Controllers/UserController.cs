using Microsoft.AspNetCore.Mvc;
using SchollOfDevs.Dto.User;
using SchollOfDevs.Services;
using SchoolOfDevs.Authorization;

namespace SchollOfDevs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request) => Ok(await _service.Authenticate(request));
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest user) => Ok(await _service.Create(user));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserRequestUpdate userIn, int id)
        {
            await _service.Update(userIn, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
