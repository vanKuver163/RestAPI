using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly ShopContext _db;
        public PagesController(ShopContext db)
        {
            _db = db;
        }

        //GET /api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await _db.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }

        //GET /api/pages/id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var page =  await _db.Pages.FindAsync(id);
            if (page == null) return NotFound();
            return page;
        }

        //Put /api/pages/id
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Page>> PutPage(int id, Page page)
        {
            if (id != page.Id) return BadRequest();

            _db.Entry(page).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();           
        }

        //Post /api/pages
        [HttpPost]       
        public async Task<ActionResult<Page>> PostPage(Page page)
        {

            _db.Pages.Add(page);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(PostPage), page);
        }

        //Delete /api/pages/id
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Page>> DeletePage(int id)
        {
            var page = await _db.Pages.FindAsync(id);
            if (page == null) return NotFound();
            _db.Pages.Remove(page);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
