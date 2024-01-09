using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class FollowedList
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MusicListId { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual MusicList MusicList { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
