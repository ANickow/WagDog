using System.ComponentModel.DataAnnotations;

namespace WagDog.Models

{
    public class MessageViewModel : BaseEntity
    {
        
        [Display(Name="Message: ")]
        [Required(ErrorMessage = "Say something!")]
        public string MessageContent { get; set; }

    }
}