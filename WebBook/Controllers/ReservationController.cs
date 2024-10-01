using WebBook.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.Controllers
{
    public class ReservationController : Controller
    {

        public IActionResult Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            List<Book> bookList = new List<Book>();

            using (var httpClient = new HttpClient())
            {
             
                using (var response = httpClient.GetAsync("https://localhost:7186/api/Reservations"))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var section = response.Result.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<List<Reservation>>(section.Result);
                    }
                }

        
                using (var response = httpClient.GetAsync("https://localhost:7186/api/Books"))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var section = response.Result.Content.ReadAsStringAsync();
                        bookList = JsonConvert.DeserializeObject<List<Book>>(section.Result);
                    }
                }
            }

         
            ViewBag.Books = bookList.ToDictionary(b => b.Id, b => b.Book_Name);
            return View(reservationList);
        }
        public async Task<IActionResult> Create()
        {
           
            List<Book> books = new List<Book>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7186/api/Books"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var section = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<List<Book>>(section);
                    }
                }
            }

            ViewBag.Books = new SelectList(books, "Id", "Book_Name");
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Reservation test)
        {
            Reservation model;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7186/api/Reservations", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            if (model != null)
            {
                return RedirectToAction(nameof(Index));
            }

         
            List<Book> books = new List<Book>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7186/api/Books"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var section = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<List<Book>>(section);
                    }
                }
            }

            ViewBag.Books = new SelectList(books, "Id", "Book_Name", test.Book_Id);
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            Reservation reservationData = new Reservation();
            List<Book> bookList = new List<Book>();

            using (var httpClient = new HttpClient())
            {
             
                using (var response = await httpClient.GetAsync($"https://localhost:7186/api/Reservations/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var section = await response.Content.ReadAsStringAsync();
                        reservationData = JsonConvert.DeserializeObject<Reservation>(section);
                    }
                }

            
                using (var response = await httpClient.GetAsync("https://localhost:7186/api/Books"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var section = await response.Content.ReadAsStringAsync();
                        bookList = JsonConvert.DeserializeObject<List<Book>>(section);
                    }
                }
            }

           
            ViewBag.Books = new SelectList(bookList, "Id", "Book_Name", reservationData.Book_Id);
            return View(reservationData);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Reservation test)
        {
            Reservation model;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync("https://localhost:7186/api/Reservations/" + id, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Reservation>(apiResponse.Result);

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
            Reservation dataList = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.DeleteAsync("https://localhost:7186/api/Reservations/" + id))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                     
                        return RedirectToAction(nameof(Index));
               
                    }
                }

            }
            return View(dataList);
        }


    }
}
