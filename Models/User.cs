using System;
using System.Collections.Generic;

namespace FineMusicAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Nicename { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Photo { get; set; }

    public string Slogan { get; set; } = null!;

    public virtual ICollection<FollowedList> FollowedLists { get; set; } = new List<FollowedList>();

    public virtual ICollection<MusicList> MusicLists { get; set; } = new List<MusicList>();
}
