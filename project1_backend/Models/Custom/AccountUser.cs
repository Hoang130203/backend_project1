using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace project1_backend.Models.Custom
{
    public  class AccountUser
    {
        [Key]
        public string PhoneNumber { get; set; } 
        public string PassWord { get; set; }    
        public string Name { get; set; }
        public string Address {  get; set; }

    }
}
