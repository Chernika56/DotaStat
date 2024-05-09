using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class NeutralItem
{
    /// <summary>
    /// ID нейтрального предмета
    /// </summary>
    public uint NiId { get; set; }

    /// <summary>
    /// Название нейстрального предмета
    /// </summary>
    public string? NiName { get; set; }

    /// <summary>
    /// Описание нейтрального предмета
    /// </summary>
    public string? NiDescription { get; set; }

    /// <summary>
    /// Уровень предмета
    /// </summary>
    public byte? NiTier { get; set; }

    /// <summary>
    /// Максимальое количество зарядов нейтрального предмета
    /// </summary>
    public byte? NiMaxCharges { get; set; }

    /// <summary>
    /// Характеристики нейтрального предмета
    /// </summary>
    public string? NiSpecifications { get; set; }

    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
}
