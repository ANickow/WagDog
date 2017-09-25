using System.ComponentModel.DataAnnotations;
namespace WagDog.Models

{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "Name: ")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string PassConf { get; set; }

        [Display(Name = "Breed")]
        public string Breed { get; set; }

        [Display(Name = "Age")]
        [Range(1,21)]
        public int Age { get; set; }

        [Display(Name = "Body Type")]
        public string BodyType { get; set; }

        [Display(Name = "Highest Education")]
        public string HighestEducation { get; set; }

        [Display(Name = "Barking" )]
        public string Barking { get; set; }

        [Display(Name = "Accidents")]
        public string Accidents { get; set; }


    }

}

