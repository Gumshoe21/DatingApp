using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

	[ApiController]
	[Route("api/[controller]")] // takes the first part of the name of the controller (users) and uses that as the route - /api/users - if nothing else is added to the route pattern besides /api/users, it will look for a HTTP GET method at this route and exec it
	public class BaseApiController : ControllerBase
	{

	}

}