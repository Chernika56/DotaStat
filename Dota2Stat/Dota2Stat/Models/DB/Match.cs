using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Match
{
    /// <summary>
    /// ID матча
    /// </summary>
    public uint MId { get; set; }

    /// <summary>
    /// Команда - победитель
    /// </summary>
    public uint? MWinner { get; set; }

    /// <summary>
    /// Дата и вермя проведения матча
    /// </summary>
    public DateTime? MDataTime { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Team? MWinnerNavigation { get; set; }

    public virtual ICollection<Team> MtmTeams { get; set; } = new List<Team>();

    public virtual ICollection<Tournament> TrmTounaments { get; set; } = new List<Tournament>();
}
