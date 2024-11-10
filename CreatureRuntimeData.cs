
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreatureRuntimeData : CardRuntimeData, ICreature {

    [Space]
    [Header("Creature Specific")]
    [Space]
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private List<CreatureType> types = new();

    public int Attack { get => attack; set => attack = value; }
    public int Health { get => health; set => health = value; }
    public List<CreatureType> Types { get => types; set => types = value; }
}