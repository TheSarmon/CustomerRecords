using CustomerRecords.Application.Models;

namespace CustomerRecords.Application.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer Create(Customer customer);
        Customer GetById(string customerId);
        void Delete(string customerId);
    }
}
