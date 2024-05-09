using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class M2mPlayerTeam
{
    /// <summary>
    /// ID записи
    /// </summary>
    public uint PltmId { get; set; }

    /// <summary>
    /// ID игрока
    /// </summary>
    public uint PltmPlayer { get; set; }

    /// <summary>
    /// ID команды
    /// </summary>
    public uint PltmTeam { get; set; }

    /// <summary>
    /// Роль игрока:
    /// 	0 - Banch
    /// 	1 - Safe lane
    /// 	2 - Mid lane
    /// 	3 - Off lane
    /// 	4 - Soft support
    /// 	5 - Hard support
    /// 	6 - Coach
    /// </summary>
    public byte? PltmRole { get; set; }

    /// <summary>
    /// Дата начала выступления за команду
    /// </summary>
    public DateOnly? PltmStartDate { get; set; }

    /// <summary>
    /// Дата окончания выступления за команду
    /// </summary>
    public DateOnly? PltmEndDate { get; set; }

    public virtual Player PltmPlayerNavigation { get; set; } = null!;

    public virtual Team PltmTeamNavigation { get; set; } = null!;
}
