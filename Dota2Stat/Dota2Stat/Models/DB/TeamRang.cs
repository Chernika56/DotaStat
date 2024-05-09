using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class TeamRang
{
    public uint TmrgId { get; set; }

    public uint? TmrgTeam { get; set; }

    public uint? TmrgRang { get; set; }

    public virtual Team? TmrgTeamNavigation { get; set; }
}
