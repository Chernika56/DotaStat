using System;
using System.Collections.Generic;
using Dota2Stat.Models.DB;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Dota2Stat;

public partial class Dota2statContext : DbContext
{
    public Dota2statContext()
    {
    }

    public Dota2statContext(DbContextOptions<Dota2statContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssemblingItem> AssemblingItems { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamePlayer> GamePlayers { get; set; }

    public virtual DbSet<GamePlayerItem> GamePlayerItems { get; set; }

    public virtual DbSet<Hero> Heroes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<M2mHeroSkill> M2mHeroSkills { get; set; }

    public virtual DbSet<M2mPlayerTeam> M2mPlayerTeams { get; set; }

    public virtual DbSet<M2mTournamentTeam> M2mTournamentTeams { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<NeutralItem> NeutralItems { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<PickBan> PickBans { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRang> TeamRangs { get; set; }

    public virtual DbSet<TeamRating> TeamRatings { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitSkill> UnitSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Chernika_56;database=dota2stat", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<AssemblingItem>(entity =>
        {
            entity.HasKey(e => new { e.AiCraftedItem, e.AiIngredientItem })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("assembling items");

            entity.HasIndex(e => e.AiCraftedItem, "IXFK_Assembling Items_Items");

            entity.HasIndex(e => e.AiIngredientItem, "IXFK_Assembling Items_Items_02");

            entity.Property(e => e.AiCraftedItem)
                .HasComment("ID предмета, который создается")
                .HasColumnName("ai_CraftedItem");
            entity.Property(e => e.AiIngredientItem)
                .HasComment("ID предмета, который используется для создания")
                .HasColumnName("ai_IngredientItem");
            entity.Property(e => e.AiQuantity)
                .HasComment("Количество предметов, необходимых для создания")
                .HasColumnName("ai_Quantity");

            entity.HasOne(d => d.AiCraftedItemNavigation).WithMany(p => p.AssemblingItemAiCraftedItemNavigations)
                .HasForeignKey(d => d.AiCraftedItem)
                .HasConstraintName("FK_Assembling Items_Items");

            entity.HasOne(d => d.AiIngredientItemNavigation).WithMany(p => p.AssemblingItemAiIngredientItemNavigations)
                .HasForeignKey(d => d.AiIngredientItem)
                .HasConstraintName("FK_Assembling Items_Items_02");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CnId).HasName("PRIMARY");

            entity.ToTable("country");

            entity.HasIndex(e => e.CnName, "UNQ_cn_Name").IsUnique();

            entity.Property(e => e.CnId)
                .HasComment("ID страны")
                .HasColumnName("cn_ID");
            entity.Property(e => e.CnName)
                .HasMaxLength(50)
                .HasComment("Название страны")
                .HasColumnName("cn_Name");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GId).HasName("PRIMARY");

            entity.ToTable("game");

            entity.HasIndex(e => e.GMatch, "IXFK_Game_Match");

            entity.HasIndex(e => e.GWinner, "IXFK_Game_Teams");

            entity.Property(e => e.GId)
                .HasComment("ID игры")
                .HasColumnName("g_ID");
            entity.Property(e => e.GDateTime)
                .HasComment("Дата и время начала игры")
                .HasColumnType("datetime")
                .HasColumnName("g_Date&Time");
            entity.Property(e => e.GDuration)
                .HasComment("Длительность игры")
                .HasColumnType("time")
                .HasColumnName("g_Duration");
            entity.Property(e => e.GMatch)
                .HasComment("ID матча")
                .HasColumnName("g_Match");
            entity.Property(e => e.GNumber)
                .HasComment("Номер игры")
                .HasColumnName("g_Number");
            entity.Property(e => e.GWinner)
                .HasComment("Команда-победитель")
                .HasColumnName("g_Winner");

            entity.HasOne(d => d.GMatchNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.GMatch)
                .HasConstraintName("FK_Game_Match");

            entity.HasOne(d => d.GWinnerNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.GWinner)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Game_Teams");
        });

        modelBuilder.Entity<GamePlayer>(entity =>
        {
            entity.HasKey(e => e.GplId).HasName("PRIMARY");

            entity.ToTable("game-player");

            entity.HasIndex(e => e.GplGame, "IXFK_Game-Player_Game");

            entity.HasIndex(e => e.GplHero, "IXFK_Game-Player_Hero");

            entity.HasIndex(e => e.GplNeutralItem, "IXFK_Game-Player_NeutralItem");

            entity.HasIndex(e => e.GplPlayer, "IXFK_Game-Player_Player");

            entity.Property(e => e.GplId).HasColumnName("gpl_ID");
            entity.Property(e => e.GplAlliedCreeps)
                .HasComment("Союзных крипов добито")
                .HasColumnName("gpl_AlliedCreeps");
            entity.Property(e => e.GplDead)
                .HasComment("Количество смертей")
                .HasColumnName("gpl_Dead");
            entity.Property(e => e.GplEnemyCreeps)
                .HasComment("Вражеских крипов добито")
                .HasColumnName("gpl_EnemyCreeps");
            entity.Property(e => e.GplEpm)
                .HasComment("Опыт в минуту")
                .HasColumnName("gpl_EPM");
            entity.Property(e => e.GplGame)
                .HasComment("ID игры")
                .HasColumnName("gpl_Game");
            entity.Property(e => e.GplGpm)
                .HasComment("Золото в минуту")
                .HasColumnName("gpl_GPM");
            entity.Property(e => e.GplHero)
                .HasComment("ID героя")
                .HasColumnName("gpl_Hero");
            entity.Property(e => e.GplKill)
                .HasComment("Количесво убийств")
                .HasColumnName("gpl_Kill");
            entity.Property(e => e.GplNethworth)
                .HasComment("Общая стоимость")
                .HasColumnName("gpl_Nethworth");
            entity.Property(e => e.GplNeutralItem)
                .HasComment("ID нейтрального предмета")
                .HasColumnName("gpl_NeutralItem");
            entity.Property(e => e.GplPlayer)
                .HasComment("ID игрока")
                .HasColumnName("gpl_Player");
            entity.Property(e => e.GplSupport)
                .HasComment("Количество помощей")
                .HasColumnName("gpl_Support");

            entity.HasOne(d => d.GplGameNavigation).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.GplGame)
                .HasConstraintName("FK_Game-Player_Game");

            entity.HasOne(d => d.GplHeroNavigation).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.GplHero)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Game-Player_Hero");

            entity.HasOne(d => d.GplNeutralItemNavigation).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.GplNeutralItem)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Game-Player_Neutral Item");

            entity.HasOne(d => d.GplPlayerNavigation).WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.GplPlayer)
                .HasConstraintName("FK_Game-Player_Player");
        });

        modelBuilder.Entity<GamePlayerItem>(entity =>
        {
            entity.HasKey(e => e.GpliId).HasName("PRIMARY");

            entity.ToTable("game-player-item");

            entity.HasIndex(e => e.GpliGamePlayer, "IXFK_Game-Player-Item_Game-Player");

            entity.HasIndex(e => e.GpliItem, "IXFK_Game-Player-Item_Items");

            entity.Property(e => e.GpliId).HasColumnName("gpli_ID");
            entity.Property(e => e.GpliGamePlayer)
                .HasComment("ID игры и игрока")
                .HasColumnName("gpli_Game-Player");
            entity.Property(e => e.GpliItem)
                .HasComment("ID предмета")
                .HasColumnName("gpli_Item");
            entity.Property(e => e.GpliTime)
                .HasComment("Время покупки предмета")
                .HasColumnType("time")
                .HasColumnName("gpli_Time");

            entity.HasOne(d => d.GpliGamePlayerNavigation).WithMany(p => p.GamePlayerItems)
                .HasForeignKey(d => d.GpliGamePlayer)
                .HasConstraintName("FK_Game-Player-Item_Game-Player");

            entity.HasOne(d => d.GpliItemNavigation).WithMany(p => p.GamePlayerItems)
                .HasForeignKey(d => d.GpliItem)
                .HasConstraintName("FK_Game-Player-Item_Items");
        });

        modelBuilder.Entity<Hero>(entity =>
        {
            entity.HasKey(e => e.HId).HasName("PRIMARY");

            entity.ToTable("heroes");

            entity.HasIndex(e => e.HName, "UNQ_h_Name");

            entity.Property(e => e.HId)
                .HasComment("ID героя")
                .HasColumnName("h_ID");
            entity.Property(e => e.HAgility)
                .HasComment("Базовая ловкость")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_Agility");
            entity.Property(e => e.HAgilityPerLvl)
                .HasComment("Прирост ловкости за уровень")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_AgilityPerLVL");
            entity.Property(e => e.HArmor)
                .HasComment("Базовая броня")
                .HasColumnName("h_Armor");
            entity.Property(e => e.HAttackInterval)
                .HasComment("Базовый интервал атак")
                .HasColumnType("float(5,3) unsigned")
                .HasColumnName("h_AttackInterval");
            entity.Property(e => e.HAttackRange)
                .HasComment("Базовая дальность атаки")
                .HasColumnName("h_AttackRange");
            entity.Property(e => e.HAttackSpeed)
                .HasComment("Базовая скорость атаки")
                .HasColumnName("h_AttackSpeed");
            entity.Property(e => e.HAttackType)
                .HasComment("Тип атаки")
                .HasColumnType("enum('melee','range')")
                .HasColumnName("h_AttackType");
            entity.Property(e => e.HAttribute)
                .HasComment("Основной атрибут героя")
                .HasColumnType("enum('strength','agility','intelligence','universal')")
                .HasColumnName("h_Attribute");
            entity.Property(e => e.HDamage)
                .HasComment("Базовый урон")
                .HasColumnName("h_Damage");
            entity.Property(e => e.HDescription)
                .HasMaxLength(100)
                .HasComment("Описание героя")
                .HasColumnName("h_Description");
            entity.Property(e => e.HEvasion)
                .HasComment("Базовое уклонение")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("h_Evasion");
            entity.Property(e => e.HHp)
                .HasComment("Базовое значение очков здоровья")
                .HasColumnName("h_HP");
            entity.Property(e => e.HHpr)
                .HasComment("Базовое восстановление здоровья")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_HPR");
            entity.Property(e => e.HIntelligence)
                .HasComment("Базовый интелект")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_Intelligence");
            entity.Property(e => e.HIntelligencePerLvl)
                .HasComment("Прирост интелекта за уровень")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_IntelligencePerLVL");
            entity.Property(e => e.HMagicResist)
                .HasComment("Базовое сопротивление магии")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("h_MagicResist");
            entity.Property(e => e.HMoveSpeed)
                .HasComment("Базовая скорость передвижения")
                .HasColumnName("h_MoveSpeed");
            entity.Property(e => e.HMp)
                .HasComment("Базовое значение очков маны")
                .HasColumnName("h_MP");
            entity.Property(e => e.HMpr)
                .HasComment("Базовое восстановление очков маны")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_MPR");
            entity.Property(e => e.HName)
                .HasMaxLength(50)
                .HasComment("Имя героя")
                .HasColumnName("h_Name");
            entity.Property(e => e.HScepter)
                .HasMaxLength(5000)
                .HasComment("Описание Aghanim's Scepter")
                .HasColumnName("h_Scepter");
            entity.Property(e => e.HShard)
                .HasMaxLength(5000)
                .HasComment("Описание Aghanim's shard")
                .HasColumnName("h_Shard");
            entity.Property(e => e.HSpellAmp)
                .HasComment("Дополнительный урон заклинаний")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("h_SpellAMP");
            entity.Property(e => e.HStatusResist)
                .HasComment("Базовое сопротивление эффектам")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("h_StatusResist");
            entity.Property(e => e.HStrength)
                .HasComment("Базовая сила")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_Strength");
            entity.Property(e => e.HStrengthPerLvl)
                .HasComment("Прирост силы за уровень")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("h_StrengthPerLVL");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PRIMARY");

            entity.ToTable("items");

            entity.HasIndex(e => e.IName, "UNQ_i_Name");

            entity.Property(e => e.IId)
                .HasComment("ID предмета")
                .HasColumnName("i_ID");
            entity.Property(e => e.IAssemble)
                .HasComment("Собирается ли предмет из других предметов")
                .HasColumnName("i_Assemble");
            entity.Property(e => e.IDescription)
                .HasMaxLength(500)
                .HasColumnName("i_Description");
            entity.Property(e => e.IDisassemble)
                .HasComment("Можно ли предмет разобрать")
                .HasColumnName("i_Disassemble");
            entity.Property(e => e.IMaxCharges)
                .HasComment("Максимальное число зарядов")
                .HasColumnName("i_MaxCharges");
            entity.Property(e => e.IMaxStack)
                .HasComment("Максимальное количество предметов в одном слоте")
                .HasColumnName("i_MaxStack");
            entity.Property(e => e.IName)
                .HasMaxLength(50)
                .HasComment("Название предмета")
                .HasColumnName("i_Name");
            entity.Property(e => e.IPrice)
                .HasComment("Цена предмета")
                .HasColumnName("i_Price");
            entity.Property(e => e.ISpecifications)
                .HasMaxLength(500)
                .HasComment("Характеристики предмета")
                .HasColumnName("i_Specifications");
            entity.Property(e => e.IType)
                .HasComment("Тип предмета")
                .HasColumnType("enum('Consumables','Attributes','Equipment','Miscellaneous','Secret shop','Accessories','Support','Magical','Armor','Weapons','Artifacts')")
                .HasColumnName("i_Type");
        });

        modelBuilder.Entity<M2mHeroSkill>(entity =>
        {
            entity.HasKey(e => new { e.HsHero, e.HsSkill })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("m2m_hero-skill");

            entity.HasIndex(e => e.HsHero, "IXFK_m2m_Hero-Skill_Heroes");

            entity.HasIndex(e => e.HsSkill, "IXFK_m2m_Hero-Skill_Skills");

            entity.Property(e => e.HsHero)
                .HasComment("ID героя")
                .HasColumnName("hs_Hero");
            entity.Property(e => e.HsSkill)
                .HasComment("ID способности")
                .HasColumnName("hs_Skill");
            entity.Property(e => e.HsNumber)
                .HasComment("Номер способности у героя")
                .HasColumnName("hs_Number");

            entity.HasOne(d => d.HsHeroNavigation).WithMany(p => p.M2mHeroSkills)
                .HasForeignKey(d => d.HsHero)
                .HasConstraintName("FK_m2m_Hero-Skill_Heroes");

            entity.HasOne(d => d.HsSkillNavigation).WithMany(p => p.M2mHeroSkills)
                .HasForeignKey(d => d.HsSkill)
                .HasConstraintName("FK_m2m_Hero-Skill_Skills");
        });

        modelBuilder.Entity<M2mPlayerTeam>(entity =>
        {
            entity.HasKey(e => e.PltmId).HasName("PRIMARY");

            entity.ToTable("m2m_player-team");

            entity.HasIndex(e => e.PltmPlayer, "IXFK_m2m_Player-Team_Player");

            entity.HasIndex(e => e.PltmTeam, "IXFK_m2m_Player-Team_Teams");

            entity.Property(e => e.PltmId)
                .HasComment("ID записи")
                .HasColumnName("pltm_ID");
            entity.Property(e => e.PltmEndDate)
                .HasComment("Дата окончания выступления за команду")
                .HasColumnName("pltm_EndDate");
            entity.Property(e => e.PltmPlayer)
                .HasComment("ID игрока")
                .HasColumnName("pltm_Player");
            entity.Property(e => e.PltmRole)
                .HasComment("Роль игрока:\n	0 - Banch\n	1 - Safe lane\n	2 - Mid lane\n	3 - Off lane\n	4 - Soft support\n	5 - Hard support\n	6 - Coach")
                .HasColumnName("pltm_Role");
            entity.Property(e => e.PltmStartDate)
                .HasComment("Дата начала выступления за команду")
                .HasColumnName("pltm_StartDate");
            entity.Property(e => e.PltmTeam)
                .HasComment("ID команды")
                .HasColumnName("pltm_Team");

            entity.HasOne(d => d.PltmPlayerNavigation).WithMany(p => p.M2mPlayerTeams)
                .HasForeignKey(d => d.PltmPlayer)
                .HasConstraintName("FK_m2m_Player-Team_Player");

            entity.HasOne(d => d.PltmTeamNavigation).WithMany(p => p.M2mPlayerTeams)
                .HasForeignKey(d => d.PltmTeam)
                .HasConstraintName("FK_m2m_Player-Team_Teams");
        });

        modelBuilder.Entity<M2mTournamentTeam>(entity =>
        {
            entity.HasKey(e => new { e.TrtmTeam, e.TrtmTournament })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("m2m_tournament-team");

            entity.HasIndex(e => e.TrtmTeam, "IXFK_m2m_Tournament-Team_Teams");

            entity.HasIndex(e => e.TrtmTournament, "IXFK_m2m_Tournament-Team_Tournaments");

            entity.Property(e => e.TrtmTeam)
                .HasComment("ID команды")
                .HasColumnName("trtm_Team");
            entity.Property(e => e.TrtmTournament)
                .HasComment("ID турнира")
                .HasColumnName("trtm_Tournament");
            entity.Property(e => e.TrtmResult)
                .HasComment("Результат команды на турнире(место)")
                .HasColumnName("trtm_Result");

            entity.HasOne(d => d.TrtmTeamNavigation).WithMany(p => p.M2mTournamentTeams)
                .HasForeignKey(d => d.TrtmTeam)
                .HasConstraintName("FK_m2m_Tournament-Team_Teams");

            entity.HasOne(d => d.TrtmTournamentNavigation).WithMany(p => p.M2mTournamentTeams)
                .HasForeignKey(d => d.TrtmTournament)
                .HasConstraintName("FK_m2m_Tournament-Team_Tournaments");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MId).HasName("PRIMARY");

            entity.ToTable("match");

            entity.HasIndex(e => e.MWinner, "IXFK_Match_Teams");

            entity.Property(e => e.MId)
                .HasComment("ID матча")
                .HasColumnName("m_ID");
            entity.Property(e => e.MDataTime)
                .HasComment("Дата и вермя проведения матча")
                .HasColumnType("datetime")
                .HasColumnName("m_Data&Time");
            entity.Property(e => e.MWinner)
                .HasComment("Команда - победитель")
                .HasColumnName("m_Winner");

            entity.HasOne(d => d.MWinnerNavigation).WithMany(p => p.Matches)
                .HasForeignKey(d => d.MWinner)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Match_Teams");

            entity.HasMany(d => d.MtmTeams).WithMany(p => p.MtmMatches)
                .UsingEntity<Dictionary<string, object>>(
                    "M2mMatchTeam",
                    r => r.HasOne<Team>().WithMany()
                        .HasForeignKey("MtmTeam")
                        .HasConstraintName("FK_m2m_Match-Team_Teams"),
                    l => l.HasOne<Match>().WithMany()
                        .HasForeignKey("MtmMatch")
                        .HasConstraintName("FK_m2m_Match-Team_Match"),
                    j =>
                    {
                        j.HasKey("MtmMatch", "MtmTeam")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("m2m_match-team");
                        j.HasIndex(new[] { "MtmMatch" }, "IXFK_m2m_Match-Team_Match");
                        j.HasIndex(new[] { "MtmTeam" }, "IXFK_m2m_Match-Team_Teams");
                        j.IndexerProperty<uint>("MtmMatch")
                            .HasComment("ID матча")
                            .HasColumnName("mtm_Match");
                        j.IndexerProperty<uint>("MtmTeam")
                            .HasComment("ID команды")
                            .HasColumnName("mtm_Team");
                    });
        });

        modelBuilder.Entity<NeutralItem>(entity =>
        {
            entity.HasKey(e => e.NiId).HasName("PRIMARY");

            entity.ToTable("neutral item");

            entity.HasIndex(e => e.NiName, "UNQ_ni_Name");

            entity.Property(e => e.NiId)
                .HasComment("ID нейтрального предмета")
                .HasColumnName("ni_ID");
            entity.Property(e => e.NiDescription)
                .HasMaxLength(500)
                .HasComment("Описание нейтрального предмета")
                .HasColumnName("ni_Description");
            entity.Property(e => e.NiMaxCharges)
                .HasComment("Максимальое количество зарядов нейтрального предмета")
                .HasColumnName("ni_MaxCharges");
            entity.Property(e => e.NiName)
                .HasMaxLength(50)
                .HasComment("Название нейстрального предмета")
                .HasColumnName("ni_Name");
            entity.Property(e => e.NiSpecifications)
                .HasMaxLength(500)
                .HasComment("Характеристики нейтрального предмета")
                .HasColumnName("ni_Specifications");
            entity.Property(e => e.NiTier)
                .HasComment("Уровень предмета")
                .HasColumnName("ni_Tier");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.HasKey(e => e.OpId).HasName("PRIMARY");

            entity.ToTable("organizers");

            entity.HasIndex(e => e.OpName, "UNQ_op_Name");

            entity.Property(e => e.OpId)
                .HasComment("ID организатора")
                .HasColumnName("op_ID");
            entity.Property(e => e.OpName)
                .HasMaxLength(50)
                .HasComment("Название организатора")
                .HasColumnName("op_Name");
        });

        modelBuilder.Entity<PickBan>(entity =>
        {
            entity.HasKey(e => e.PbId).HasName("PRIMARY");

            entity.ToTable("pick-ban");

            entity.HasIndex(e => e.PbGame, "IXFK_Pick-Ban_Game");

            entity.HasIndex(e => e.PbHero, "IXFK_Pick-Ban_Heroes");

            entity.Property(e => e.PbId).HasColumnName("pb_ID");
            entity.Property(e => e.PbGame)
                .HasComment("ID игры")
                .HasColumnName("pb_Game");
            entity.Property(e => e.PbHero)
                .HasComment("ID героя")
                .HasColumnName("pb_Hero");
            entity.Property(e => e.PbStatus)
                .HasComment("Статус героя")
                .HasColumnType("enum('pick','ban')")
                .HasColumnName("pb_Status");

            entity.HasOne(d => d.PbGameNavigation).WithMany(p => p.PickBans)
                .HasForeignKey(d => d.PbGame)
                .HasConstraintName("FK_Pick-Ban_Game");

            entity.HasOne(d => d.PbHeroNavigation).WithMany(p => p.PickBans)
                .HasForeignKey(d => d.PbHero)
                .HasConstraintName("FK_Pick-Ban_Heroes");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlId).HasName("PRIMARY");

            entity.ToTable("player");

            entity.HasIndex(e => e.PlCountry, "IXFK_Player_Country");

            entity.HasIndex(e => e.PlNickname, "UNQ_pl_NickName").IsUnique();

            entity.Property(e => e.PlId)
                .HasComment("ID игрока")
                .HasColumnName("pl_ID");
            entity.Property(e => e.PlCountry)
                .HasComment("Страна игрока")
                .HasColumnName("pl_Country");
            entity.Property(e => e.PlFirstName)
                .HasMaxLength(50)
                .HasComment("Имя игрока")
                .HasColumnName("pl_FirstName");
            entity.Property(e => e.PlLastName)
                .HasMaxLength(50)
                .HasComment("Фамилия игрока")
                .HasColumnName("pl_LastName");
            entity.Property(e => e.PlMmr)
                .HasComment("Match Making Rating")
                .HasColumnName("pl_MMR");
            entity.Property(e => e.PlNickname)
                .HasMaxLength(50)
                .HasComment("Игровой Nickname ")
                .HasColumnName("pl_Nickname");
            entity.Property(e => e.PlRang)
                .HasComment("Ранг игрока в мировом топе")
                .HasColumnName("pl_Rang");

            entity.HasOne(d => d.PlCountryNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.PlCountry)
                .HasConstraintName("FK_Player_Country");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkId).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.SkName, "UNQ_sk_Name");

            entity.Property(e => e.SkId)
                .HasComment("ID способности")
                .HasColumnName("sk_ID");
            entity.Property(e => e.SkDescription)
                .HasMaxLength(5000)
                .HasComment("Описание способности")
                .HasColumnName("sk_Description");
            entity.Property(e => e.SkHealthCost)
                .HasComment("Стоимость здоровья")
                .HasColumnName("sk_HealthCost");
            entity.Property(e => e.SkManaCost)
                .HasComment("Стоимость маны")
                .HasColumnName("sk_ManaCost");
            entity.Property(e => e.SkMaxLvl)
                .HasComment("Максимальный уровень способности")
                .HasColumnName("sk_MaxLVL");
            entity.Property(e => e.SkName)
                .HasMaxLength(50)
                .HasComment("Название способности")
                .HasColumnName("sk_Name");
            entity.Property(e => e.SkReaload)
                .HasComment("Перезарядка способности")
                .HasColumnType("float(4,1) unsigned")
                .HasColumnName("sk_Reaload");
            entity.Property(e => e.SkSpecifications)
                .HasMaxLength(500)
                .HasComment("Характеристики способности")
                .HasColumnName("sk_Specifications");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StId).HasName("PRIMARY");

            entity.ToTable("studio");

            entity.HasIndex(e => e.StName, "UNQ_st_Name");

            entity.Property(e => e.StId)
                .HasComment("ID студии освещения")
                .HasColumnName("st_ID");
            entity.Property(e => e.StName)
                .HasMaxLength(50)
                .HasComment("Название студии")
                .HasColumnName("st_Name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TmId).HasName("PRIMARY");

            entity.ToTable("teams");

            entity.Property(e => e.TmId)
                .HasComment("ID команды")
                .HasColumnName("tm_ID");
            entity.Property(e => e.TmName)
                .HasMaxLength(50)
                .HasComment("Название команды")
                .HasColumnName("tm_Name");
            entity.Property(e => e.TmRating)
                .HasComment("Рейтинг команды(PTS)")
                .HasColumnName("tm_Rating");
        });

        modelBuilder.Entity<TeamRang>(entity =>
        {
            entity.HasKey(e => e.TmrgId).HasName("PRIMARY");

            entity.ToTable("team-rang");

            entity.HasIndex(e => e.TmrgTeam, "IXFK_Team-Rang_Teams").IsUnique();

            entity.Property(e => e.TmrgId).HasColumnName("tmrg_ID");
            entity.Property(e => e.TmrgRang).HasColumnName("tmrg_Rang");
            entity.Property(e => e.TmrgTeam).HasColumnName("tmrg_Team");

            entity.HasOne(d => d.TmrgTeamNavigation).WithOne(p => p.TeamRang)
                .HasForeignKey<TeamRang>(d => d.TmrgTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Team-Rang_Teams");
        });

        modelBuilder.Entity<TeamRating>(entity =>
        {
            entity.HasKey(e => e.TmrId).HasName("PRIMARY");

            entity.ToTable("team-rating");

            entity.HasIndex(e => e.TmrTeam, "IXFK_Team-Rating_Teams");

            entity.Property(e => e.TmrId).HasColumnName("tmr_ID");
            entity.Property(e => e.TmrDate)
                .HasComment("Дата добавления очков")
                .HasColumnName("tmr_Date");
            entity.Property(e => e.TmrRatingAdd)
                .HasComment("Сколько очков добавлено")
                .HasColumnName("tmr_RatingAdd");
            entity.Property(e => e.TmrTeam)
                .HasComment("ID команды")
                .HasColumnName("tmr_Team");

            entity.HasOne(d => d.TmrTeamNavigation).WithMany(p => p.TeamRatings)
                .HasForeignKey(d => d.TmrTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Team-Rating_Teams");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TrId).HasName("PRIMARY");

            entity.ToTable("tournaments");

            entity.HasIndex(e => e.TrOrganizer, "IXFK_Tournaments_Organizers");

            entity.HasIndex(e => e.TrName, "UNQ_tr_Name");

            entity.Property(e => e.TrId)
                .HasComment("ID турнира")
                .HasColumnName("tr_ID");
            entity.Property(e => e.TrEndDate)
                .HasComment("Дата окончания турнираы")
                .HasColumnName("tr_EndDate");
            entity.Property(e => e.TrName)
                .HasMaxLength(50)
                .HasComment("Название турнира")
                .HasColumnName("tr_Name");
            entity.Property(e => e.TrOrganizer)
                .HasComment("ID организатора")
                .HasColumnName("tr_Organizer");
            entity.Property(e => e.TrParticipants)
                .HasComment("Число команд-участников на турнире")
                .HasColumnName("tr_Participants");
            entity.Property(e => e.TrPlace)
                .HasMaxLength(50)
                .HasComment("Место проведения турнира: Страна, город")
                .HasColumnName("tr_Place");
            entity.Property(e => e.TrPrize)
                .HasComment("Призовой фонд($)")
                .HasColumnName("tr_Prize");
            entity.Property(e => e.TrStartDate)
                .HasComment("Дата начала турнира")
                .HasColumnName("tr_StartDate");
            entity.Property(e => e.TrTier)
                .HasComment("Уровень турнира(зависит от рейтинга играющих на турнире команд)")
                .HasColumnName("tr_Tier");

            entity.HasOne(d => d.TrOrganizerNavigation).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.TrOrganizer)
                .HasConstraintName("FK_Tournaments_Organizers");

            entity.HasMany(d => d.TrmMatches).WithMany(p => p.TrmTounaments)
                .UsingEntity<Dictionary<string, object>>(
                    "M2mTournamentMatch",
                    r => r.HasOne<Match>().WithMany()
                        .HasForeignKey("TrmMatch")
                        .HasConstraintName("FK_m2m_Tournament-Match_Match"),
                    l => l.HasOne<Tournament>().WithMany()
                        .HasForeignKey("TrmTounament")
                        .HasConstraintName("FK_m2m_Tournament-Match_Tournaments"),
                    j =>
                    {
                        j.HasKey("TrmTounament", "TrmMatch")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("m2m_tournament-match");
                        j.HasIndex(new[] { "TrmMatch" }, "IXFK_m2m_Tournament-Match_Match");
                        j.HasIndex(new[] { "TrmTounament" }, "IXFK_m2m_Tournament-Match_Tournaments");
                        j.IndexerProperty<uint>("TrmTounament")
                            .HasComment("ID турнира")
                            .HasColumnName("trm_Tounament");
                        j.IndexerProperty<uint>("TrmMatch")
                            .HasComment("ID матча")
                            .HasColumnName("trm_Match");
                    });

            entity.HasMany(d => d.TrstStudios).WithMany(p => p.TrstTournaments)
                .UsingEntity<Dictionary<string, object>>(
                    "M2mTournamentStudio",
                    r => r.HasOne<Studio>().WithMany()
                        .HasForeignKey("TrstStudio")
                        .HasConstraintName("FK_m2m_Tournament-Studio_Studio"),
                    l => l.HasOne<Tournament>().WithMany()
                        .HasForeignKey("TrstTournament")
                        .HasConstraintName("FK_m2m_Tournament-Studio_Tournaments"),
                    j =>
                    {
                        j.HasKey("TrstTournament", "TrstStudio")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("m2m_tournament-studio");
                        j.HasIndex(new[] { "TrstStudio" }, "IXFK_m2m_Tournament-Studio_Studio");
                        j.HasIndex(new[] { "TrstTournament" }, "IXFK_m2m_Tournament-Studio_Tournaments");
                        j.IndexerProperty<uint>("TrstTournament")
                            .HasComment("ID турнира")
                            .HasColumnName("trst_Tournament");
                        j.IndexerProperty<uint>("TrstStudio")
                            .HasComment("ID студии")
                            .HasColumnName("trst_Studio");
                    });
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UId).HasName("PRIMARY");

            entity.ToTable("units");

            entity.HasIndex(e => e.UName, "UNQ_u_Name");

            entity.Property(e => e.UId)
                .HasComment("ID сущности")
                .HasColumnName("u_ID");
            entity.Property(e => e.UArmor)
                .HasComment("Базовая броня")
                .HasColumnName("u_Armor");
            entity.Property(e => e.UAttackInterval)
                .HasComment("Базовый интервал атак")
                .HasColumnType("float(5,3) unsigned")
                .HasColumnName("u_AttackInterval");
            entity.Property(e => e.UAttackRange)
                .HasComment("Базовая дальность атаки")
                .HasColumnName("u_AttackRange");
            entity.Property(e => e.UAttackSpeed)
                .HasComment("Базовая скорость атаки")
                .HasColumnName("u_AttackSpeed");
            entity.Property(e => e.UAttackType)
                .HasComment("Тип атаки")
                .HasColumnType("enum('melee','reange')")
                .HasColumnName("u_AttackType");
            entity.Property(e => e.UDamage)
                .HasComment("Базовый урон")
                .HasColumnName("u_Damage");
            entity.Property(e => e.UDescription)
                .HasMaxLength(100)
                .HasComment("Описание сущности")
                .HasColumnName("u_Description");
            entity.Property(e => e.UExperience)
                .HasComment("Опыт за убийство")
                .HasColumnName("u_Experience");
            entity.Property(e => e.UGold)
                .HasComment("Золото за убийство")
                .HasColumnName("u_Gold");
            entity.Property(e => e.UHp)
                .HasComment("Базовое значение очков здоровья")
                .HasColumnName("u_HP");
            entity.Property(e => e.UHpr)
                .HasComment("Базовое восстановление здоровья")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("u_HPR");
            entity.Property(e => e.UMagicResist)
                .HasComment("Базовое сопротивление магии")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("u_MagicResist");
            entity.Property(e => e.UMoveSpeed)
                .HasComment("Базовая скорость передвижения")
                .HasColumnName("u_MoveSpeed");
            entity.Property(e => e.UMp)
                .HasComment("Базовое значение очков маны")
                .HasColumnName("u_MP");
            entity.Property(e => e.UMpr)
                .HasComment("Базовое восстановление очков маны")
                .HasColumnType("float(5,1) unsigned")
                .HasColumnName("u_MPR");
            entity.Property(e => e.UName)
                .HasMaxLength(50)
                .HasComment("Имя сущности")
                .HasColumnName("u_Name");
            entity.Property(e => e.USpellAmp)
                .HasComment("Дополнительный урон заклинаний")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("u_SpellAMP");
            entity.Property(e => e.UStatusResist)
                .HasComment("Базовое сопротивление эффектам")
                .HasColumnType("float(4,3) unsigned")
                .HasColumnName("u_StatusResist");
            entity.Property(e => e.UType)
                .HasComment("Тип сущности")
                .HasColumnType("enum('structures','lane creeps','small creeps','medium creeps','large creeps','ancient creeps','roshan','tormentor','observer')")
                .HasColumnName("u_Type");
        });

        modelBuilder.Entity<UnitSkill>(entity =>
        {
            entity.HasKey(e => new { e.UsSkill, e.UsUnit })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("unit-skill");

            entity.HasIndex(e => e.UsSkill, "IXFK_Unit-Skill_Skills");

            entity.HasIndex(e => e.UsUnit, "IXFK_Unit-Skill_Units");

            entity.Property(e => e.UsSkill)
                .HasComment("ID способности")
                .HasColumnName("us_Skill");
            entity.Property(e => e.UsUnit)
                .HasComment("ID сущности")
                .HasColumnName("us_Unit");
            entity.Property(e => e.UsNumer)
                .HasComment("Номер способности")
                .HasColumnName("us_Numer");

            entity.HasOne(d => d.UsSkillNavigation).WithMany(p => p.UnitSkills)
                .HasForeignKey(d => d.UsSkill)
                .HasConstraintName("FK_Unit-Skill_Skills");

            entity.HasOne(d => d.UsUnitNavigation).WithMany(p => p.UnitSkills)
                .HasForeignKey(d => d.UsUnit)
                .HasConstraintName("FK_Unit-Skill_Units");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
