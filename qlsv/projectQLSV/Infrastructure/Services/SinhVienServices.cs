using Design.Common;
using Design.DTO;
using Design.Entity;
using Infrastructure.DataEx;
using Infrastructure.Enum;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SinhVienServices<T> : IRepository<T> where T : SinhVien
    {
        private DbSet<T> _services;
        private readonly AppDbContext dbContext;
        public SinhVienServices(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<T> Table
        {
            get
            {
                return _services;
            }
        }

        public IQueryable<T> TableUntracked
        {
            get
            {
                return _services.AsNoTracking();
            }
        }
        public ICollection<T> Local
        {
            get
            {
                return _services.Local;
            }
        }


        public async Task AddAsync(T entity)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                entity.Lop = null;
                try
                {
                    if (Entities.Any(x => x.Id == entity.Id))
                    {
                        throw new InvalidOperationException("Tài khoản đã tồn tại");
                    }
                    entity.roll = "user";
                    await Entities.AddAsync(entity);
                    if (AutoCommitEnabledInternal)
                    {
                        await CommitAsync();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
        public async Task<ErrorHelper> CreatUser(UserDTO userDTO, IFormFile file)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    //var userHT = dbContext.sinhViens.Any(x=>x.Email == userDTO.Email);
                    var userHT = Entities.FirstOrDefault(x => x.Email == userDTO.Email);
                    if (userHT != null)
                    {
                        return ErrorHelper.EmailDaTontai;
                    }
                    else
                    {
                        userHT = (T)(object)new SinhVien();
                        userHT.Lop = null;
                        if (file != null && file.Length > 0)
                        {
                            string imageUrl = await uploadfile.UploadFile(file);
                            userDTO.avata = imageUrl;
                            userHT.avata = userDTO.avata;
                        }
                        userHT.HoSinhVien = userDTO.HoSinhVien ;
                        userHT.TenSinhVien = userDTO.TenSinhVien;
                        userHT.NgaySinh = userDTO.NgaySinh;
                        userHT.NoiSinh = userDTO.NoiSinh;
                        userHT.DiaChi = userDTO.DiaChi;
                        userHT.DienThoai = userDTO.DienThoai;
                        userHT.HocBong = userDTO.HocBong;
                        userHT.GioiTinh = userDTO.GioiTinh;
                        userHT.Email = userDTO.Email;
                        userHT.Password = userDTO.Password;
                        userHT.MaLop = userDTO.MaLop;
                        userHT.roll = "user";
                        await Entities.AddAsync(userHT);
                        if (AutoCommitEnabledInternal)
                        {
                            await CommitAsync();
                        }
                        var RollNumber = dbContext.ranDomNumbers.FirstOrDefault(x => x.sinhVienID != userHT.Id);
                        RollNumber = new RanDomNumber()
                        {
                            sinhVienID = userHT.Id
                        };
                        dbContext.ranDomNumbers.Add(RollNumber);
                        dbContext.SaveChanges();
                        trans.Commit();
                        return ErrorHelper.ThanhCong;

                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }


        public async Task<ErrorHelper> UpdateUser(UserDTO userDTO, IFormFile file)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var userHT = Entities.FirstOrDefault(x => x.Email == userDTO.Email);
                    if (userHT == null)
                    {
                        return ErrorHelper.EmailKhongTontai;
                    }
                    else
                    {
                        userHT.Lop = null;
                        if (file != null && file.Length > 0)
                        {
                            string imageUrl = await uploadfile.UploadFile(file);
                            userDTO.avata = imageUrl;
                            userHT.avata = userDTO.avata;
                        }
                        userHT.HoSinhVien = userDTO.HoSinhVien;
                        userHT.TenSinhVien = userDTO.TenSinhVien;
                        userHT.NgaySinh = userDTO.NgaySinh;
                        userHT.NoiSinh = userDTO.NoiSinh;
                        userHT.DiaChi = userDTO.DiaChi;
                        userHT.DienThoai = userDTO.DienThoai;
                        userHT.HocBong = userDTO.HocBong;
                        userHT.GioiTinh = userDTO.GioiTinh;
                        userHT.Email = userDTO.Email;
                        userHT.Password = userDTO.Password;
                        userHT.MaLop = userDTO.MaLop;
                        userHT.roll = "user";
                        dbContext.Update(userHT);
                        dbContext.SaveChanges();
                        trans.Commit();
                        return ErrorHelper.ThanhCong;

                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }


        public T Attach(T entity)
        {
            return _services.Attach(entity).Entity;

        }


        public async Task CommitAsync()
        {
            await dbContext.SaveChangesAsync();

        }
       
        [Authorize(Roles = "admin")]
        public async Task DeleteAsync(int id)
        {
            var userHt = dbContext.sinhViens.FirstOrDefault(x => x.Id == id);

            if (AutoCommitEnabledInternal)
            {
                var ranDomnumberHT = dbContext.ranDomNumbers.FirstOrDefault(x=> x.sinhVienID == userHt.Id);
                if (ranDomnumberHT != null)
                {
                    dbContext.Remove(userHt);
                    dbContext.Remove(ranDomnumberHT);
                    await dbContext.SaveChangesAsync();
                }
               
            }
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    ChangeStateToModifiedIfApplicable(entity);

                    if (AutoCommitEnabledInternal)
                    {
                        await CommitAsync();

                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
        public bool? AutoCommitEnabled { get; set; }

        private bool AutoCommitEnabledInternal
        {
            get
            {
                return AutoCommitEnabled ?? true;
            }
        }

        private void ChangeStateToModifiedIfApplicable(T entity)
        {

            var entry = dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
        }
        private DbSet<T> Entities
        {
            get
            {
                if (_services == null)
                {
                    _services = dbContext.Set<T>();
                }

                return _services as DbSet<T>;
            }
        }

        public async Task SendEmail(string Email)
        {
            var EmailHT = dbContext.sinhViens.FirstOrDefault(x => x.Email == Email);
            var rollnumber = dbContext.ranDomNumbers.FirstOrDefault(x => x.sinhVienID == EmailHT.Id);
            if (EmailHT != null)
            {
                Random random = new Random();
                int randomNumber = random.Next(1000, 1000001);
                string SenNumber = randomNumber.ToString();
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("duc.luongming@gmail.com"));
                email.To.Add(MailboxAddress.Parse(Email));
                email.Subject = "Mã token Email của bạn";
                email.Body = new TextPart(TextFormat.Html) { Text = SenNumber };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("duc.luongming@gmail.com", "byfjilzbcusuczbm");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                rollnumber.ranDomNumber = randomNumber;
                rollnumber.exPiredTime = DateTime.Now.AddMinutes(3);
                dbContext.Update(rollnumber);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ErrorHelper> SuaMk(string Email, string mkcu, string newmk, int rollnumber)
        {
            var emaiht = dbContext.sinhViens.FirstOrDefault(x => x.Email == Email);
            var rollnumberHT = dbContext.ranDomNumbers.FirstOrDefault(x => x.ranDomNumber == rollnumber);
            if (rollnumberHT == null)
            {
                return ErrorHelper.KeyKhongTonTai;
            }
            else
            {
                if (DateTime.Now < rollnumberHT.exPiredTime)
                {
                    if (emaiht != null && rollnumberHT != null)
                    {
                        if (await dbContext.sinhViens.AnyAsync(x => x.Email == Email && x.Password == mkcu))
                        {
                            emaiht.Password = newmk;
                            dbContext.Update(emaiht);
                            await dbContext.SaveChangesAsync();
                            return ErrorHelper.ThanhCong;
                        }
                        else
                        {
                            return ErrorHelper.MKSai;
                        }
                    }
                    else
                    {
                        return ErrorHelper.EmailKhongTontai;
                    }
                }
                else
                {
                    return ErrorHelper.KeyExpiredTime;
                }
            }
            
           
        }
    }
}
