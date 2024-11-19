





using UnityEngine;

[CreateAssetMenu(fileName = "New Berserk Keyword", menuName = "Keywords/Berserk", order = 2)]
public class Keyword_Berserk : Keyword {

    public override void Apply (CreatureRuntimeCardData creatureData) {
        creatureData.Keywords.Add(this);
    }
}