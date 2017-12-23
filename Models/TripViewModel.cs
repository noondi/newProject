// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace newProject.Models 
// {  
//     public class TripViewModel : BaseEntity
//     {      

//         [Required]
//         [Display(Name = "Place: ")]
//         [MinLength(2)]
//         public string Destination {get; set; } // Trip name

//         [Required]
//         [Display(Name = "Description: ")]
//         [MinLength(20)]
//         public string Description {get; set; }

//         [Required]
//         [DataType(DataType.Date)]
//         [Display(Name = "Travel Start Date: ")]
//         [DateValidation(ErrorMessage = "Date cannot be in the past!")]        
//         public DateTime TravelStartDate {get; set; }

//          [Required]
//         [DataType(DataType.Date)]
//         [Display(Name = "Travel End Date: ")] 
//         [DateValidation(ErrorMessage = "Date cannot be in the past!")] 
//         public DateTime TravelEndDate {get; set; }
             
//     } 
    
// }