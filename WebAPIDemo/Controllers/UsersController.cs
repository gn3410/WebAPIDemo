using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPIDemo.Models;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace WebAPIDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] //啟用跨原始來源資源分享(CORS)
    public class UsersController : ApiController
    {
        /// <summary>
        /// 取得使用者列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Users")]
        [AllowAnonymous]
        [ResponseType(typeof(IEnumerable<Users>))]
        public async Task<IHttpActionResult> GetUsers()
        {
            using (API_DemoEntities db = new API_DemoEntities()) //開啟SQL連線
            {
                IEnumerable<Users> model = await db.Users.OrderBy(m => m.UserName).ToListAsync();
                if (model == null) return NotFound();
                return Ok(model);
            }
        }

        /// <summary>
        /// 取得單一使用者資料
        /// </summary>
        /// <param name="id">使用者id</param>
        /// <returns></returns>
        /// http://localhost:60063/api/Users?id=1
        [HttpGet]
        [Route("api/Users")]
        [AllowAnonymous]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            using (API_DemoEntities db = new API_DemoEntities())
            {
                Users model = null;
                try
                {
                    model = await db.Users.Where(m => m.ID == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                if (model == null) return NotFound();
                return Ok(model);
            }
        }

        /// <summary>
        /// 新增一個使用者記錄
        /// </summary>
        /// <param name="model">使用者結構及資料</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Users")]
        [AllowAnonymous]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> Post(Users model)
        {
            using (API_DemoEntities db = new API_DemoEntities())
            {
                try
                {
                    db.Users.Add(model);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
        }

        /// <summary>
        /// 修改一個使用者記錄
        /// </summary>
        /// <param name="model">使用者結構及資料</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Users")]
        [AllowAnonymous]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> Put(Users model)
        {
            using (API_DemoEntities db = new API_DemoEntities())
            {
                try
                {
                    var user = db.Users.Where(m => m.ID == model.ID).FirstOrDefault();
                    if (user == null) return NotFound();

                    user.UserName = model.UserName;
                    user.UserPwd = model.UserPwd;
                    user.UserEmail = model.UserEmail;
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
        }

        /// <summary>
        /// 刪除一個使用者記錄
        /// </summary>
        /// <param name="id">使用者id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Users")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var db = new API_DemoEntities())
            {
                try
                {
                    var user = db.Users.Where(m => m.ID == id).FirstOrDefault();
                    if (user == null) return NotFound();
                    //db.Entry(user).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
        }
    }
}
