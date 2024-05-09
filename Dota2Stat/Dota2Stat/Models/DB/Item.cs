using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class Item
{
    /// <summary>
    /// ID предмета
    /// </summary>
    public uint IId { get; set; }

    /// <summary>
    /// Название предмета
    /// </summary>
    public string? IName { get; set; }

    public string? IDescription { get; set; }

    /// <summary>
    /// Цена предмета
    /// </summary>
    public ushort? IPrice { get; set; }

    /// <summary>
    /// Тип предмета
    /// </summary>
    public string? IType { get; set; }

    /// <summary>
    /// Максимальное число зарядов
    /// </summary>
    public byte? IMaxCharges { get; set; }

    /// <summary>
    /// Максимальное количество предметов в одном слоте
    /// </summary>
    public byte? IMaxStack { get; set; }

    /// <summary>
    /// Можно ли предмет разобрать
    /// </summary>
    public bool? IDisassemble { get; set; }

    /// <summary>
    /// Собирается ли предмет из других предметов
    /// </summary>
    public bool? IAssemble { get; set; }

    /// <summary>
    /// Характеристики предмета
    /// </summary>
    public string? ISpecifications { get; set; }

    public virtual ICollection<AssemblingItem> AssemblingItemAiCraftedItemNavigations { get; set; } = new List<AssemblingItem>();

    public virtual ICollection<AssemblingItem> AssemblingItemAiIngredientItemNavigations { get; set; } = new List<AssemblingItem>();

    public virtual ICollection<GamePlayerItem> GamePlayerItems { get; set; } = new List<GamePlayerItem>();
}
