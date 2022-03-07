using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        // Constructor
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
