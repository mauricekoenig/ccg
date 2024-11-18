


using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRuntimeCardData : RuntimeCardData {

    public int Attack { get; set; }
    public int Health { get; set; }
    public int AttacksPerTurn { get; set; }

    public HashSet<CreatureType> Types { get; set; } = new();
    public HashSet<Keyword> Keywords { get; set; } = new();

    public event Action OnAttacksPerTurnChanged;

    public CreatureRuntimeCardData (int id, string name, int cost, string artworkBase64, int attack, int health) : base (id, name, cost, artworkBase64) {

        Attack = attack;
        Health = health;
        AttacksPerTurn = 0;
    }
    public CreatureRuntimeCardData(int id, string name, int cost, Sprite artwork, int attack, int health) : base(id, name, cost, artwork) {

        Attack = attack;
        Health = health;
        AttacksPerTurn = 0;
    }

    public override string ToString() {
        return $"Name: {this.Name}, Cost: {this.Cost}";
    }

    public override RuntimeCardData Clone() {

        return new CreatureRuntimeCardData(this.ID, this.Name, this.Cost, this.Artwork, this.Attack, this.Health);
    }

    public string GetKeywordString () {

        string s = "";
        foreach (var keyword in Keywords) {
            Debug.Log("Keyword: " + keyword.Type.ToString());
            s += keyword.Type.ToString() + " ";
        } return s;
;
    }

    public void PerformAttack () {
        if (AttacksPerTurn <= 0) return;
        AttacksPerTurn--;
        OnAttacksPerTurnChanged?.Invoke();
    }

    public void ResetAttacksPerTurn () {

        AttacksPerTurn = 1;
        OnAttacksPerTurnChanged?.Invoke();
    }
}