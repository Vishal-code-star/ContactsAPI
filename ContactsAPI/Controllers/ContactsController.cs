using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
            var contact=await dbContext.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(contact);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                Name = addContactRequest.Name,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest)
        {
            var contact=await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                 contact.Name= updateContactRequest.Name;
                 contact.Phone= updateContactRequest.Phone;
                 contact.Email= updateContactRequest.Email;
                 contact.Address= updateContactRequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            else
                return NotFound();  

        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact!=null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            else 
                return NotFound();

        }
    }
}
