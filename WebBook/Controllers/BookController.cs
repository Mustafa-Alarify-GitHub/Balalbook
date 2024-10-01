using WebBook.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace WebBook.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            List<Book> dataList = new List<Book>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:7186/api/Books"))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var section = response.Result.Content.ReadAsStringAsync();
                        dataList = JsonConvert.DeserializeObject<List<Book>>(section.Result);
                    }
                }
            }
            return View(dataList);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Book test)
        {
            Book model;


            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7186/api/Books", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(apiResponse); 

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Errorثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثثث: {response.StatusCode}, Response: {apiResponse}");
                        return View("Error");
                    }

                    model = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }

            if (model != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Book dataList = new Book();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://localhost:7186/api/Books/" + id))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var section = response.Result.Content.ReadAsStringAsync();
                        dataList = JsonConvert.DeserializeObject<Book>(section.Result);
                    }
                }

            }
            return View(dataList);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Book test)
        {
            Book model;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync("https://localhost:7186/api/Books/" + id, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Book>(apiResponse.Result);

                }
            }
            if (model != null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            Book dataList = new Book();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.DeleteAsync("https://localhost:7186/api/Books/" + id))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.Result);
                        return RedirectToAction(nameof(Index));
                     
                    }
                }

            }
            return View(dataList);
        }
    }
}
