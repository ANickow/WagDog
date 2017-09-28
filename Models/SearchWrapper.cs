using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace WagDog.Models
{
    public class SearchWrapper : BaseEntity
    {
        public List<Dog> Dogs { get; set; }
        public List<Filter> Filters { get; set; }

        public SearchWrapper(List<Dog> d, List<Filter> f){
            Dogs = d;
            Filters = f;
        }
    }
}