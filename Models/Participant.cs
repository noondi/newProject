using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using newProject.Models;

namespace newProject.Models
{
    public class Participant
    {
       
       [Key]
        public int ParticipantId {get;set;}    

        // This table results from the many to many marriage 
        // between the two tables weddings and users       
        public int ActivityId {get;set;}
        public Activity Activity {get;set;}
        
        public int UserId {get;set;}
        public User User {get;set;}

    }
}