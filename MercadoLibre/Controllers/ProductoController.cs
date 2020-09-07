using System.IO;
using System.Linq;
using MercadoLibre.DB;
using MercadoLibre.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace MercadoLibre.Controllers
{
    public class ProductoController : Controller
    {
        
        
        private IWebHostEnvironment env;

        public ProductoController(IWebHostEnvironment env)
        {
            this.env = env;
            
        }
        public IActionResult Index()
        {
            var contex = new MercadoAppContex();
           var product = contex.Productos.ToList();
            return View(product);
        }
        [HttpGet ]
        public IActionResult Crear()
        {
          //  var contex = new MercadoAppContex();

            //ViewBag.Productos = contex.Productos.ToList();
            return View(new Producto());
        }
        [HttpPost]
        public IActionResult Crear(Producto producto,IFormFile imagen)
        {
              var contex = new MercadoAppContex();
            // if (ModelState.IsValid)
            //  {
            //       
            //     if (imagen.Length > 0)
            //     {
            //         var filePath = Path.Combine(env.ContentRootPath, "imagen", imagen.FileName);

            //         using (var stream = new FileStream(filePath, FileMode.Create))
            //         {
            //             imagen.CopyTo(stream);
            //       }
            //     }

            //  producto.imagen = imagen.FileName;
            //  contex.Productos.Add(producto);
            //  contex.SaveChanges();


            //     return RedirectToAction("Index");
            // }


            var filePath = Path.Combine(env.WebRootPath,"imagen",imagen.FileName);

            using (var Stream = new FileStream(filePath, FileMode.Create)) 
            {
                imagen.CopyTo(Stream);
            }

            producto.imagen = imagen.FileName;
            contex.Productos.Add(producto);
              contex.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Detalle(int id)
        {
            var contex = new MercadoAppContex();
            var lista = contex.Productos.Where(o=>o.id==id).ToList();

            return View(lista);
        }
    }
}