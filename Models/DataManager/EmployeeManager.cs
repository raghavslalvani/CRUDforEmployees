using CRUDforEmployees.Models.Repository;

namespace CRUDforEmployees.Models.DataManager
{
    public class EmployeeManager : IDataRepository<Employee>
    {
        readonly EmployeeContext _employeeContext;
        public EmployeeManager(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employeeContext.Employees.ToList();
        }
        public Employee Get(long id)
        {
            return _employeeContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }
        public void Add(Employee entity)
        {
            _employeeContext.Employees.Add(entity);
            _employeeContext.SaveChanges();
        }
        public void Update(Employee employee, Employee entity)
        {
            employee.FirstName =entity.FirstName;
            employee.LastName = entity.LastName;
            employee.PhoneNumber = entity.PhoneNumber;
            employee.Email = entity.Email;
            employee.DateOfBirth = entity.DateOfBirth;

            _employeeContext.SaveChanges();
        }
        public void Delete(Employee entity)
        {
            _employeeContext.Employees.Remove(entity);
            _employeeContext.SaveChanges();
        }
    }
}
