


using System.Threading.Tasks;
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

    private void Start() {

        if (RuntimeGameData.Initialized) {

            Debug.Log("Daten sind schon geladen!");
            return;
        }

        gameData.Init();

        preLoader.Run(PreLoaderAction.LoadAllCreatures);
        preLoader.Run(PreLoaderAction.LoadAllEffects);
        preLoader.Run(PreLoaderAction.LoadAllVillains);
        preLoader.Run(PreLoaderAction.LoadAllCardSprites);
    }

    public void ChangeScene (int buildIndex) {

        SceneManager.LoadScene(buildIndex);
    }
}