using ApiProductos.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors;

namespace ApiProductos.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        public readonly DBAPIContext _dbcontext;


        public ProductoController(DBAPIContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet]
        [Route("lista")]
        public IActionResult listaProductos() {

            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", reponse = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }



        [HttpGet]
        [Route("obtener/{idProducto:int}")]
        public IActionResult obtenerProducto(int idProducto)
        {

            Producto producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                producto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", reponse = producto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = producto });
            }

        }


        [HttpPost]
        [Route("guardar")]

        public IActionResult Guardar([FromBody] Producto producto)
        {
            try
            {
                _dbcontext.Productos.Add(producto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }


        [HttpPut]
        [Route("editar")]

        public IActionResult Editar([FromBody] Producto producto)
        {

            Producto productoEdit = _dbcontext.Productos.Find(producto.IdProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {

                productoEdit.CodigoBarra = producto.CodigoBarra is null ? productoEdit.CodigoBarra : productoEdit.CodigoBarra;
                productoEdit.Descripcion = producto.Descripcion is null ? productoEdit.Descripcion : productoEdit.Descripcion;
                productoEdit.Marca = producto.Marca is null ? productoEdit.Marca : productoEdit.Marca;
                productoEdit.IdCategoria = producto.IdCategoria is null ? productoEdit.IdCategoria : productoEdit.IdCategoria;
                productoEdit.Precio = producto.Precio is null ? productoEdit.Precio : productoEdit.Precio;


                _dbcontext.Productos.Update(productoEdit);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }


        [HttpDelete]
        [Route("eliminar/{idProducto:int}")]
        public IActionResult Eliminar (int idProducto)
        {

            Producto producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {

          


                _dbcontext.Productos.Remove(producto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }


        }
    }
}
