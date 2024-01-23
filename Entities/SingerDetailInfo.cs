namespace FineMusicAPI.Entities
{
    public class SingerDetailInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Birthday { get; set; } = "";
        public string Descriotion { get; set; } = "";
        public List<MusicInfo> Top10MusicList { get; set; } = new List<MusicInfo>();
        public List<MusicCollectionInfo> MyCollectionList { get; set; } = new List<MusicCollectionInfo>();

    }
}
