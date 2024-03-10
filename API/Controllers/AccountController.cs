using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

	public class AccountController : BaseApiController
	{
		private readonly DataContext _context;
		private readonly ITokenService _tokenService;

		public AccountController(DataContext context, ITokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}

		[HttpPost("register")] // POST: api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{

			if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
			using var hmac = new HMACSHA512(); // using to dispose of this class automatically when were done with it, if a calss implmenets IDispoable it must implement a Dispose method - when we're done with this calss, it will call dispose
			var user = new AppUser
			{
				UserName = registerDto.Username.ToLower(), // can be null
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key
			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return new UserDto
			{
				Username = user.UserName,
				Token = _tokenService.CreateToken(user)
			};

		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

			if (user == null) return Unauthorized("invalid username");

			using var hmac = new HMACSHA512(user.PasswordSalt); // returns Byte array
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
			}
			return new UserDto
			{
				Username = user.UserName,
				Token = _tokenService.CreateToken(user)
			};
		}
		private async Task<bool> UserExists(string username)
		{
			return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
		}
	}

}