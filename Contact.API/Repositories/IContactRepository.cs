using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<BusinessContact>> Get();
        Task<BusinessContact> Get(int id);
        Task<BusinessContact> Create(BusinessContact contact);
        Task Update(BusinessContact contact);
        Task Delete(int id);
    }
}
