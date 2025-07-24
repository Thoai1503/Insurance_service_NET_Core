using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblAssignHistory
{
    public int Id { get; set; }

    public int ContractId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
