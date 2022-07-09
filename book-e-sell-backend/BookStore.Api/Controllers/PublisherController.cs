using BookStore.Model.Models;
using BookStore.Model.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        PublisherRepository _publisherRepository=new PublisherRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var publishers = _publisherRepository.GetPublishers(pageIndex, pageSize, keyword);
            ListResponse<PublisherModel> listResponse = new ListResponse<PublisherModel>()
            {
                Results = publishers.Results.Select(c => new PublisherModel(c)),
                TotalRecords = publishers.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPublisher(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            PublisherModel publisherModel = new PublisherModel(publisher);

            return Ok(publisherModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPublisher(PublisherModel model)
        {
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(response);

            return Ok(publisherModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.UpdatePublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(response);

            return Ok(publisherModel);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            return Ok(_publisherRepository.DeletePublisher(id));
        }
    }
}
