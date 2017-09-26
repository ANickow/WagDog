using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Preferences : BaseEntity
    {
       public int PreferencesId { get; set; }
       public int Value { get; set; }
       public string Category { get; set; }
       public int DogId { get; set; }
       public bool DealBreaker { get; set; }
    }
}
