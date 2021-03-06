﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sem1Exampaper.Models.AttendModels
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Display(Name = "Student ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentID { get; set; } 

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<StudentSubject> studentSubjects { get; set; }
        public virtual ICollection<Attendance> attendances { get; set; }
    }
}