


using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRuntimeCardData : RuntimeCardData {

    private int attack;
    public int Attack { get { return attack; } set { attack = value;  OnAttackChanged?.Invoke(this);  } }

    private int health;
    public int Health { get { return health; } set { health = value; OnHealthChanged?.Invoke(this, (health <= 0) ); } }

    private int attacksPerTurn;
    public int AttacksPerTurn { get { return this.attacksPerTurn; } set { this.attacksPerTurn = value; OnAttacksPerTurnChanged?.Invoke(); } }

    public HashSet<CreatureType> Types { get; set; } = new();
    public HashSet<Keyword> Keywords { get; set; } = new();

    public event Action<CreatureRuntimeCardData, bool> OnHealthChanged;
    public event Action<CreatureRuntimeCardData> OnAttackChanged;
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

    public void PerformAttack (CreatureRuntimeCardData defendingCreature) {

        if (this.AttacksPerTurn <= 0) return;

        this.AttacksPerTurn--;

        this.Health -= defendingCreature.Attack;
        defendingCreature.Health -= this.Attack;
    }

    public void ResetAttacksPerTurn () {

        AttacksPerTurn = 1;
    }
}