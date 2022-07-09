using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryRepository = new CategoryRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetCategories(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var categories = _categoryRepository.GetCategories(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if(category == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            
            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCategory(CategoryModel model)
        {
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
            };
            var response = _categoryRepository.AddCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);
            

            return Ok(categoryModel);
        }


        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
            };
            var response = _categoryRepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);


            return Ok(categoryModel);
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            bool response = _categoryRepository.DeleteCategory(id);


            return Ok(response);
        }
    }
}
