using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Data;
using MvcDemo.Models;
using MvcDemo.Repositories;

namespace MvcDemo.Controllers.Api
{
    /// <summary>
    /// RESTful API for contacts
    /// </summary>
    /// <remarks>
    /// Resource: contact message from a web site visitor.
    /// 
    /// CRUD operation to http verb mappings:
    /// Create = Post
    /// Read   = Get
    /// Update = Put
    /// Delete = Delete
    /// </remarks>
    /// <usage>
    /// --------------------------------------------------------------------------------------
    /// API                         Description               Request Body   Response Body
    /// --------------------------------------------------------------------------------------
    /// GET /api/contacts           Get all items             None           Array of Contacts
    /// GET /api/contacts/{id}      Get an item by ID         None           Contact item
    /// POST /api/contacts          Add a new item            Contact item   Contact item
    /// PUT /api/contacts/{id}      Update an existing item   Contact item   None
    /// DELETE /api/contacts/{id}   Delete an item            None           None
    /// --------------------------------------------------------------------------------------
    /// </usage>
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactsController(DemoContext context, IContactRepository repository)
        {
            _repository = repository;

            if (_repository.Find().Count() == 0)
            {
                // Add some fake test data.
                _repository.Add(new Contact { DateSent = DateTime.Now.Date, Email = "test@test.com", Name = "Bart Simpson", Message = "I Didn't Do It. Nobody saw me do it. You can't prove anything!" });
                _repository.Add(new Contact { DateSent = DateTime.Now.Date, Email = "test@test.com", Name = "Lisa Simpson", Message = "Quit it, Bart!" });
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(_repository.Find().ToList());
        }

        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult Get(int id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound(); // 404
            }
            return new ObjectResult(item); // 200
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contact item)
        {
            if (item == null)
            {
                return BadRequest(); // 400 
            }

            item.DateSent = DateTime.Now.Date;
            _repository.Add(item);
            return CreatedAtRoute("GetContact", new { id = item.Id }, item); // returns 201 response plus location header of newly created item
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Contact item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest(); // 400
            }

            var contact = _repository.Find(id);
            if (contact == null)
            {
                return NotFound(); // 404
            }

            contact.Name = item.Name;
            contact.DateSent = item.DateSent;
            contact.Email = item.Email;
            contact.Message = item.Message;

            _repository.Update(contact);
            return new NoContentResult(); // 204 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _repository.Find(id);
            if (contact == null)
            {
                return NotFound(); // 404
            }

            _repository.Remove(id);
            return new NoContentResult(); // 204
        }
    }
}
