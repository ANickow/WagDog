using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Interest : BaseEntity
    {
        public int InterestId { get; set; }
        public string Description { get; set; }
    }
}