using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class MusicList
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public string? Cover { get; set; }

    public DateTime CreateTime { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<FollowedList> FollowedLists { get; set; } = new List<FollowedList>();

    public virtual ICollection<MusicOfList> MusicOfLists { get; set; } = new List<MusicOfList>();

    public virtual User User { get; set; } = null!;
}
