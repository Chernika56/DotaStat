using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class TeamRating
{
    public uint TmrId { get; set; }

    /// <summary>
    /// ID команды
    /// </summary>
    public uint? TmrTeam { get; set; }

    /// <summary>
    /// Сколько очков добавлено
    /// </summary>
    public ushort? TmrRatingAdd { get; set; }

    /// <summary>
    /// Дата добавления очков
    /// </summary>
    public DateOnly? TmrDate { get; set; }

    public virtual Team? TmrTeamNavigation { get; set; }
}
