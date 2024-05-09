using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Unit
{
    /// <summary>
    /// ID сущности
    /// </summary>
    public uint UId { get; set; }

    /// <summary>
    /// Имя сущности
    /// </summary>
    public string? UName { get; set; }

    /// <summary>
    /// Описание сущности
    /// </summary>
    public string? UDescription { get; set; }

    /// <summary>
    /// Тип сущности
    /// </summary>
    public string? UType { get; set; }

    /// <summary>
    /// Базовое значение очков здоровья
    /// </summary>
    public ushort? UHp { get; set; }

    /// <summary>
    /// Базовое значение очков маны
    /// </summary>
    public ushort? UMp { get; set; }

    /// <summary>
    /// Базовое восстановление здоровья
    /// </summary>
    public float? UHpr { get; set; }

    /// <summary>
    /// Базовое восстановление очков маны
    /// </summary>
    public float? UMpr { get; set; }

    /// <summary>
    /// Базовый урон
    /// </summary>
    public ushort? UDamage { get; set; }

    /// <summary>
    /// Базовая скорость передвижения
    /// </summary>
    public ushort? UMoveSpeed { get; set; }

    /// <summary>
    /// Базовая броня
    /// </summary>
    public ushort? UArmor { get; set; }

    /// <summary>
    /// Базовая скорость атаки
    /// </summary>
    public ushort? UAttackSpeed { get; set; }

    /// <summary>
    /// Базовый интервал атак
    /// </summary>
    public float? UAttackInterval { get; set; }

    /// <summary>
    /// Тип атаки
    /// </summary>
    public string? UAttackType { get; set; }

    /// <summary>
    /// Базовая дальность атаки
    /// </summary>
    public ushort? UAttackRange { get; set; }

    /// <summary>
    /// Дополнительный урон заклинаний
    /// </summary>
    public float? USpellAmp { get; set; }

    /// <summary>
    /// Базовое сопротивление магии
    /// </summary>
    public float? UMagicResist { get; set; }

    /// <summary>
    /// Базовое сопротивление эффектам
    /// </summary>
    public float? UStatusResist { get; set; }

    /// <summary>
    /// Золото за убийство
    /// </summary>
    public uint? UGold { get; set; }

    /// <summary>
    /// Опыт за убийство
    /// </summary>
    public uint? UExperience { get; set; }

    public virtual ICollection<UnitSkill>? UnitSkills { get; set; } = new List<UnitSkill>();
}
