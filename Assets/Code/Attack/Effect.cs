using Code;
using System;
using UnityEngine;
[Serializable]
public class Effect : IComparable<Effect>
{
    [SerializeField] public EffectType effectType;
    [SerializeField] public float duration;
    [SerializeField] public int level;

    public int CompareTo(Effect other)
    {
        return other.level.CompareTo(level);
    }

    public Effect(EffectType ef, float dur, int lvl){
        effectType = ef;
        duration = dur;
        level = lvl;
    }
}

