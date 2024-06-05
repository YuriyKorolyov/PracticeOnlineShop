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
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers(CancellationToken cancellationToken)
    {
        var userDtos = await _userRepository.GetAll()
            .Include(u => u.Role)
            .ProjectTo<UserReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int userId, CancellationToken cancellationToken)
    {
        if (! await _userRepository.Exists(userId, cancellationToken))
            return NotFound();

        var userDto = _mapper.Map<UserReadDto>(await _userRepository.GetById(userId, query =>
        query.Include(u => u.Role),
        cancellationToken));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> PostUser([FromBody] UserCreateDto userDto, CancellationToken cancellationToken)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = _mapper.Map<User>(userDto);
        user.Role = await _roleRepository.GetById(userDto.RoleId, cancellationToken);

        if (! await _userRepository.Add(user, cancellationToken))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        var createdUserDto = _mapper.Map<UserReadDto>(user);
        return CreatedAtAction(nameof(GetUser), new { userId = createdUserDto.Id }, createdUserDto);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> PutUser(int userId, [FromBody] UserUpdateDto userDto, CancellationToken cancellationToken)
    {
        if (userDto == null)
            return BadRequest(ModelState);

        if (userId != userDto.Id)
            return BadRequest(ModelState);

        if (! await _userRepository.Exists(userId, cancellationToken))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();
        
        var user = await _userRepository.GetById(userId, cancellationToken);
        user.Role = await _roleRepository.GetById(userDto.RoleId, cancellationToken);
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.ShippingAddress = userDto.ShippingAddress;

        if (! await _userRepository.Update(user, cancellationToken))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId, CancellationToken cancellationToken)
    {
        if (! await _userRepository.Exists(userId, cancellationToken))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (! await _userRepository.DeleteById(userId, cancellationToken))
        {
            ModelState.AddModelError("", "Something went wrong deleting user");
        }

        return NoContent();
    }
}
