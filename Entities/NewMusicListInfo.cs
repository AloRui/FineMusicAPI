using System.Security.Principal;

namespace FineMusicAPI.Entities
{
    public class NewMusicListInfo
    {
        public string Name { get; set; } = "";
        public string Base64 { get; set; } = "";
        public string Desc { get; set; } = "";
    }
}