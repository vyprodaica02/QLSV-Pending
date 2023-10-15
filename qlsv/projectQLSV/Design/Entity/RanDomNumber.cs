using Design.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Design.Entity
{
    public class RanDomNumber : BaseEntity
    {
        public int? ranDomNumber { get; set; }
        public DateTime? exPiredTime { get; set; }
        public int? sinhVienID { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SinhVien? SinhVien { get; set; }
    }
}
