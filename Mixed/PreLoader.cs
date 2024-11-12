



using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TestDatabaseService))]
public class PreLoader : MonoBehaviour, IPreLoader {

    public GameData gameData;
    private IDataBaseService databaseService;

    private void Awake() {
        
        databaseService = GetComponent<IDataBaseService>();
        if (RuntimeGameData.Initialized) return;
        this.databaseService.OnSelectAllCreatures += Handler_OnSelectAllCreatures;
        this.databaseService.OnSelectAllDecks += Handler_OnSelectAllDecks;
    }

    public void Run (PreLoaderAction action) {

        switch (action) {

            case PreLoaderAction.LoadAllCreatures:
                databaseService.SelectAllCreatures();
                break;

            case PreLoaderAction.LoadAllEffects:
                gameData.runtimeGameData.LoadAllEffects();
                break;

            case PreLoaderAction.LoadAllCardSprites:
                gameData.runtimeGameData.LoadAllSprites();
                break;

            case PreLoaderAction.LoadAllVillains:
                gameData.runtimeGameData.LoadAllVillains();
                break;

            case PreLoaderAction.LoadAllDecks:
                databaseService.SelectAllDecks();
                break;

        }
    }

    public void Handler_OnSelectAllCreatures(HashSet<CreatureRuntimeCardData> data) {

        gameData.runtimeGameData.AssignCreatures(data);
    }

    public void Handler_OnSelectAllDecks (HashSet<DeckMetaData> metaData) {

        gameData.runtimeGameData.AssignDeckMetaData(metaData);
        Debug.Log("PreLoader: Handler_OnSelectAllDecks - Deck Count: " + metaData.Count);
    }
}

public enum PreLoaderAction {

    LoadAllCreatures,
    LoadAllEffects,
    LoadAllCardSprites,
    LoadAllVillains,
    LoadAllDecks
}