using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblAuthorization
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public int? AuthenId { get; set; }

    public int? Active { get; set; }

    public virtual TblAuthentication? Authen { get; set; }
}
