using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Player
{
    /// <summary>
    /// ID игрока
    /// </summary>
    public uint PlId { get; set; }

    /// <summary>
    /// Имя игрока
    /// </summary>
    public string? PlFirstName { get; set; }

    /// <summary>
    /// Фамилия игрока
    /// </summary>
    public string? PlLastName { get; set; }

    /// <summary>
    /// Игровой Nickname 
    /// </summary>
    public string? PlNickname { get; set; }

    /// <summary>
    /// Страна игрока
    /// </summary>
    public uint PlCountry { get; set; }

    /// <summary>
    /// Match Making Rating
    /// </summary>
    public uint? PlMmr { get; set; }

    /// <summary>
    /// Ранг игрока в мировом топе
    /// </summary>
    public uint? PlRang { get; set; }

    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();

    public virtual ICollection<M2mPlayerTeam> M2mPlayerTeams { get; set; } = new List<M2mPlayerTeam>();

    public virtual Country? PlCountryNavigation { get; set; } = null!;
}
