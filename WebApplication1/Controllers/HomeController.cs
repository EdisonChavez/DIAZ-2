using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController: Controller
    {       
        private readonly ProductContext _producto;

        public ProductController(ProductContext producto)
        {
            _producto = producto;
        }

        public IActionResult Index()
        {
            var productos = _producto.obtener();
            Viewbag.Productos= productos
            return View();
        }

        public IActionResult GuardarProducto(Producto producto)
        {                        
            if(Validacion.ValidarProducto(producto)){
                _producto.Guardar(producto);
                return View("Index");
            }
            return View();
        }

        public IActionResult productosAReponer()
        {
            var productosAReponer = _producto.productosPocoStock();
            Viewbag.productosAReponer= productosAReponer.
            return View();
        }        

        public class Validaciones(){
            public ValidarProducto(Producto producto){
                if(_producto.Where(o =>o.nombre = producto.nombre).FirstOrDefault()){
                    return Error.WithMessage("ya existe un producto igual");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
