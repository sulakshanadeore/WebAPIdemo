using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIdemo.Models;

namespace WebAPIdemo.Controllers
{
    public class StudentController : ApiController
    {
        // GET
        // api/<controller>
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id,string name)
        {
            return "Studid=" + id +  "     studname=" + name;
        }

        // POST api/<controller>
        //public void Post(int id,string name)
        //{
        //}
        
        public SchoolStudent Post([FromBody]SchoolStudent student,[FromUri] int age=9)
        {
            return student;
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}