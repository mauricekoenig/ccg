


using System.Collections.Generic;
using UnityEngine;

public class LocationRuntimeCardData : RuntimeCardData {


    public LocationRuntimeCardData (int id, string name, int cost, string artworkBase64, int attack, int health) : base(id, name, cost, artworkBase64) {

    }

    public override RuntimeCardData Clone() {
        return null;
    }
}