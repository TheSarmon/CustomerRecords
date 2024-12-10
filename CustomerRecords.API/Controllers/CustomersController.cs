using CustomerRecords.API.Repositories;
using CustomerRecords.API.Models;

using Microsoft.AspNetCore.Mvc;


namespace CustomerRecords.API.Controllers
{
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetCustomers()
        {
            var customers = _customerRepository.GetAll();
            return Json(customers);
        }

        public JsonResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input" });
            }

            var newCustomer = _customerRepository.Create(customer);
            return Json(newCustomer);
        }
        
        public JsonResult DeleteCustomer(string customerId)
        {
            string message = _customerRepository.Delete(customerId);
            return Json(message);
        }
    }
}
