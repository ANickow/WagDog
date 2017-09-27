using System.ComponentModel.DataAnnotations;

namespace WagDog.Models

{
    public class MessageViewModel : BaseEntity
    {
        
        [Display(Name="Title: ")]
        [Required(ErrorMessage = "Give your message a title.")]
        public string MessageTitle { get; set; }

        [Display(Name="Message: ")]
        [Required(ErrorMessage = "You must have something to say!")]
        public string MessageContent { get; set; }

    }
}

// USE FOR MESSAGE/MESSAGEREPLY