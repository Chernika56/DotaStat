using System;
using System.Collections.Generic;

namespace Dota2Stat.Models.DB;

public partial class AssemblingItem
{
    /// <summary>
    /// ID предмета, который создается
    /// </summary>
    public uint AiCraftedItem { get; set; }

    /// <summary>
    /// ID предмета, который используется для создания
    /// </summary>
    public uint AiIngredientItem { get; set; }

    /// <summary>
    /// Количество предметов, необходимых для создания
    /// </summary>
    public byte? AiQuantity { get; set; }

    public virtual Item AiCraftedItemNavigation { get; set; } = null!;

    public virtual Item AiIngredientItemNavigation { get; set; } = null!;
}
