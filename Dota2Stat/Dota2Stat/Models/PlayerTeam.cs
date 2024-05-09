using Dota2Stat.Models.DB;

namespace Dota2Stat.Models
{
    public class PlayerTeam
    {
        public Team Team { get; set; }
        public uint? TmId { get; set; }
        public string? TmName { get; set; }
        public uint? TmRating { get; set; }
        public List<PlayerInfo>? Players { get; set; }
        public List<uint> SelectedPlayerIds { get; set; }
    }

    public class PlayerInfo
    {
        public uint PlId { get; set; }
        public string NickName { get; set; }
    }
}
