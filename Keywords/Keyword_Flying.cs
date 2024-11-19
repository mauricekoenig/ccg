



using UnityEngine;

[CreateAssetMenu(fileName = "New Flying Keyword", menuName = "Keywords/Flying", order = 1)]
public class Keyword_Flying : Keyword {

    public override void Apply (CreatureRuntimeCardData creatureData) {
        creatureData.Keywords.Add(this);
    }
}