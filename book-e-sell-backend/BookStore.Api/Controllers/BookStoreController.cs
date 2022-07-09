using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/public")]
    public class BookStoreController : Controller
    {
        UserRepository _repository = new UserRepository();

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            User user = _repository.Login(model);
           
            if (user == null)
            {
                return BadRequest("Invalid Email or password!!!");
            }
            UserModel userModel = new UserModel(user);
            return Ok(userModel);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            User user = _repository.Register(model);
            if (user == null)
            {
                return BadRequest("Invalid user!!!");
            }
            return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
        }
    }
}
