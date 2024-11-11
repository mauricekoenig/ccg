
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public Villain TestVillain;
    public RuntimeGameData runtimeGameData;

    public void Init () {

        runtimeGameData = new RuntimeGameData(this);
    }

    public bool Initialized () {

        return this.runtimeGameData.initialized;
    }
}