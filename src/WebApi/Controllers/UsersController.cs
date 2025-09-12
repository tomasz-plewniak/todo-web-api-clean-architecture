using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [ActionName(nameof(GetUsersAsync))]
    [ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
    public async Task<IActionResult>  GetUsersAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<User> users = await userService.GetUsersAsync(cancellationToken);
        
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetUserAsync))]   
    [ProducesResponseType<User>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        User? user = await userService.GetUserAsync(id, cancellationToken);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpPost]
    [ActionName(nameof(CreateUserAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser createUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        User user = await userService.CreateUserAsync(createUser);
        
        
        
        return CreatedAtAction(nameof(GetUserAsync), new {id = user.Id}, user);   
    }

    [HttpPatch("{id}")]
    [ActionName(nameof(UpdateUserAsync))]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateUser updateUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        User? user = await userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        
        user.UserName = updateUser.UserName;
        user.Email = updateUser.Email;
        
        await userService.UpdateUserAsync(user);
        
        return Ok(user); 
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(
        [FromRoute] Guid id)
    {
        User? user = await userService.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await userService.DeleteUserAsync(user);
        return NoContent();
    }
}