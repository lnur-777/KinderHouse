using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }

    public string Name { get; set; } = null!;

    public string SurName { get; set; } = null!;
}
