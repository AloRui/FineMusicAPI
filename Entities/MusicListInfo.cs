namespace FineMusicAPI.Entities
{
    public class MusicListInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string CreateTime { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MusicCount { get; set; }
    }
}