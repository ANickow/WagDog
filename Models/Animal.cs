using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Animal : BaseEntity
    {
        public int AnimalId { get; set; }
        public string Type { get; set; }
        public List<Cohab> Dogs { get; set; }
        public Animal()
        {
            Dogs = new List<Cohab>();
        }

    }
}