


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(PreLoader))]
public class MainMenu : MonoBehaviour {

    public GameData gameData;
    public IPreLoader preLoader;

    private void Awake() {

        preLoader = GetComponent<PreLoader>();
    }

    public async void LoadAllData () {

        if (this.gameData.Initialized()) return;
        await preLoader.Run(PreLoaderAction.LoadAllCreatures);
        LoadResources();
    }

    private void LoadResources () {

        preLoader.Run(PreLoaderAction.LoadAllEffects);
        preLoader.Run(PreLoaderAction.LoadAllVillains);
        preLoader.Run(PreLoaderAction.LoadAllCardSprites);
        this.gameData.runtimeGameData.initialized = true;
    }

    public void ChangeScene (int buildIndex) {

        LoadAllData();
        SceneManager.LoadScene(buildIndex);
    }
}