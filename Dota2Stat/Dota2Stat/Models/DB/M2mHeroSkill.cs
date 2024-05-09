using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class M2mHeroSkill
{
    /// <summary>
    /// ID героя
    /// </summary>
    public uint HsHero { get; set; }

    /// <summary>
    /// ID способности
    /// </summary>
    public uint HsSkill { get; set; }

    /// <summary>
    /// Номер способности у героя
    /// </summary>
    public ushort? HsNumber { get; set; }

    public virtual Hero HsHeroNavigation { get; set; } = null!;

    public virtual Skill HsSkillNavigation { get; set; } = null!;
}
