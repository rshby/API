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
            // Auto Increment NIK
            var empCount = _context.Employees.Count() + 1;
            var Year = DateTime.Now.Year;

            // Cek phone dan email tidak boleh sama
            var dataEmail = _context.Employees.SingleOrDefault(e => e.Email == inputData.Email);
            var dataPhone = _context.Employees.SingleOrDefault(e => e.Phone == inputData.Phone);

            if (dataEmail == null && dataPhone == null) // email dan phone aman
            {
                // Save data Tabel Employee
                Employee emp = new Employee()
                {
                    NIK = Year + "00" + empCount.ToString(),
                    FirstName = inputData.FirstName,
                    LastName = inputData.LastName,
                    Phone = inputData.Phone,
                    BirthDate = inputData.BirthDate,
                    Salary = inputData.Salary,
                    Email = inputData.Email,
                    Gender = inputData.Gender
                };
                _context.Employees.Add(emp);
                _context.SaveChanges();

                // Save Data Tabel Account
                Account acc = new Account()
                {
                    NIK = emp.NIK,
                    Password = inputData.Password
                };
                _context.Accounts.Add(acc);
                _context.SaveChanges();

                // Save Data Education
                Education edu = new Education()
                {
                    Degree = inputData.Degree,
                    GPA = inputData.GPA,
                    University_Id = inputData.University_Id,
                };
                _context.Educations.Add(edu);
                _context.SaveChanges();

                // Save Data Tabel Profiling
                Profiling prf = new Profiling()
                {
                    NIK = emp.NIK,
                    Education_Id = edu.Id
                };
                _context.Profilings.Add(prf);
                _context.SaveChanges();
                return 1; // Sukses Register
            }
            else if (dataEmail != null && dataPhone == null) 
            {
                return -1; // email sudah ada
            }
            else if (dataEmail == null && dataPhone != null) 
            {
                return -2; // phone sudah ada
            }
            else
            {
                return 0; // Email dan Phone Sudah Ada
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

        // Change Password
        public int ChangePassword(ChangePasswordVM inputData)
        {
            // ambil 1 data hasil join untuk mencocokan
            var data = _context.Employees.Join(_context.Accounts,
                e => e.NIK,
                a => a.NIK,
                (e, a) => new {
                    NIK = a.NIK,
                    Email = e.Email,
                    Password = a.Password,
                    OTP = a.OTP,
                    ExpiredToken = a.ExpiredToken,
                    IsUsed = a.IsUsed
                }).SingleOrDefault(r => r.Email == inputData.Email);

            // cek email apakah ada di database
            if (data != null)
            {
                // cek apakah IsUsed == false
                if (data.IsUsed == false)
                {
                    // cek apakah OTP dan Expired Benar
                    if ((data.OTP == inputData.OTP) && (DateTime.Now <= data.ExpiredToken))
                    {
                        // cek apakah Password dan ConfirmPassword Sama
                        if (inputData.Password == inputData.ConfirmPassword)
                        {
                            // buat objek dari class model Account
                            var dataUpdate = new Account()
                            {
                                NIK = data.NIK,
                                Password = inputData.Password,
                                ExpiredToken = DateTime.Now,
                                IsUsed = true,
                                OTP = 0
                            };

                            // update ke database
                            var updateResult = Update(dataUpdate);

                            // cek apabila update berhasil
                            if (updateResult == 1)
                            {
                                return 1; // sukses Update Password
                            }
                            else
                            {
                                return -6; // gagal update
                            }
                        }
                        else
                        {
                            return -5; // Pass dan ConfirmPass Tidak Sama
                        }
                    }
                    else if((data.OTP != inputData.OTP) && (DateTime.Now <= data.ExpiredToken))
                    {
                        return -2; // OTP Salah
                    }
                    else if ((data.OTP == inputData.OTP) && (DateTime.Now > data.ExpiredToken))
                    {
                        return -3; // Expired
                    }
                    else
                    {
                        return -4; // OTP Salah dan sudah Expired
                    }
                }
                else
                {
                    return -1; // OTP sudah digunakan
                }
            }
            else
            {
                return 0; // email tidak ada di database
            }
        }
    }
}