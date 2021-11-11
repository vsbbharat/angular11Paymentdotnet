using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PaymentDetailContext _contextUser;

        public UserController(PaymentDetailContext context)
        {
            _contextUser = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {            
            return await _contextUser.Users.ToListAsync();
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAll()
        {
            return await _contextUser.Users.ToListAsync();
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var userDetail = await _contextUser.Users.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> Post(User Userdata)
        {
            _contextUser.Users.Add(Userdata);
            await _contextUser.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = Userdata.UserID }, Userdata);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> Login(User Userdata)
        {
            List<User> list = new List<User>();
            var userlist = from m in _contextUser.Users
                         select m;

            if (!String.IsNullOrEmpty(Userdata.name))
            {
                userlist = userlist.Where(s => s.name!.Contains(Userdata.name));
            }
            if (userlist != null)
            {
                list = userlist.ToList<User>();
            }

            if (list[0].password != Userdata.password)
            {
                return NotFound();
            }

            return list[0];
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(int id, User paymentDetail)
        {
            if (id != paymentDetail.UserID)
            {
                return BadRequest();
            }

            _contextUser.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _contextUser.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _contextUser.Users.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _contextUser.Users.Remove(paymentDetail);
            await _contextUser.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int id)
        {
            return _contextUser.Users.Any(e => e.UserID == id);
        }
    }
}
