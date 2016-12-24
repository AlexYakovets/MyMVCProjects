using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyMeetings.Models;
using Newtonsoft.Json;

namespace MyMeetings.API
{
    public class UsersController : ApiController
    {
        ApplicationDbContext DBContext = new ApplicationDbContext();
        // GET api/<controller>
        public string Get()
        {
            return JsonConvert.SerializeObject(DBContext.Users);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}