using System;
using System.Collections.Generic;

namespace Data_Accese_Layer.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateOnly TheDate { get; set; }

    public TimeOnly TheTime { get; set; }

    public string TheStatus { get; set; } = null!;

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public int? ClinicId { get; set; }

    public virtual Clinic? Clinic { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
