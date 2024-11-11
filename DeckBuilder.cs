using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    IPreLoader preLoader;
    public GameData gameData;

    private void Awake() {
        preLoader = GetComponent<IPreLoader>();
    }

    private void Start() {
        preLoader.Run(PreLoaderAction.LoadAllCreatures);
    }

    public void LoadCollectionElements () {

        var cards = gameData.GetAllCards();
    }
}
