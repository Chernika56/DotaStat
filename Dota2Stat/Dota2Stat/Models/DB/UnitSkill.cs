using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class UnitSkill
{
    /// <summary>
    /// ID сущности
    /// </summary>
    public uint UsUnit { get; set; }

    /// <summary>
    /// ID способности
    /// </summary>
    public uint UsSkill { get; set; }

    /// <summary>
    /// Номер способности
    /// </summary>
    public byte? UsNumer { get; set; }

    public virtual Skill UsSkillNavigation { get; set; } = null!;

    public virtual Unit UsUnitNavigation { get; set; } = null!;
}
