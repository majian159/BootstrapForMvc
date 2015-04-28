using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public sealed class StudentModel
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [DisplayName("学生名称")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [DisplayName("年龄")]
        [Range(0, 150)]
        public short Age { get; set; }

        [DisplayName("班级")]
        [StringLength(10)]
        public string Class { get; set; }

        [DisplayName("是否启用")]
        public bool Enable { get; set; }
    }
}