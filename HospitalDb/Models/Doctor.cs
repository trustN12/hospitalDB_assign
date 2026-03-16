using System;
using System.Collections.Generic;

namespace HospitalDb.Models;

public partial class Doctor
{
    public int Did { get; set; }

    public string? Name { get; set; }

    public string? Specialisation { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
