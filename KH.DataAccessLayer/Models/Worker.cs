using System;
using System.Collections.Generic;

namespace KH.DataAccessLayer.Models;

public partial class Worker
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }

    public string? FatherName { get; set; }

    public int? PositionId { get; set; }

    public int? LessonId { get; set; }

    public int? SectorId { get; set; }

    public decimal? Salary { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public string? Note { get; set; }

    public virtual Lesson? Lesson { get; set; }

    public virtual Position? Position { get; set; }

    public virtual Sector? Sector { get; set; }
}
