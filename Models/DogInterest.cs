using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class DogInterest : BaseEntity
    {
        public int DogInterestId { get; set; }
        public int DogId { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}