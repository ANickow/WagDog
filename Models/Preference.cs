using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Preference : BaseEntity
    {
       public int PreferenceId { get; set; }
       public int FilterId { get; set; }
       public Filter Filter {get; set; }
       public int DogId { get; set; }
       public bool DealBreaker { get; set; }
    }
}
