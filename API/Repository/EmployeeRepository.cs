using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        // Delete Data Employee By NIK
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        // Get All Data Employees
        public IEnumerable<Employee> GET()
        {
            return context.Employees.ToList();
        }

        // Get Data Employee By NIK
        public Employee Get(string NIK)
        {
            var hasil = context.Employees.Find(NIK);
            return hasil;
        }

        // Insert Data Employee Into Database
        public int Insert(Employee employee)
        {
            var emailData = context.Employees.Where(e => e.Email == employee.Email).SingleOrDefault();
            var phoneData = context.Employees.Where(e => e.Phone == employee.Phone).SingleOrDefault();  
            if (emailData == null && phoneData == null)
            {
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return result;
            }
            else if (emailData != null && phoneData == null) // email sudah ada
            {
                var result = -1; 
                return result;
            }
            else if (phoneData != null && emailData == null) // no hp sudah ada
            {
                var result = -2;
                return result;
            }
            else
            {
                var result = 0;
                return result;
            }
        }
        
        // Update Data Employee
        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }

        // New Update Employee
        public async Task UpdateEmployee(string NIK, JsonPatchDocument empModel)
        {
            var hasilData = await context.Employees.FindAsync(NIK);
            if (hasilData != null)
            {
                empModel.ApplyTo(hasilData);
                await context.SaveChangesAsync();
            }
        }
    }
}