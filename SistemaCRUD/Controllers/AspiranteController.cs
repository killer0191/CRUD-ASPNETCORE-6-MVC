using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using SistemaCRUD.Models;
using System.Text;

namespace SistemaCRUD.Controllers
{
    public class AspiranteController : Controller
    {
        private readonly HttpClient _httpClient;
        public AspiranteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api-aspirantesweb.igrtecapi.site/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Aspirantes");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<Aspirante>>(content);
                return View("Index", productos);
            }
            else
            {
                return BadRequest(response);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        /*public IActionResult CreateForm()
        {
            return PartialView("CreateForm");
        }*/

        [HttpPost]
        public async Task<IActionResult> Create(Aspirante producto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Aspirantes", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el producto.");
                }
            }
            return View(producto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            return await Delete(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Aspirantes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el producto.";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit()
        {
            return View();
        }
        /*public IActionResult EditForm()
{
    return PartialView("EditForm");
}*/

        [HttpPost]
        public async Task<IActionResult> GetEdit(int id)
        {
            var response = await _httpClient.PostAsync($"api/Aspirantes/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var aspirante = JsonConvert.DeserializeObject<Aspirante>(content);
                return View("Edit", aspirante);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Aspirante aspirante)
        {

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(aspirante), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/Aspirantes/", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar el aspirante.");
                }
            }

            return View(aspirante);
        }
    }
}