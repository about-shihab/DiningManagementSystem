using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class PaymentValidate
    {
        [Required]
        public decimal Amount { get; set; }
    }

    [MetadataType(typeof(PaymentValidate))]
    public partial class Payment
    {
    }
}
