namespace TaskManagementSystem.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TMSDbContext DB;
        public EmployeeRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }
        public Employee GetEmployee(int id)
        {
            return DB.Employees.Find(id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return DB.Employees;
        }

        public Employee AddEmployee(Employee employee)
        {
            DB.Employees.Add(employee);
            DB.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = DB.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return employeeChanges;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = DB.Employees.Find(id);
            if (employee != null)
            {
                DB.Employees.Remove(employee);
                DB.SaveChanges();
            }
            return employee;
        }
    }
}
