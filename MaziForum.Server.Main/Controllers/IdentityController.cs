using MaziForum.Server.Data.Entities;
using MaziForum.Server.Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MaziForum.Server.Main.Controllers
{
   [ApiController]
   [Route("api/identity")]
   public class IdentityController : ControllerBase
   {
      private readonly SignInManager<User> _signInManager;
      private readonly UserManager<User> _userManager;
      private readonly IConfiguration _configuration;

      public IdentityController(
         SignInManager<User> signInManager,
         UserManager<User> userManager,
         IConfiguration configuration
      )
      {
         _signInManager = signInManager;
         _userManager = userManager;
         _configuration = configuration;
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] Credentials credentials)
      {
         if (ModelState.IsValid)
         {
            SignInResult result = await _signInManager.PasswordSignInAsync(
               credentials.Email,
               credentials.Password,
               true,
               false
            );

            if (result.Succeeded)
            {
               User user = await _userManager.FindByEmailAsync(credentials.Email);

               SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
               {
                  Subject = (
                     await _signInManager.CreateUserPrincipalAsync(user)
                  ).Identities.First(),
                  Expires = DateTime.Now.AddMinutes(
                     int.Parse(_configuration["BearerTokens:ExpiryMins"])
                  ),
                  SigningCredentials = new SigningCredentials(
                     new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"])
                     ),
                     SecurityAlgorithms.HmacSha256Signature
                  )
               };

               JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
               SecurityToken securityToken = new JwtSecurityTokenHandler().CreateToken(
                  tokenDescriptor
               );

               var loginResult = new LoginResult
               {
                  Id = user.Id,
                  Succeeded = true,
                  Message = "User logged in successfully.",
                  Token = tokenHandler.WriteToken(securityToken),
                  NickName = user.NickName,
                  Email = user.Email
               };

               //var image = await _dbContext.Medias.FirstOrDefaultAsync(
               //   x => x.UserId == user.Id
               //);
               //if (image != null)
               //{
               //   loginResult.ImageUrl = mediaService.GetMediaUrl(image.FileName);
               //}

               return Ok(loginResult);
            }
            else
            {
               var data = new CreateUserResult
               {
                  Succeeded = false,
                  Message = new List<string> { { "Invalid Email or Password" } }
               };

               return Ok(data);
            }
         }
         return Ok(ModelState);
      }

      [HttpPost("logout")]
      public async Task<IActionResult> Logout(string email)
      {
         await _signInManager.SignOutAsync();

         return Ok("User Logged Out Successfully.");
      }
   }
}
