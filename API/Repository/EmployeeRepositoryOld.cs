using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepositoryOld : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepositoryOld(MyContext context)
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
            int lastNIK = 0;
            var emailData = context.Employees.SingleOrDefault(e => e.Email == employee.Email);
            var phoneData = context.Employees.SingleOrDefault(e => e.Phone == employee.Phone);  

            if (GET().Count() != 0)
            {
                lastNIK = int.Parse(context.Employees.OrderByDescending(e => e.NIK).
                    Select(e => e.NIK).FirstOrDefault().ToString().Substring(4));
            }
            employee.NIK = DateTime.Now.ToString("yyyy") + (lastNIK + 1).ToString().PadLeft(3, '0');

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
    }
}