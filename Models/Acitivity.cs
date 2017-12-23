using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newProject.Models 
{  
    public class Activity
    {
         [Key]
        public int ActivityId {get;set;}
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage="No special characters.")]
        [Display(Name="Title")]
        public string Title {get;set;}

        // [Required]       
        // [DataType(DataType.Date)]
        // [Display(Name="Time")]
        // public string Time {get;set;}

        [Required]
        [Display(Name="Activity Date")]
        [FutureDate]
        [DataType(DataType.Date)]
        public DateTime ActivityDate {get;set;}

        [Required]
        [Display(Name="Duration (hours)")]         
        public Decimal Duration {get;set;}

        [Required]
        [Display(Name="Description")]
        [RegularExpression(@"^[0-9A-Za-z\s-#]+$", ErrorMessage="No special characters.")]
        public string Description {get;set;}      

        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}

    
        public int UserId {get;set;}
        public User User {get;set;}

        // Because of the many to many relationship
      
        public List<Participant> Participants {get;set;}

        public Activity() {
            Participants = new List<Participant>();
        }
    }
}