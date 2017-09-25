using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Human : BaseEntity
    {
        public int HumanId { get; set; }
        public string Relationship { get; set; }
        public List<Family> Dogs { get; set; }
        public Human()
        {
            Dogs = new List<Family>();
        }

    }
}