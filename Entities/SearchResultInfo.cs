namespace FineMusicAPI.Entities
{
    public class SearchResultInfo
    {
        public string Type { get; set; } = "";
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Dictionary<string, string> Detail { get; set; } = new Dictionary<string, string>();
    }
}