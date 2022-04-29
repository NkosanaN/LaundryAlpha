using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SandSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoryController : ControllerBase
    {

        private readonly IUnityOfWork _unityofwork;
        //private readonly IToastNotification _toastNotification;

        public ProjectCategoryController(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
            //_toastNotification = toastNotification;
        }

        // GET: api/<ProjectCategoryController>
        [HttpGet]
        public ActionResult GetProductCategory()
        {
            IEnumerable<ProductCategory> obj = _unityofwork.ProductCategory.GetAll();
            obj ??= new List<ProductCategory>();
            return Ok(obj);
        }

        // GET api/<ProjectCategoryController>/5
        [HttpGet("{id}")]
        public ActionResult GetSingleProductCategory(int id)
        {
            ProductCategory obj = _unityofwork.ProductCategory.GetFirstOrDefault(u => u.ProductCatID == id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        // POST api/<ProjectCategoryController>
        [HttpPost]
        public ActionResult Post(ProductCategory model)
        {
            _unityofwork.ProductCategory.Add(model);
            _unityofwork.Save();
            return CreatedAtAction("GetProductCategory", new { id = model.ProductCatID }, model);
        }

        // PUT api/<ProjectCategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, ProductCategory model)
        {
            if (id != model.ProductCatID)
            {
                return BadRequest();
            }
            _unityofwork.ProductCategory.Update(model);
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
           var type =  _unityofwork.ProductCategory.GetFirstOrDefault(u => u.ProductCatID == id);

            if (type == null)
            {
                return NotFound();
            }

            _unityofwork.ProductCategory.Delete(type);
            _unityofwork.Save();

            return NoContent();
        }
    }
}
