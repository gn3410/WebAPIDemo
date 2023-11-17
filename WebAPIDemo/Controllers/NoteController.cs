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
        [ResponseType(typeof(IEnumerable<Note>))]
        public async Task<IHttpActionResult> GetUsers()
        {
            using (API_DemoEntities db = new API_DemoEntities()) //開啟SQL連線
            {
                IEnumerable<Note> model = await db.Note.OrderBy(m => m.NoteID).ToListAsync();
                if (model == null) return NotFound();
                return Ok(model);
            }
        }

        /// <summary>
        /// 取得單一筆記資料
        /// </summary>
        /// <param name="NoteID">筆記ID</param>
        /// <returns></returns>
        /// http://localhost:60063/api/Notes?id=1
        [HttpGet]
        [Route("api/Notes")]
        [AllowAnonymous]
        [ResponseType(typeof(Note))]
        public async Task<IHttpActionResult> GetUser(int NoteID)
        {
            using (API_DemoEntities db = new API_DemoEntities())
            {
                Note model = null;
                try
                {
                    model = await db.Note.Where(m => m.NoteID == NoteID).FirstOrDefaultAsync();
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
