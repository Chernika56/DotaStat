using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class GamePlayerItem
{
    public uint GpliId { get; set; }

    /// <summary>
    /// ID игры и игрока
    /// </summary>
    public uint GpliGamePlayer { get; set; }

    /// <summary>
    /// ID предмета
    /// </summary>
    public uint GpliItem { get; set; }

    /// <summary>
    /// Время покупки предмета
    /// </summary>
    public TimeOnly GpliTime { get; set; }

    public virtual GamePlayer GpliGamePlayerNavigation { get; set; } = null!;

    public virtual Item GpliItemNavigation { get; set; } = null!;
}
