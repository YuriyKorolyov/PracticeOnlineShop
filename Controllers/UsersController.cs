using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto.Update;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Interfaces;
using MyApp.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
    {
        var userDtos = await _userRepository.GetAll()
            .Include(u => u.Role)
            .ProjectTo<UserReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int userId)
    {
        if (! await _userRepository.Exists(userId))
            return NotFound();

        var userDto = _mapper.Map<UserReadDto>(await _userRepository.GetById(userId, query =>
        query.Include(u => u.Role)));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> PostUser([FromBody] UserCreateDto userDto)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = _mapper.Map<User>(userDto);
        user.Role = await _roleRepository.GetById(userDto.RoleId);

        if (! await _userRepository.Add(user))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        var createdUserDto = _mapper.Map<UserReadDto>(user);
        return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> PutUser(int userId, [FromBody] UserUpdateDto userDto)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (userId != userDto.Id)
            return BadRequest(ModelState);

        if (! await _userRepository.Exists(userId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();
        
        var user = await _userRepository.GetById(userId);
        user.Role = await _roleRepository.GetById(userDto.RoleId);
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.ShippingAddress = userDto.ShippingAddress;

        if (! await _userRepository.Update(user))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        if (! await _userRepository.Exists(userId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (! await _userRepository.DeleteById(userId))
        {
            ModelState.AddModelError("", "Something went wrong deleting user");
        }

        return NoContent();
    }
}
