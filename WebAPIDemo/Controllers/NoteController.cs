using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] //啟用跨原始來源資源分享(CORS)
    public class NoteController : ApiController
    {
        [HttpGet]
        [Route("api/Notes")]
        [AllowAnonymous]
        [ResponseType(typeof(IEnumerable<Notes>))]
        public async Task<IHttpActionResult> GetNotes()
        {
            using (API_DemoEntities db = new API_DemoEntities()) //開啟SQL連線
            {
                IEnumerable<Notes> model = await db.Notes.OrderBy(m => m.ID).ToListAsync();
                if (model == null) return NotFound();
                return Ok(model);
            }
        }

        /// <summary>
        /// 取得單一筆記資料
        /// </summary>
        /// <param name="id">筆記ID</param>
        /// <returns></returns>
        /// http://localhost:60063/api/Notes?id=1
        [HttpGet]
        [Route("api/Notes")]
        [AllowAnonymous]
        [ResponseType(typeof(Notes))]
        public async Task<IHttpActionResult> GetNote(int id)
        {
            using (API_DemoEntities db = new API_DemoEntities())
            {
                Notes model = null;
                try
                {
                    model = await db.Notes.Where(m => m.ID == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                if (model == null) return NotFound();
                return Ok(model);
            }
        }

        // POST: api/Note
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Note/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Note/5
        public void Delete(int id)
        {
        }
    }
}
