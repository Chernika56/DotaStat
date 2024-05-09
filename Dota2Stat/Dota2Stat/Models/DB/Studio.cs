using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Studio
{
    /// <summary>
    /// ID студии освещения
    /// </summary>
    public uint StId { get; set; }

    /// <summary>
    /// Название студии
    /// </summary>
    public string? StName { get; set; }

    public virtual ICollection<Tournament> TrstTournaments { get; set; } = new List<Tournament>();
}
