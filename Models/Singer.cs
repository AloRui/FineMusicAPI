using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class Singer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
