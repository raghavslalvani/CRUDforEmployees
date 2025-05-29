using CRUDforEmployees.Models;
using CRUDforEmployees.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDforEmployees.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IDataRepository<Employee> _dataRepository;
        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }
        [HttpGet("{id}",Name = "Get")]
        public IActionResult Get(long id)
        {

            Employee employee = _dataRepository.Get(id);
            if(employee == null)
            {
                return NotFound("Employee coudn't be found");
            }
                return Ok(employee);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("employee is null");
            }
                _dataRepository.Add(employee);
            return CreatedAtRoute(
                "Get",
                new { Id = employee.EmployeeId },employee);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id,[FromBody] Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null");
            }
            Employee employeetobeUpdated = _dataRepository.Get(id);
            if(employeetobeUpdated == null)
            {
                return NotFound("The requested Employee could not be found");
            }
            _dataRepository.Update(employeetobeUpdated,employee);
            return Ok(
                new Employee()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    EmployeeId = id
                });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Employee employee = _dataRepository.Get(id);
            if(employee == null)
            {
                return NotFound("The requested Employee could not be found");
            }
            _dataRepository.Delete(employee);
            return Ok("Employee deleted successfully");
        }
    }
}
