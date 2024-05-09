using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class PickBan
{
    public uint PbId { get; set; }

    /// <summary>
    /// ID игры
    /// </summary>
    public uint PbGame { get; set; }

    /// <summary>
    /// ID героя
    /// </summary>
    public uint PbHero { get; set; }

    /// <summary>
    /// Статус героя
    /// </summary>
    public string? PbStatus { get; set; }

    public virtual Game PbGameNavigation { get; set; } = null!;

    public virtual Hero PbHeroNavigation { get; set; } = null!;
}
