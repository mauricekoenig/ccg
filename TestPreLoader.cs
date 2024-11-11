



using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TestDatabaseService))]
public class TestPreLoader : MonoBehaviour, IPreLoader {

    public GameData gameData;
    private IDataBaseService databaseService;

    private void Awake() {
        
        databaseService = GetComponent<IDataBaseService>();
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
                await gameData.LoadAllEffects();
                break;
        }
    }

    public void Handler_OnSelectAllCreatures(HashSet<CreatureRuntimeCardData> data) {

        gameData.AssignCreatures(data);
    }

}

public enum PreLoaderAction {

    LoadAllCreatures,
    LoadAllEffects
}