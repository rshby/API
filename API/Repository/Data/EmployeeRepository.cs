using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;

        // Constructor
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;  
        }
    }
}
