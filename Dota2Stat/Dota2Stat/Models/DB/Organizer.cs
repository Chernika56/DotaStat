using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Organizer
{
    /// <summary>
    /// ID организатора
    /// </summary>
    public uint OpId { get; set; }

    /// <summary>
    /// Название организатора
    /// </summary>
    public string? OpName { get; set; }

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
