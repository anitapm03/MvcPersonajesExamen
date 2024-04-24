using Microsoft.AspNetCore.Mvc;
using MvcPersonajesExamen.Models;
using MvcPersonajesExamen.Services;

namespace MvcPersonajesExamen.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }
    
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await 
                this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.CrearPersonajeAsync
                (personaje.IdPersonaje, personaje.Nombre,
                personaje.Imagen, personaje.Serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.EditarPersonajeAsync
                (personaje.IdPersonaje, personaje.Nombre,
                personaje.Imagen, personaje.Serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Buscador()
        {
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Buscador(string serie)
        {
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            List<Personaje> personajes = await this.service
                .GetPersonajesSerieAsync(serie);
            return View(personajes);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }
    }
}
