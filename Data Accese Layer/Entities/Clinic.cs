using System;
using System.Collections.Generic;

namespace Data_Accese_Layer.Entities;

public partial class Clinic
{
    public int ClinicId { get; set; }

    public int ClinicNumber { get; set; }

    public int? DeptId { get; set; }

    public int? DoctorId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
