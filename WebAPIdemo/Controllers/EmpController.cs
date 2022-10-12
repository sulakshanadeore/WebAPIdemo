using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIdemo.Models;
using HelperLibrary;
using DisconnectedBLL;
namespace WebAPIdemo.Controllers
{
   // [RoutePrefix("MEmp")]
    public class EmpController : ApiController
    {
        // GET api/<controller>
        EmployeeHelper obj = null;
        public EmpController()
        {
         obj = new EmployeeHelper();
        }

        [Route("GetAllEmps")]
        public List<Employees> GetEmpList()
        {
            //return new string[] { "value1", "value2" };
            
            List<Employee_BAL> empbal = new List<Employee_BAL>(); empbal=obj.EmployeeList();
            List<Employees> emps = new List<Employees>();
            foreach (var item in empbal)
            {
                //Employees emp = new Employees();
                emps.Add(new Employees { EmployeeID = item.EmployeeID, FirstName = item.FirstName, LastName = item.LastName, BirthDate = item.BirthDate, Title = item.Title });
            }
            return emps;

        }

        [Route("FindEmp")]
        public Employees GetEmployeeDetails()
        {
            return new Employees
            {


                EmployeeID = 1,
                LastName = "Davolio",
                FirstName = "Nancy",
                Title = "Sales Representative",
                BirthDate = new DateTime(1948, 08, 12)
            };
            }
        // GET api/<controller>/5
        [Route("FindEmp/{id}")]
        public Employees GetEmpByID(int id)
        {
            Employee_BAL empbal = new Employee_BAL();
            empbal=obj.LocateEmployee(id);
            Employees emp = new Employees();
            emp.EmployeeID = empbal.EmployeeID;
            emp.FirstName = empbal.FirstName;
            emp.LastName = empbal.LastName;
            emp.Title = empbal.Title;
            emp.BirthDate = empbal.BirthDate;
            return emp;
            //return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage PostProduct([FromBody] Employees empdata)
        {
            Employee_BAL empbal = new Employee_BAL();
            empbal.EmployeeID = empdata.EmployeeID;
            empbal.FirstName = empdata.FirstName;
            empbal.LastName = empdata.LastName;
            empbal.Title = empdata.Title;
            empbal.BirthDate = empdata.BirthDate;


            bool ans=obj.AddNewEmployee(empbal);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            
        }

        // PUT api/<controller>/5
        public HttpResponseMessage PutProduct(int id, [FromBody] Employees empdata)
        {

            Employee_BAL empbal = new Employee_BAL();
            empbal.EmployeeID = empdata.EmployeeID;
            empbal.FirstName = empdata.FirstName;
            empbal.LastName = empdata.LastName;
            empbal.Title = empdata.Title;
            empbal.BirthDate = empdata.BirthDate;

           bool ans= obj.EditEmployeeData(id, empbal);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteProduct(int id)
        {
            bool ans=obj.RemoveEmployeeData(id);
            if (ans)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }
    }
}