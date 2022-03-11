using API.Context;
using API.Models;
using API.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        private readonly AccountRepository _accountRepository;

        // Constructor
        public AccountRoleRepository(MyContext myContext, AccountRepository accRepo) : base(myContext)
        {
            this.myContext = myContext;
            this._accountRepository = accRepo;
        }

        // Get Role Name By Email
        public IEnumerable GetUserData(string inputEmail)
        {
            // Join Data
            var hasilData = myContext.Employees
                .Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a})
                .Join(myContext.AccountRoles, ea => ea.a.NIK, ar => ar.NIK, (ea, ar) => new { ea, ar})
                .Join(myContext.Roles, eaar => eaar.ar.Role_Id, r => r.Id, (eaar, r) => new { eaar, r})
                .Where(record => record.eaar.ea.e.Email == inputEmail)
                .Select(data => data.r.name).ToList();

            return hasilData;
        }
        
        // SignIn As Manager
        public int SignInManager(LoginVM inputData)
        {
            // ambil data
            var data = myContext.Employees.SingleOrDefault(d => d.Email == inputData.Email);

            if (data != null)
            {
                // ambil data join
                var dataJoin = myContext.Employees.Join(myContext.Accounts,
                    e => e.NIK,
                    a => a.NIK,
                    (e, a) => new { e, a})
                    .Select(d => new { 
                        Email = d.e.Email,
                        Password = d.a.Password
                    }).SingleOrDefault(record => record.Email == inputData.Email);

                // cek email dan password apakah sama
                if ((inputData.Email == dataJoin.Email) && (_accountRepository.ValidatePassword(inputData.Password, dataJoin.Password)))
                {
                    return 1; // sukses Login
                }
                else
                {
                    return -1; // Password Salah
                }
            }
            else
            {
                return 0; // email tidak ada di database
            }
        }
    }
}
