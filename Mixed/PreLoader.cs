



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
        this.gameData.Init();
    }
    private void Start() {

        this.databaseService.OnSelectAllCreatures += Handler_OnSelectAllCreatures;
    }

    public async Task Run (PreLoaderAction action) {

        switch (action) {

            case PreLoaderAction.LoadAllCreatures:
                await databaseService.SelectAllCreatures();
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

        }
    }

    public void Handler_OnSelectAllCreatures(HashSet<CreatureRuntimeCardData> data) {

        gameData.runtimeGameData.AssignCreatures(data);
    }

}

public enum PreLoaderAction {

    LoadAllCreatures,
    LoadAllEffects,
    LoadAllCardSprites,
    LoadAllVillains
}