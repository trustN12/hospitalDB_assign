using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDb.Models;

public partial class Patient
{
    public int Pid { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string? Fname { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string? Lname { get; set; }

    [Required(ErrorMessage = "Please select Gender")]
    public string? Gender { get; set; }

    [Required(ErrorMessage = "Please select Doctor")]
    public int? Did { get; set; }

    [NotMapped]
    public string? DoctorName { get; set; }
    public virtual Doctor? DidNavigation { get; set; }
    
    
    public string? Specialisation { get; set; }

}