using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController : BaseApiController
{
	private readonly DataContext _context; // this will give us the ability to use this field in the rest of our methods
										   // we now need access to our db so we can quewry ther users and return them from our API dcontroller, so we'll use a constructor
										   // the context is INJECTED inside the UsersController when it's created
										   // this DataContext object is SCOPED to the HTTP request itself - once the controller's done doing its work, the context will be disposed of
	public UsersController(DataContext context)
	{
		this._context = context;
	}

	// a task represents an asynchronous operation that can return a value.
	[HttpGet]
	public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
	{
		var users = await _context.Users.ToListAsync();
		return users;
	}

	[HttpGet("{id}")] // /api/users/2
	public async Task<ActionResult<AppUser>> GetUser(int id)
	{
		return await _context.Users.FindAsync(id);
	}
}

