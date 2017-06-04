using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                var BaseAddress = "http://" + HttpContext.Request.Host.Value + "/";
                var ApiAddress = "api/Contacts/";
                client.BaseAddress = new Uri(BaseAddress);
                var response = await client.GetAsync(ApiAddress);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            List<Contact> model = JsonConvert.DeserializeObject<List<Contact>>(result);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    var BaseAddress = "http://" + HttpContext.Request.Host.Value + "/";
                    var ApiAddress = "api/Contacts/";
                    client.BaseAddress = new Uri(BaseAddress);
                    string json = JsonConvert.SerializeObject(contact);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(ApiAddress, content);
                }
                return RedirectToAction("Contacts");
            }
            return View(contact);
        }
    }
}