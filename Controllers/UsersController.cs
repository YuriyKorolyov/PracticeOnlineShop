using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetUsersAsync());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        if (! await _userRepository.UserExistsAsync(userId))
            return NotFound();

        var userDto = _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser([FromQuery] int roleId, [FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = _mapper.Map<User>(userDto);
        user.Role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (! await _userRepository.AddUserAsync(user))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        var createdUserDto = _mapper.Map<UserDto>(user);
        return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> PutUser(int userId, [FromBody] UserDto userDto)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (userId != userDto.Id)
            return BadRequest(ModelState);

        if (! await _userRepository.UserExistsAsync(userId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var user = _mapper.Map<User>(userDto);

        if (! await _userRepository.UpdateUserAsync(user))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        if (! await _userRepository.UserExistsAsync(userId))
        {
            return NotFound();
        }

        var userToDelete = await _userRepository.GetUserByIdAsync(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (! await _userRepository.DeleteUserAsync(userToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting user");
        }

        return NoContent();
    }
}
