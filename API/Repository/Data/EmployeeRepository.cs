using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
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

        // Get ALl Masetr Data
        public IEnumerable MasterEmployee()
        {
            // join data
            var data = myContext.Employees
                .Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a})
                .Join(myContext.Profilings, ea => ea.a.NIK, p => p.NIK, (ea, p) => new { ea, p})
                .Join(myContext.Educations, eap => eap.p.Education_Id, ed => ed.Id, (eap, ed) => new {eap, ed })
                .Join(myContext.Universities, eaped => eaped.ed.University_Id, u => u.Id, (eaped, u) => new {eaped, u})
                .Select(d => new
                {
                    NIK = d.eaped.eap.ea.e.NIK.ToString(),
                    FullName = $"{d.eaped.eap.ea.e.FirstName} {d.eaped.eap.ea.e.LastName}",
                    Phone = d.eaped.eap.ea.e.Phone,
                    Gender = d.eaped.eap.ea.e.Gender.ToString(),
                    Email = d.eaped.eap.ea.e.Email,
                    BirthDate = d.eaped.eap.ea.e.BirthDate.ToString("dd MMMM yyyy"),
                    Salary = d.eaped.eap.ea.e.Salary,
                    Education_Id = d.eaped.eap.p.Education_Id,
                    GPA = d.eaped.ed.GPA,
                    Degree = d.eaped.ed.Degree,
                    UniversityName = d.u.Name
                });

            return data;
        }

        public GenderVM getGender()
        {
            var data = new GenderVM()
            {
                jumlah_total = myContext.Employees.Count(),
                jumlah_pria = myContext.Employees.Count(e => e.Gender == 0),
                jumlah_wanita = myContext.Employees.Count(e => e.Gender > 0)
            };
            return data;
        }
    }
}
