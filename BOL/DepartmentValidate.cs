using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    class DepartmentValidate
    {
        [Required]
        [Display(Name = "Department")]
        public string Name { get; set; }
        [Required]
        public int DeptId { get; set; }
    }
    [MetadataType(typeof(DepartmentValidate))]
    public partial class Department
    {

    }
}
