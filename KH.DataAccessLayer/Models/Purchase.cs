using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class Purchase
{
    public int Id { get; set; }

    public int PupilId { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }

    public string? Note { get; set; }

    public virtual Pupil Pupil { get; set; } = null!;
}
