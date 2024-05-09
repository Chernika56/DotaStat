using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Hero
{
    /// <summary>
    /// ID героя
    /// </summary>
    public uint HId { get; set; }

    /// <summary>
    /// Имя героя
    /// </summary>
    public string? HName { get; set; }

    /// <summary>
    /// Основной атрибут героя
    /// </summary>
    public string? HAttribute { get; set; }

    /// <summary>
    /// Описание героя
    /// </summary>
    public string? HDescription { get; set; }

    /// <summary>
    /// Базовое значение очков здоровья
    /// </summary>
    public ushort? HHp { get; set; }

    /// <summary>
    /// Базовое значение очков маны
    /// </summary>
    public ushort? HMp { get; set; }

    /// <summary>
    /// Базовое восстановление здоровья
    /// </summary>
    public float? HHpr { get; set; }

    /// <summary>
    /// Базовое восстановление очков маны
    /// </summary>
    public float? HMpr { get; set; }

    /// <summary>
    /// Базовая сила
    /// </summary>
    public float? HStrength { get; set; }

    /// <summary>
    /// Прирост силы за уровень
    /// </summary>
    public float? HStrengthPerLvl { get; set; }

    /// <summary>
    /// Базовая ловкость
    /// </summary>
    public float? HAgility { get; set; }

    /// <summary>
    /// Прирост ловкости за уровень
    /// </summary>
    public float? HAgilityPerLvl { get; set; }

    /// <summary>
    /// Базовый интелект
    /// </summary>
    public float? HIntelligence { get; set; }

    /// <summary>
    /// Прирост интелекта за уровень
    /// </summary>
    public float? HIntelligencePerLvl { get; set; }

    /// <summary>
    /// Базовый урон
    /// </summary>
    public ushort? HDamage { get; set; }

    /// <summary>
    /// Базовая скорость передвижения
    /// </summary>
    public ushort? HMoveSpeed { get; set; }

    /// <summary>
    /// Базовая броня
    /// </summary>
    public ushort? HArmor { get; set; }

    /// <summary>
    /// Базовая скорость атаки
    /// </summary>
    public ushort? HAttackSpeed { get; set; }

    /// <summary>
    /// Базовый интервал атак
    /// </summary>
    public float? HAttackInterval { get; set; }

    /// <summary>
    /// Тип атаки
    /// </summary>
    public string? HAttackType { get; set; }

    /// <summary>
    /// Базовая дальность атаки
    /// </summary>
    public ushort? HAttackRange { get; set; }

    /// <summary>
    /// Дополнительный урон заклинаний
    /// </summary>
    public float? HSpellAmp { get; set; }

    /// <summary>
    /// Базовое сопротивление магии
    /// </summary>
    public float? HMagicResist { get; set; }

    /// <summary>
    /// Базовое сопротивление эффектам
    /// </summary>
    public float? HStatusResist { get; set; }

    /// <summary>
    /// Базовое уклонение
    /// </summary>
    public float? HEvasion { get; set; }

    /// <summary>
    /// Описание Aghanim&apos;s Scepter
    /// </summary>
    public string? HScepter { get; set; }

    /// <summary>
    /// Описание Aghanim&apos;s shard
    /// </summary>
    public string? HShard { get; set; }

    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();

    public virtual ICollection<M2mHeroSkill> M2mHeroSkills { get; set; } = new List<M2mHeroSkill>();

    public virtual ICollection<PickBan> PickBans { get; set; } = new List<PickBan>();
}
