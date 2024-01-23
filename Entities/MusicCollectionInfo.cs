using System.Security.Principal;

namespace FineMusicAPI.Entities
{
    public class MusicCollectionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string SingerName { get; set; } = "";
        public List<MusicInfo> MusicList { get; set; } = new List<MusicInfo>();
    }
}
