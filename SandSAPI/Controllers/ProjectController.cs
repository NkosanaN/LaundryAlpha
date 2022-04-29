using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SandSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IUnityOfWork _unityofwork;
        //private readonly IToastNotification _toastNotification;

        public ProjectController(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
            //_toastNotification = toastNotification;
        }

        // GET: api/<ProjectCategoryController>
        [HttpGet]
        public ActionResult GetProduct()
        {
            IEnumerable<Product> obj = _unityofwork.Product.GetAll();
            obj ??= new List<Product>();
            return Ok(obj);
        }

        // GET api/<ProjectCategoryController>/5
        [HttpGet("{id}")]
        public ActionResult GetSingleProductCategory(int id)
        {
            Product obj = _unityofwork.Product.GetFirstOrDefault(u => u.ProductID == id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        // POST api/<ProjectCategoryController>
        [HttpPost]
        public ActionResult Post(Product model)
        {
            _unityofwork.Product.Add(model);
            _unityofwork.Save();
            return CreatedAtAction("GetProduct", new { id = model.ProductID }, model);
        }

        // PUT api/<ProjectCategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Product model)
        {
            if (id != model.ProductID)
            {
                return BadRequest();
            }
            _unityofwork.Product.Update(model);
            try
            {
                _unityofwork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return NoContent();
        }

        // DELETE api/<ProjectCategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           var type =  _unityofwork.Product.GetFirstOrDefault(u => u.ProductID == id);

            if (type == null)
            {
                return NotFound();
            }

            _unityofwork.Product.Delete(type);
            _unityofwork.Save();

            return NoContent();
        }
    }
}
