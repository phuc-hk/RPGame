using GameDevTV.Inventories;
using RPGame.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPGame/Inventory/Stat Equipable Item"))]
public class StatEquiptableItem : EquipableItem, IModifierProvider
{
    [SerializeField] Modifier[] AddictiveModifiers;
    [SerializeField] Modifier[] PercentageModifiers;

    public IEnumerable<float> GetAddictiveModifier(Stat stat)
    {
        foreach (var modifier in AddictiveModifiers)
        {
            if (modifier.stat == stat)
            {
                yield return modifier.value;
            }
        }
    }

    public IEnumerable<float> GetPercentageModifier(Stat stat)
    {
        foreach (var modifier in PercentageModifiers)
        {
            if (modifier.stat == stat)
            {
                yield return modifier.value;
            }
        }
    }

    [System.Serializable]
    struct Modifier
    {
        public Stat stat;
        public float value;
    }
}
