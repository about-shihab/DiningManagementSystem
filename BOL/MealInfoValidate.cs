using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{


    public class DateRangeAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            DateTime date = DateTime.Parse(value.ToString()); // assuming it's in a parsable string format

            if (date > DateTime.Now) 
            {
                return true;
            }

            return false;
        }
    }

    public class MealInfoValidate
    {

        [Required(ErrorMessage = "Start Date is Required")]
        [DataType(DataType.Date)]
        [DateRange(ErrorMessage = "Can't off Todays Meal")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is Required")]
        [DataType(DataType.Date)]
        [DateRange(ErrorMessage = "Can't off Todays Meal")]
        public System.DateTime EndDate { get; set; }
    }


    [MetadataType(typeof(MealInfoValidate))]
    public partial class MealInfo
    {
    }
}
