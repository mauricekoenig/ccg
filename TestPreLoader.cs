



using System.Collections.Generic;
using System.Data;
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

    public void Run (PreLoaderAction action) {

        switch (action) {

            case PreLoaderAction.LoadAllCreatures:
                databaseService.SelectAllCreatures();
                break;
        }
    }
    public void Handler_OnSelectAllCreatures(HashSet<CreatureData> data) {

        gameData.AssignCreatures(data);
        Debug.Log("LOADED CREATURES");
    }

}

public enum PreLoaderAction {

    LoadAllCreatures
}