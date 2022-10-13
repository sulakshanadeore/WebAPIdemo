using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApiClientDemo.Models;
using System.Net.Http.Formatting;
namespace WebApiClientDemo.Controllers
{
    public class EmployeeClientController : Controller
    {
        // GET: EmployeeClient
        public ActionResult Index()
        {
            List<Employees> emplist = new List<Employees>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44374/api/");

                var responseTask = client.GetAsync("Emp");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<Employees[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new Employees {EmployeeID=item.EmployeeID,FirstName=item.FirstName,LastName=item.LastName,BirthDate=item.BirthDate,Title=item.Title });

                    }
                }

            }
            

            return View(emplist);
        }


        public ActionResult AddNewEmployee()
        {

            return View();
        }
        [HttpPost]
        
        public ActionResult AddNewEmployee(Employees empmodel)
        {

            //if (ModelState.IsValid)
            //{
                using (var client = new HttpClient())
                {
                client.BaseAddress = new Uri("https://localhost:44374/api/Emp");

                var emp = new Employees {FirstName=empmodel.FirstName,LastName=empmodel.LastName,Title=empmodel.Title,BirthDate=empmodel.BirthDate };

              var postTask=  client.PostAsJsonAsync<Employees>(client.BaseAddress, emp);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<Employees>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                }


            }
            
            return RedirectToAction("Index");
        }
    }
}