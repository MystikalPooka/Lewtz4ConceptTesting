using System;

namespace LewtzTesting.Data_Structure
{
    [Flags()]
    public enum ItemTypes
    {
        None = 0,
        Magic = 1,
        Armor = 1 << 1,
        Shield = 1 << 2 | Armor,
        Melee_Weapon = 1 << 3,
        Ranged_Weapon = 1 << 4,
        Potion = 1 << 5,
        Ring = 1 << 6,
        Rod = 1 << 7,
        Scroll = 1 << 8,
        Staff = 1 << 9,
        Wand = 1 << 10,
        Wondrous = 1 << 11,
        Salvage = 1 << 12,
        Alchemical = 1 << 13,
        Alcohol = 1 << 14,
        Tool = 1 << 15,
        Gem = 1 << 16,
        Art = 1 << 17,
        Ability = 1 << 18,
        Weapon = Melee_Weapon | Ranged_Weapon
    }
}
