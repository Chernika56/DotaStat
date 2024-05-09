using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Game
{
    /// <summary>
    /// ID игры
    /// </summary>
    public uint GId { get; set; }

    /// <summary>
    /// ID матча
    /// </summary>
    public uint GMatch { get; set; }

    /// <summary>
    /// Номер игры
    /// </summary>
    public byte GNumber { get; set; }

    /// <summary>
    /// Команда-победитель
    /// </summary>
    public uint? GWinner { get; set; }

    /// <summary>
    /// Длительность игры
    /// </summary>
    public TimeOnly? GDuration { get; set; }

    /// <summary>
    /// Дата и время начала игры
    /// </summary>
    public DateTime? GDateTime { get; set; }

    public virtual Match GMatchNavigation { get; set; } = null!;

    public virtual Team? GWinnerNavigation { get; set; }

    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();

    public virtual ICollection<PickBan> PickBans { get; set; } = new List<PickBan>();
}
