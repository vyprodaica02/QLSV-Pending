using Design.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Design.DTO
{
    public class UserDTO
    {
        public string HoSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string HocBong { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? avata { get; set; }
        public int? MaLop { get; set; }

    }
}
