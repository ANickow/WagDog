using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class Filter : BaseEntity
    {
       public int FilterId { get; set; }
       public string Category { get; set; }
       public string UserFilter { get; set; }
       public string LinqFilter { get; set; }
    }
}