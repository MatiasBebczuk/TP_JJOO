using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_JJOO.Models;

namespace TP_JJOO.Controllers;

public class HomeController : Controller

    {
        // Acción para mostrar la página de inicio
        public IActionResult Index()
        {
            return View();
        }

        // Acción para mostrar la lista de deportes
        public IActionResult Deportes()
        {
            List<Deporte> deportes = BD.ListarDeportes();
            ViewBag.Deportes = deportes;
            return View();
        }

        // Acción para mostrar la lista de países
        public IActionResult Paises()
        {
            List<Pais> paises = BD.ListarPaises();
            ViewBag.Paises = paises;
            return View();
        }

        // Acción para mostrar detalles de un deporte específico
        public IActionResult VerDetalleDeporte(int idDeporte)
        {
            var deporte = BD.VerInfoDeporte(idDeporte);
            if (deporte == null)
            {
                return NotFound(); // Manejar el caso en que el deporte no se encuentra
            }

            var deportistas = BD.ListarDeportistas(idDeporte);

            ViewBag.Deporte = deporte;
            ViewBag.Deportistas = deportistas;
            return View();
        }

        // Acción para mostrar detalles de un país específico
        public IActionResult VerDetallePais(int idPais)
        {
            var pais = BD.VerInfoPais(idPais);
            if (pais == null)
            {
                return NotFound(); // Manejar el caso en que el país no se encuentra
            }

            var deportistas = BD.ListarDeportistasPorPais(idPais);

            ViewBag.Pais = pais;
            ViewBag.Deportistas = deportistas;
            return View();
        }

        // Acción para mostrar detalles de un deportista específico
        public IActionResult VerDetalleDeportista(int idDeportista)
        {
            var deportista = BD.VerInfoDeportista(idDeportista);
            if (deportista == null)
            {
                return NotFound(); // Manejar el caso en que el deportista no se encuentra
            }

            ViewBag.Deportista = deportista;
            return View();
        }

        // Acción para mostrar el formulario para agregar un nuevo deportista
        [HttpGet]
        public IActionResult AgregarDeportista()
        {
            List<Deporte> deportes = BD.ListarDeportes();
            List<Pais> paises = BD.ListarPaises();
            ViewBag.Deportes = deportes;
            ViewBag.Paises = paises;
            return View();
        }

        // Acción para manejar el envío del formulario de agregar un deportista
        [HttpPost]
        public IActionResult GuardarDeportista(string Nombre, string Apellido, int IdPais, int IdDeporte, string Foto, DateTime FechaNacimiento)
        {
            Deportista dep = new Deportista{
                Nombre = Nombre,
                Apellido = Apellido,
                IdPais = IdPais,
                IdDeporte = IdDeporte,
                Imagen = Foto,
                FechaNacimiento = FechaNacimiento
            };

            if (ModelState.IsValid)
            {
                BD.AgregarDeportista(dep);
                return RedirectToAction("Index");
            }

            // Si la validación falla, recargar los datos y volver al formulario
            List<Deporte> deportes = BD.ListarDeportes();
            List<Pais> paises = BD.ListarPaises();
            ViewBag.Deportes = deportes;
            ViewBag.Paises = paises;
            return View("AgregarDeportista", dep);
        }

        // eliminar un deportista
       public IActionResult EliminarDeportista(int idCandidato)
    {
        BD.EliminarDeportista(idCandidato);
        return View();
    }

        // Acción para mostrar los créditos
        public IActionResult Creditos()
        {
            return View();
        }
}
