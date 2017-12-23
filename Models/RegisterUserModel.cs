using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newProject.Models 
{  
    public class RegisterUserModel: BaseEntity
    {
        [Key]

        [Required]
        [Display(Name = "First Name: ")]
        [MinLength(2)]
        public string FirstName {get; set; }
        
        [Required]
        [Display(Name = "Last Name: ")]
        [MinLength(2)]
        public string LastName {get; set; }        

        [Required]
        [EmailAddress]
        [Display(Name = "Email: ")]
        public string Email {get; set; } 

      

        [Required]
        [MinLength(2)]
        [DataType(DataType.Password)]
         [Display(Name = "Password: ")]
        public string Password {get; set; } 

        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password does not match Password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password: ")]
         public string ConfirmPassword {get; set; } 
        
    }
}