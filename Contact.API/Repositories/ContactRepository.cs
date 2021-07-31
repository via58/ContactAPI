using Contact.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;
        public ContactRepository(ContactContext context)
        {
            this._context = context;
        }
        public async Task<BusinessContact> Create(BusinessContact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task Delete(int id)
        {
            var contactToDelete = await _context.Contacts.FindAsync(id);
            if (contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BusinessContact>> Get()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<BusinessContact> Get(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task Update(BusinessContact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
