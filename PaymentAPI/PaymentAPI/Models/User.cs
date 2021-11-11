using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string password { get; set; }

        [Column(TypeName = "int")]
        public int age { get; set; }

        [Column(TypeName = "int")]
        public int marks { get; set; }
    }
}
