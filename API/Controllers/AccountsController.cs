using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository _accountRepo;

        // Constructor
        public AccountsController(AccountRepository accRepo) : base(accRepo)
        {
            this._accountRepo = accRepo;    
        }

        // Insert Register
        [HttpPost("register")]
        public ActionResult Register(RegisterVM inputData)
        {
            try
            {
                if (inputData != null)
                {
                    var hasilRegister = _accountRepo.Register(inputData); 
                    if (hasilRegister > 0)
                    {
                        return Ok("Sukses, Register Berhasil");
                    }
                    else if (hasilRegister == -1)
                    {
                        return BadRequest("Maaf, Email sudah ada");
                    }
                    else if (hasilRegister == -2)
                    {
                        return BadRequest("Maaf, nomor telepon sudah ada");
                    }
                    else
                    {
                        return BadRequest("Maaf, email dan nomor telepon sudah ada");
                    }
                }
                else
                {
                    return BadRequest("Data Kosong");
                }
            }
            catch (Exception)
            {
                return BadRequest("Error Saat Register");
            }
        }

        // Login
        [HttpGet("login")]
        public ActionResult Login(LoginVM inputData)
        {
            try
            {
                // cek data sudah diisi
                if (inputData != null)
                {
                    // panggil method Login di Repository
                    var hasilLogin = _accountRepo.Login(inputData);

                    // Cek hasil login
                    if (hasilLogin > 0)
                    {
                        return Ok("Selamat Login Berhasil");
                    }
                    else if (hasilLogin == 0)
                    {
                        return NotFound($"akun dengan email {inputData.Email} tidak ditemukan di database");
                    }
                    else if (hasilLogin == -1)
                    {
                        return BadRequest("Maaf, Password Anda Salah!");
                    }
                    else
                    {
                        return BadRequest("Maaf, email anda salah");
                    }
                }
                else
                {
                    return BadRequest("data tidak diisi");
                }
            }
            catch(Exception e) 
            {
                return BadRequest($"error system login : {e}");
            }   
        }

        // Forgot Password
        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(LoginVM inputData)
        {
            try
            {
                var hasil = _accountRepo.ForgotPassword(inputData.Email);
                if (hasil > 0)
                {
                    return Ok("Cek Email!!"); // sukses
                }
                else if (hasil == 0)
                {
                    return BadRequest("Cek Kodingan"); // gagal kirim email
                }
                else if (hasil == -1)
                {
                    return BadRequest("Gagal Update"); // gagal update
                }
                else
                {
                    return NotFound($"data dengan email {inputData.Email} tidak ada di database");
                }
            }
            catch(Exception e)
            {
                return BadRequest($"Error System : {e}");
            }
        }

        // Change Password
        [HttpPost("changepassword")]
        public ActionResult ChangePassword(ChangePasswordVM inputData)
        {
            try
            {
                // cek data tidak kosong dan Password dan ConfirmPass sama
                if (inputData != null)
                {
                    // proses change password
                    var changePassword = _accountRepo.ChangePassword(inputData);

                    // cek apakah chane passowrd berhasil
                    if (changePassword == 1)
                    {
                        return Ok($"SUKSES, Akun {inputData.Email} Berhasil Mengganti Passowrd");
                    }
                    else if (changePassword == 0)
                    {
                        return NotFound($"Data Dengan Email {inputData.Email} Tidak Ada di Database");
                    }
                    else if (changePassword == -1)
                    {
                        return BadRequest($"Kode OTP Salah");
                    }
                    else if (changePassword == -2)
                    {
                        return BadRequest("Kode OTP Salah");
                    }
                    else if (changePassword == -3)
                    {
                        return BadRequest("Kode OTP SUdah Expired");
                    }
                    else if (changePassword == -4)
                    {
                        return BadRequest("Kode OTP Salah dan Sudah Expired");
                    }
                    else if (changePassword == -5)
                    {
                        return BadRequest("Password dan Confirm Password Harus Sama");
                    }
                    else
                    {
                        return BadRequest("Error Proses Change Password");
                    }
                }
                else
                {
                    return BadRequest("Data Inputan Tidak Lengkap");
                }
            }
            catch(Exception e)
            {
                return BadRequest($"Error system Change Password : {e}");
            }
        }
    }
}
