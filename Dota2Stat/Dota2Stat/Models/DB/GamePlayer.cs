using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class GamePlayer
{
    public uint GplId { get; set; }

    /// <summary>
    /// ID игры
    /// </summary>
    public uint GplGame { get; set; }

    /// <summary>
    /// ID игрока
    /// </summary>
    public uint GplPlayer { get; set; }

    /// <summary>
    /// ID героя
    /// </summary>
    public uint? GplHero { get; set; }

    /// <summary>
    /// Количесво убийств
    /// </summary>
    public uint? GplKill { get; set; }

    /// <summary>
    /// Количество смертей
    /// </summary>
    public uint? GplDead { get; set; }

    /// <summary>
    /// Количество помощей
    /// </summary>
    public uint? GplSupport { get; set; }

    /// <summary>
    /// Вражеских крипов добито
    /// </summary>
    public uint? GplEnemyCreeps { get; set; }

    /// <summary>
    /// Союзных крипов добито
    /// </summary>
    public uint? GplAlliedCreeps { get; set; }

    /// <summary>
    /// Общая стоимость
    /// </summary>
    public uint? GplNethworth { get; set; }

    /// <summary>
    /// Золото в минуту
    /// </summary>
    public uint? GplGpm { get; set; }

    /// <summary>
    /// Опыт в минуту
    /// </summary>
    public uint? GplEpm { get; set; }

    /// <summary>
    /// ID нейтрального предмета
    /// </summary>
    public uint? GplNeutralItem { get; set; }

    public virtual ICollection<GamePlayerItem> GamePlayerItems { get; set; } = new List<GamePlayerItem>();

    public virtual Game GplGameNavigation { get; set; } = null!;

    public virtual Hero? GplHeroNavigation { get; set; }

    public virtual NeutralItem? GplNeutralItemNavigation { get; set; }

    public virtual Player GplPlayerNavigation { get; set; } = null!;
}
