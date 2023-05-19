using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class Pupil
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }

    public string? MotherName { get; set; }

    public string? MotherPhoneNum { get; set; }

    public string? FatherPhoneNum { get; set; }

    public string? Address { get; set; }

    public string? FatherName { get; set; }

    public string? Orientation { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime? RegisterDate { get; set; }

    public int SectorId { get; set; }

    public int? ParentMaritalStatus { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual Sector Sector { get; set; } = null!;
}
