using API.Context;
using API.Models;
using API.ViewModel;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;

        // Constructor
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        // Get Role Name By Email
        public AccountRoleVM GetUserData(string inputEmail)
        {
            // Join Data
            var hasilData = myContext.Employees
                .Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a})
                .Join(myContext.AccountRoles, ea => ea.a.NIK, ar => ar.NIK, (ea, ar) => new { ea, ar})
                .Join(myContext.Roles, eaar => eaar.ar.Role_Id, r => r.Id, (eaar, r) => new { eaar, r})
                .Select(data => new
                {
                    NIK = data.eaar.ea.e.NIK,
                    Email = data.eaar.ea.e.Email,
                    RoleId = data.eaar.ar.Role_Id,
                    RoleName = data.r.name

                }).SingleOrDefault(record => record.Email == inputEmail);

            var data = new AccountRoleVM()
            {
                NIK = hasilData.NIK,
                Email = hasilData.Email,
                RoleId = hasilData.RoleId,
                RoleName = hasilData.RoleName
            };
            return data;
        }
        
        // SignIn As Manager
        public int SignManager(LoginVM inputData)
        {
            // ambil NIK dari Email
            var data = myContext.Employees.SingleOrDefault(d => d.Email == inputData.Email);
            
            if (data != null)
            {
                // cek apakah email adalah director
                var cekDirector = myContext.AccountRoles.SingleOrDefault(d => (d.NIK == data.NIK) && (d.Role_Id == 3));

                if (cekDirector != null) // sudah menjadi direktor
                {
                    var dataSudahAda = myContext.AccountRoles.SingleOrDefault(d => (d.NIK == data.NIK) && (d.Role_Id == 2));
                    if (dataSudahAda == null) // data belum ada
                    {
                        var dataAR = new AccountRole()
                        {
                            NIK = data.NIK,
                            Role_Id = 2
                        };

                        Insert(dataAR); // data baru
                        return 1; // menambah role manager ke email tersebut
                    }
                    else
                    {
                        return -2; // data role manager sudah ada
                    }
                }
                {
                    return -1; // belum menjadi direktor
                }
            }
            else
            {
                return 0; // email tidak terdaftar
            }
        }
    }
}
