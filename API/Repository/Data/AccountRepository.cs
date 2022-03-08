using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext _context;
        private readonly EmployeeRepository _empRepo;

        // Constructor
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this._context = myContext; 
        }

        // Insert Register Account
        public int Register(RegisterVM inputData)
        {
            Employee emp = new Employee()
            {
                FirstName = inputData.FirstName,
                LastName = inputData.LastName,
                Phone = inputData.Phone,
                BirthDate = inputData.BirthDate,
                Salary = inputData.Salary,
                Email = inputData.Email,
                Gender = inputData.Gender
            };

            Account acc = new Account()
            {
                Password = inputData.Password,
            };

            Education edu = new Education()
            {
                Degree = inputData.Degree,
                GPA = inputData.GPA,
                University_Id = inputData.University_Id,
            };

            Profiling prf = new Profiling();

            // Auto Increment NIK
            var empCount = _context.Employees.Count() + 1;
            var Year = DateTime.Now.Year;
            emp.NIK = Year + "00" + empCount.ToString();
            acc.NIK = emp.NIK;

            // Set Data Tabel Profiling
            prf.NIK = acc.NIK;
            prf.Education = edu;

            // Cek phone dan email tidak boleh sama
            var dataEmail = _context.Employees.SingleOrDefault(e => e.Email == inputData.Email);
            var dataPhone = _context.Employees.SingleOrDefault(e => e.Phone == inputData.Phone);

            if (dataEmail == null && dataPhone == null) // email dan phone aman
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                _context.Accounts.Add(acc);
                _context.SaveChanges();
                _context.Educations.Add(edu);
                _context.SaveChanges();
                _context.Profilings.Add(prf);
                _context.SaveChanges();
                return 1;
            }
            else if (dataEmail != null && dataPhone == null) // email sudah ada
            {
                return -1;
            }
            else if (dataEmail == null && dataPhone != null) // phone sudah ada
            {
                return -2;
            }
            else
            {
                return 0;
            }
        }
    }
}
