using MaziForum.Server.Data.Entities;
using MaziForum.Server.Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaziForum.Server.Main.Controllers
{
   [Route("api/users")]
   public class UsersController : Controller
   {
      private readonly UserManager<User> _userManager;

      public UsersController(UserManager<User> userManager)
      {
         _userManager = userManager;
      }

      [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
      {
         if (ModelState.IsValid)
         {
            User user = new User
            {
               NickName = model.NickName,
               UserName = model.Email,
               Email = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
               result = await _userManager.AddPasswordAsync(user, model.Password);
               if (result.Succeeded)
               {
                  var data = new CreateUserResult
                  {
                     Succeeded = true,
                     Message = new List<string> { { "User created successfully." } }
                  };
                  return Json(data);
               }
               else
               {
                  await _userManager.DeleteAsync(user);
               }
            }
            else
            {
               var errors = new List<string>();
               foreach (var err in result.Errors)
               {
                  errors.Add(err.Description);
               }
               var data = new CreateUserResult { Succeeded = false, Message = errors };

               return Json(data);
            }
         }

         return BadRequest(ModelState);
      }
   }
}
