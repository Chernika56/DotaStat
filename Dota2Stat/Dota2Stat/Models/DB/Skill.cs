using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Skill
{
    /// <summary>
    /// ID способности
    /// </summary>
    public uint SkId { get; set; }

    /// <summary>
    /// Название способности
    /// </summary>
    public string? SkName { get; set; }

    /// <summary>
    /// Описание способности
    /// </summary>
    public string? SkDescription { get; set; }

    /// <summary>
    /// Характеристики способности
    /// </summary>
    public string? SkSpecifications { get; set; }

    /// <summary>
    /// Стоимость маны
    /// </summary>
    public uint? SkManaCost { get; set; }

    /// <summary>
    /// Стоимость здоровья
    /// </summary>
    public uint? SkHealthCost { get; set; }

    /// <summary>
    /// Перезарядка способности
    /// </summary>
    public float? SkReaload { get; set; }

    /// <summary>
    /// Максимальный уровень способности
    /// </summary>
    public byte? SkMaxLvl { get; set; }

    public virtual ICollection<M2mHeroSkill> M2mHeroSkills { get; set; } = new List<M2mHeroSkill>();

    public virtual ICollection<UnitSkill> UnitSkills { get; set; } = new List<UnitSkill>();
}
