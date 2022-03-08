using API.Context;
using API.Models;
using API.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext _context;

        public IEnumerable<object> Accounts { get; private set; }

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
                _context.Accounts.Add(acc);
                _context.Educations.Add(edu);
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

        // Login menggunakan Email dan Password
        public int Login(LoginVM inputData)
        {
            // ambil data berdasarkan email
            var dataDiambil = _context.Employees.SingleOrDefault(e => e.Email == inputData.Email);

            // cek ke database apakah data email ada
            if (dataDiambil != null)
            {
                // ambil data join tabel Employee dan Account
                var data = _context.Employees.Join(_context.Accounts,
                    e => e.NIK,
                    a => a.NIK,
                    (e, a) => new
                    {
                        Email = e.Email,
                        Password = a.Password
                    }).SingleOrDefault(r => r.Email == inputData.Email);

                // cek apakah Email cocok dengan Password
                if (data.Email == inputData.Email && data.Password == inputData.Password)
                {
                    return 1; //sukses login
                }
                else if (data.Email == inputData.Email && data.Password != inputData.Password)
                {
                    return -1; // password salah
                }
                else
                {
                    return -2; // email salah
                }
            }
            else
            {
                return 0; // data email tidak ada
            }
        }

        // Lupa Password
        public int ForgotPassword(string inputEmail)
        {
            // join tabel Employee dan Account
            var dataJoin = _context.Employees.Join(_context.Accounts,
                e => e.NIK,
                a => a.NIK,
                (e, a) => new {
                    NIK = e.NIK,
                    Email = e.Email,
                    Password = a.Password
                }).SingleOrDefault(d => d.Email == inputEmail);

            // cek apakah email ada di database
            if (dataJoin != null)
            {
                // update dataDipilih tamahkan OTP
                var dataUpdate = new Account()
                {
                    NIK = dataJoin.NIK,
                    Password = "admin123", // set password default
                    ExpiredToken = DateTime.Now.AddMinutes(5),
                    IsUsed = false,
                    OTP = new Random().Next(111111, 999999)
                };

                // update tabel Account sesuai dengan dataUpdate
                var update = Update(dataUpdate);

                // cek apabila update berhasil
                if (update == 1)
                {
                    // kirim kode OTP ke email
                    try
                    {
                        var email = new MimeMessage();
                        email.From.Add(new MailboxAddress("OTP Forgot Password", "rshby99@gmail.com"));
                        email.To.Add(MailboxAddress.Parse(inputEmail));
                        email.Subject = "OTP Forgot Password API Account";
                        email.Body = new TextPart("Plain") { Text = $"Kode OTP : {dataUpdate.OTP}" };

                        SmtpClient smtp = new SmtpClient();
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("rshby99@gmail.com", "reo050299");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        smtp.Dispose();

                        return 1; // sukses kirim email
                    }
                    catch (Exception)
                    {
                        return 0; // gagal kirim email
                    }
                }
                else
                {
                    return -1; // gagal update
                }
            }
            else
            {
                return -2; // email tidak ada di database
            }
        }
    }
}