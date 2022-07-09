using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        CartRepository _cartRepository = new CartRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetCartItems(string keyword = "")
        {
            var carts = _cartRepository.GetCartItems(keyword);
            ListResponse<CartModel> listResponse = new ListResponse<CartModel>()
            {
                Results = carts.Results.Select(c => new CartModel(c)),
                TotalRecords = carts.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpGet]
        [Route("list/{userId}")]
        public IActionResult GetUserCartItems(int userId)
        {
            var carts = _cartRepository.GetUserCartItems(userId);
            ListResponse<CartResponse> listResponse = new ListResponse<CartResponse>()
            {
                Results = carts.Results.Select(c => new CartResponse(c)),
                TotalRecords = carts.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = 1,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = _cartRepository.AddCart(cart);

            return Ok(new CartModel(cart));
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = _cartRepository.UpdateCart(cart);

            return Ok(new CartModel(cart));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }
    }
}
