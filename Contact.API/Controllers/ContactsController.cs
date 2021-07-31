using Contact.API.Models;
using Contact.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactsController(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<BusinessContact>> GetContacts()
        {
            return await _contactRepository.Get();
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<BusinessContact>> GetContacts(int id)
        {
            return await _contactRepository.Get(id);
        }
        [HttpPost]
        
        public async Task<ActionResult<BusinessContact>> PostContacts([FromBody] BusinessContact businessContact)
        {
            var newContact = await _contactRepository.Create(businessContact);
            return CreatedAtAction(nameof(GetContacts), new { id = newContact.Id }, newContact);
        }
        [HttpPut]
        public async Task<ActionResult> PutContacts(int id, [FromBody] BusinessContact businessContact)
        {
            if (id != businessContact.Id)
            {
                return BadRequest();
            }
            await _contactRepository.Update(businessContact);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var contactToDelete = await _contactRepository.Get(id);
            if (contactToDelete == null)
                return NotFound();

            await _contactRepository.Delete(contactToDelete.Id);
            return NoContent();
        }
    }
}
