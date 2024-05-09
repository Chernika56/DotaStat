using Dota2Stat.Models.DB;

namespace Dota2Stat.Models
{
    public class TournamentTeam
    {
        public Tournament Tournament {  get; set; } 
        public uint? TmId { get; set; }
        public byte? TmResult { get; set; }
        public DateOnly TrStartDate { get; set; }
        public DateOnly TrEndDate { get; set; }
    }
}
