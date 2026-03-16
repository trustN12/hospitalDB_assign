using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDb.Models;

public partial class Patient
{
    public int Pid { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Gender { get; set; }

    public int? Did { get; set; }

    [NotMapped]
    public string? DoctorName { get; set; }
    public virtual Doctor? DidNavigation { get; set; }
    
    public string Specialisation { get; set; }

}