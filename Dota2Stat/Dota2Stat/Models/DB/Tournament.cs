using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Tournament
{
    /// <summary>
    /// ID турнира
    /// </summary>
    public uint TrId { get; set; }

    /// <summary>
    /// Название турнира
    /// </summary>
    public string? TrName { get; set; }

    /// <summary>
    /// Уровень турнира(зависит от рейтинга играющих на турнире команд)
    /// </summary>
    public byte? TrTier { get; set; }

    /// <summary>
    /// Число команд-участников на турнире
    /// </summary>
    public ushort? TrParticipants { get; set; }

    /// <summary>
    /// ID организатора
    /// </summary>
    public uint TrOrganizer { get; set; }

    /// <summary>
    /// Призовой фонд($)
    /// </summary>
    public uint? TrPrize { get; set; }

    /// <summary>
    /// Место проведения турнира: Страна, город
    /// </summary>
    public string? TrPlace { get; set; }

    /// <summary>
    /// Дата начала турнира
    /// </summary>
    public DateOnly? TrStartDate { get; set; }

    /// <summary>
    /// Дата окончания турнираы
    /// </summary>
    public DateOnly? TrEndDate { get; set; }

    public virtual ICollection<M2mTournamentTeam> M2mTournamentTeams { get; set; } = new List<M2mTournamentTeam>();

    public virtual Organizer? TrOrganizerNavigation { get; set; } = null!;

    public virtual ICollection<Match> TrmMatches { get; set; } = new List<Match>();

    public virtual ICollection<Studio> TrstStudios { get; set; } = new List<Studio>();
}
