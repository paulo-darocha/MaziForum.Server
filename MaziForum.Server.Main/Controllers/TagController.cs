using MaziForum.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MaziForum.Server.Main.Controllers
{
   [ApiController]
   [Route("api/tags")]
   public class TagController : ControllerBase
   {
      private readonly ForumDbContext dbContext;

      public TagController(ForumDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      [HttpGet]
      public async Task<IActionResult> GetTags()
      {
         var tags = await dbContext.Tags.ToListAsync();
         if (tags.Count == 0)
         {
            return NotFound();
         }

         return Ok(tags);
      }
   }
}
