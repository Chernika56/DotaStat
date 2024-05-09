using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Team
{
    /// <summary>
    /// ID команды
    /// </summary>
    public uint TmId { get; set; }

    /// <summary>
    /// Название команды
    /// </summary>
    public string? TmName { get; set; }

    /// <summary>
    /// Рейтинг команды(PTS)
    /// </summary>
    public uint? TmRating { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<M2mPlayerTeam> M2mPlayerTeams { get; set; } = new List<M2mPlayerTeam>();

    public virtual ICollection<M2mTournamentTeam> M2mTournamentTeams { get; set; } = new List<M2mTournamentTeam>();

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual TeamRang? TeamRang { get; set; }

    public virtual ICollection<TeamRating> TeamRatings { get; set; } = new List<TeamRating>();

    public virtual ICollection<Match> MtmMatches { get; set; } = new List<Match>();
}
