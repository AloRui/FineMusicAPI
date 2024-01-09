using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class Music
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public string? Description { get; set; }

    public int CollectionId { get; set; }

    public string? LrcFile { get; set; }

    public int HitCount { get; set; }

    public string FileSrc { get; set; } = null!;

    public virtual Collection Collection { get; set; } = null!;

    public virtual ICollection<MusicOfList> MusicOfLists { get; set; } = new List<MusicOfList>();
}
