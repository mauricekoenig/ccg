using System;
using UnityEngine;

public class PlayerResources : MonoBehaviour {


    public int currentMana;
    public int maxMana;
    public int temporaryMana;

    private Player player;

    public event Action<PlayerResources> PlayerResourcesChanged;

    public void Init (Player player) {

        maxMana = 10;
        temporaryMana = 0;
        this.player = player;
    }

    public void Add (int amount) {
        currentMana += amount;
    }

    public void Remove (int amount) {
        currentMana -= amount;
    }

    public void Invoke_PlayerResourcesChanged () {

        PlayerResourcesChanged?.Invoke(this);
    }
}