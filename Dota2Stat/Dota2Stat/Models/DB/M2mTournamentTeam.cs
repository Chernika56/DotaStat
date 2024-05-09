using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class M2mTournamentTeam
{
    /// <summary>
    /// ID команды
    /// </summary>
    public uint TrtmTeam { get; set; }

    /// <summary>
    /// ID турнира
    /// </summary>
    public uint TrtmTournament { get; set; }

    /// <summary>
    /// Результат команды на турнире(место)
    /// </summary>
    public byte? TrtmResult { get; set; }

    public virtual Team TrtmTeamNavigation { get; set; } = null!;

    public virtual Tournament TrtmTournamentNavigation { get; set; } = null!;
}
