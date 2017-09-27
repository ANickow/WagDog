using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Cohab : BaseEntity
    {
        public int CohabId { get; set; }
        public int DogId { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}