using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Country
{
    /// <summary>
    /// ID страны
    /// </summary>
    public uint CnId { get; set; }

    /// <summary>
    /// Название страны
    /// </summary>
    public string CnName { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
