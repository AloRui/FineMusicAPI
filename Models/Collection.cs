using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class Collection
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SingerId { get; set; }

    public string Cover { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public virtual ICollection<Music> Musics { get; set; } = new List<Music>();

    public virtual Singer Singer { get; set; } = null!;
}
