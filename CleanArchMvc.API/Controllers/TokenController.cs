using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace CleanArchMvc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authenticate;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authenticate, IConfiguration configuration)
    {
        _authenticate = authenticate;
        _configuration = configuration;
    }


    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserToken), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<UserToken>> Register([FromBody] LoginModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest(new { message = "Invalid payload. Require email and password." });

        var result = await _authenticate.RegisterUser(model.Email!, model.Password!);
        if (!result) return BadRequest(new { message = "Unable to register user. Check password strength and if user already exists." });

        var token = GenerateToken(model.Email!);
        return Ok(token);
    }




    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserToken), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            return BadRequest(new { message = "Invalid payload. Require email and password." });

        var success = await _authenticate.Authenticate(model.Email!, model.Password!);
        if (!success) return Unauthorized(new { message = "Invalid credentials" });

        var token = GenerateToken(model.Email!);
        return Ok(token);
    }

    private UserToken GenerateToken(string email)
    {
        var jwtSection = _configuration.GetSection("JwtSettings");
        var key = jwtSection.GetValue<string>("Key");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");
        var duration = jwtSection.GetValue<int?>("DurationInMinutes") ?? 60;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, email)
        };

        var expiration = DateTime.UtcNow.AddMinutes(duration);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(tokenDescriptor);

        return new UserToken { Token = tokenString, Expiration = expiration };
    }

}
