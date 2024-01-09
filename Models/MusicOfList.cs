using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class MusicOfList
{
    public int Id { get; set; }

    public int MusicId { get; set; }

    public int MusicListId { get; set; }

    public DateTime AddedTime { get; set; }

    public virtual Music Music { get; set; } = null!;

    public virtual MusicList MusicList { get; set; } = null!;
}
