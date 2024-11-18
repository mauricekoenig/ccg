


using System.Collections.Generic;
using UnityEngine;

public class SpellRuntimeCardData : RuntimeCardData {


    public SpellRuntimeCardData (int id, string name, int cost, string artworkBase64, int attack, int health, string color) : base(id, name, cost, artworkBase64, color) {

    }

    public override RuntimeCardData Clone() {
        return null;
    }
}