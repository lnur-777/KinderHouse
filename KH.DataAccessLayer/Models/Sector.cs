using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class Sector
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Pupil> Pupils { get; set; } = new List<Pupil>();

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
