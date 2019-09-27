using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TipezeNuymbaTestWebApp.Models;

namespace TipezeNuymbaTestWebApp.Controllers
{
    public class HomeManagementController : Controller
    {
        // GET: HomeManagement
        public ActionResult Index()
        {
            List<HouseModel> allHouses = new List<HouseModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/TipezeNyumbaHosted/api/");
                Task<HttpResponseMessage> response = client.GetAsync("houses");
                response.Wait();

                HttpResponseMessage result = response.Result;

                // If success received
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HouseModel>>();
                    readTask.Wait();
                    allHouses = readTask.Result.ToList();
                }
                else
                {
                    //Error response received
                    allHouses = new List<HouseModel>();
                    ModelState.AddModelError(string.Empty, "yalakwa !!");
                    ViewBag.Message = "Internal Server Error";
                    allHouses = new List<HouseModel>();
                }
            }
           return View(allHouses);
        }

        // GET: HomeManagement/Details/5
        public ActionResult Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/TipezeNyumbaHosted");
                string urlExtended = "houses/" + id;
                Task<HttpResponseMessage> responseTask = client.GetAsync(urlExtended);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
                else
                {
                    
                }
            }
            return View();
        }

        // GET: HomeManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
