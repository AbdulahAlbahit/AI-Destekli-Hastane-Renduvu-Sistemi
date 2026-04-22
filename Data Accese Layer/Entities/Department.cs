using System;
using System.Collections.Generic;

namespace Data_Accese_Layer.Entities;

public partial class Department
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
}
