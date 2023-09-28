using GameDevTV.Inventories;
using RPGame.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEquiptment : Equipment, IModifierProvider
{
    public IEnumerable<float> GetAddictiveModifier(Stat stat)
    {
        foreach (var slot in GetAllPopulatedSlots())
        {
            var item = GetItemInSlot(slot) as IModifierProvider;
            if (item == null) continue;
            foreach (float modifier in item.GetAddictiveModifier(stat))
            {
                yield return modifier;
            }
        }
    }

    public IEnumerable<float> GetPercentageModifier(Stat stat)
    {
        foreach (var slot in GetAllPopulatedSlots())
        {
            var item = GetItemInSlot(slot) as IModifierProvider;
            if (item == null) continue;
            foreach (float modifier in item.GetPercentageModifier(stat))
            {
                yield return modifier;
            }
        }
    }
}
