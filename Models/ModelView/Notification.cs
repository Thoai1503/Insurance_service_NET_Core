﻿using Insurance_agency.Models.Entities;
using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public partial class Notification
{
    public int id { get; set; } =0;

    public string detail { get; set; } = "";

    public int notification_type_id { get; set; } = 0;

    public int status { get; set; } = 0;

    public int from { get; set; } = 0;
    public int to { get; set; } = 0;


    public int user_id { get; set; } = 0;

    public int is_read { get; set; } = 0;

    public User employee { get; set; } = null!;

    //  public virtual TblNotificationType? NotificationType { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
