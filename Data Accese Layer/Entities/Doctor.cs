using System;
using System.Collections.Generic;

namespace Data_Accese_Layer.Entities;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public int? ClinicId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Clinic? Clinic { get; set; }
}
