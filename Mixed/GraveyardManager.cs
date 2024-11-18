


using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardManager : MonoBehaviour, IGraveyardManager {

    private Dictionary<int, List<RuntimeCardData>> Lists = new();

    public List<RuntimeCardData> GetCards (int id) {
        return Lists[id];
    }
}