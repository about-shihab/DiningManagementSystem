using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    class MealValidate
    {

        [Display(Name = "Meal Id")]
        public int StudentId { get; set; }

        [Display(Name = "Meal Date")]
        public System.DateTime MealDate { get; set; }

        [Display(Name = "Guest Meal For Lunch")]
        public int LunchGuestMeal { get; set; }
        [Display(Name = "Dinner Meal For Lunch")]
        public int DinnerGuestMeal { get; set; }
    }

    [MetadataType(typeof(MealValidate))]
    public partial class Meal
    {
    }
}
