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
    public class EmpController : ApiController
    {
        // GET api/<controller>
        EmployeeHelper obj = null;
        public EmpController()
        {
         obj = new EmployeeHelper();
        }

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

        // GET api/<controller>/5
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