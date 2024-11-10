


using System.Collections.Generic;
using UnityEngine;

public class CreatureData : BaseCardData {

    public int Attack { get; set; }
    public int Health { get; set; }
    public HashSet<CreatureType> Types { get; set; }

    public CreatureData (int id, string name, int cost, string artworkBase64, int attack, int health) : base (id, name, cost, artworkBase64) {

        Attack = attack;
        Health = health;
    }
}