namespace FineMusicAPI.Entities
{
    public class MusicInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CreateTime { get; set; } = "";
        public string Description { get; set; } = "";
        public int CollectionId { get; set; }
        public string CollectionName { get; set; } = "";
        public int SingerId { get; set; }
        public string SingerName { get; set; } = "";
        public string LrcFile { get; set; } = "";
        public int HitCount { get; set; }
        public string FileSrc { get; set; } = "";
    }
}