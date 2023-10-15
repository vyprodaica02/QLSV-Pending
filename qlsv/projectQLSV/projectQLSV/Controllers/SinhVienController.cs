using Design.DTO;
using Design.Entity;
using Infrastructure.DataEx;
using Infrastructure.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace projectQLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly SinhVienServices<SinhVien> repository;
        private readonly AppDbContext _dbContext;

        public SinhVienController(SinhVienServices<SinhVien> userRepository, AppDbContext dbContext)
        {
            repository = userRepository;
            this._dbContext = dbContext;
        }

        [HttpPost("AddSinhVien")]
        public async Task<IActionResult> Add(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                return BadRequest("lỗi");
            }
            await repository.AddAsync(sinhVien);
            return Ok("oke");
        }

        [HttpPost("CreatUser")]
        public async Task<IActionResult> CreatUser([FromForm] UserDTO userDTO,  IFormFile file)
        {
            if (userDTO == null)
            {
                return BadRequest("lỗi");
            }
           var res =  await repository.CreatUser(userDTO, file);

            if (res == ErrorHelper.ThanhCong)
            {
                return Ok("Thành công");
            }
            else if (res == ErrorHelper.EmailDaTontai)
            {
                return BadRequest("Email đã tồn tại");
            }
            else
            {
                return BadRequest("Lỗi");

            }
        }

        [HttpDelete("Delete/id")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Lỗi");
            }
            else
            {
                await repository.DeleteAsync(id);
                return Ok("oke");
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateUse(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                return BadRequest("Lỗi");
            }
            else
            {
                await repository.UpdateAsync(sinhVien);
                return Ok("oke");

            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var res = repository.GetAll();
            return Ok(res);
        }

        [HttpGet("getFilter")]
        public async Task<IActionResult> GetFilter(string? userId, int pageIndex, int pageSize)
        {
            var query = _dbContext.sinhViens.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.TenSinhVien == userId);
            }

            // Không áp dụng phân trang nếu pageSize <= 0
            if (pageSize <= 0)
            {
                var filteredData = await query.ToListAsync();
                return Ok(filteredData);
            }

            // Phân trang
            int totalItems = await query.CountAsync();

            // Tính totalPages một cách chính xác
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Đảm bảo rằng pageIndex hợp lệ và không vượt quá số lượng trang
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var filteredDataWithPaging = await query.ToListAsync();

            // Trả về dữ liệu phân trang cùng với totalPages
            var result = new
            {
                Data = filteredDataWithPaging,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> sendEmail(string Email)
        {
           await repository.SendEmail(Email);
            return Ok("thanh công");
        }

        [HttpPut("SuaMK")]
        public async Task<IActionResult> SuaMK(string Email, string mkcu, string newmk, int rollnumber)
        {
            var res = await repository.SuaMk(Email, mkcu, newmk, rollnumber);
            if (res == ErrorHelper.ThanhCong)
            {
                return Ok("Thành công");
            }
            else if (res == ErrorHelper.EmailKhongTontai)
            {
                return BadRequest("Email không tồn tại");
            }
            else if (res == ErrorHelper.MKSai)
            {
                return BadRequest("Mật khẩu sai");
            }
            else if (res == ErrorHelper.KeyExpiredTime)
            {
                return BadRequest("Key đã hết hạn");
            }
            else
            {
                return BadRequest("Lỗi");
            }
        }
    }
}