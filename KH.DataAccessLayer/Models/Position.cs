using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
