using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookRepository _bookRepository = new BookRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var books = _bookRepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            BookModel bookModel = new BookModel(book);

            return Ok(bookModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddBook(BookModel model)
        {
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateBook(BookModel model)
        {
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            return Ok(_bookRepository.DeleteBook(id));
        }
    }
}
