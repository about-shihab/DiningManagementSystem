using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    class ExpenseValidate
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }
    }

    [MetadataType(typeof(ExpenseValidate))]
    public partial class Expense
    {
        
    }
}
