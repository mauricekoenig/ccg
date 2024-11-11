


using System.Collections.Generic;
using UnityEngine;

public class CreatureRuntimeCardData : RuntimeCardData {

    public int Attack { get; set; }
    public int Health { get; set; }
    public HashSet<CreatureType> Types { get; set; }

    public CreatureRuntimeCardData (int id, string name, int cost, string artworkBase64, int attack, int health) : base (id, name, cost, artworkBase64) {

        Attack = attack;
        Health = health;
    }

    public override string ToString() {
        return $"Name: {this.Name}, Cost: {this.Cost}";
    }
}