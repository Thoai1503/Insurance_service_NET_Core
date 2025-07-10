using Insurance_agency.Models;
using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public partial class User
{
    public int id { get; set; }

    public string full_name { get; set; }

    public string email { get; set; }

    public string password { get; set; }

    public string phone { get; set; }

    public string address { get; set; } = "";

    public int auth_id { get; set; }

    public int active { get; set; }

    public virtual ICollection<ContractView> Contracts { get; set; } = new List<ContractView>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
