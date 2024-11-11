



using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DataCollection <T> : ScriptableObject where T : IIdentifiable {

    public List<T> Data = new();

    public T GetById (int id) {
        return Data.Where(x => x.ID == id).FirstOrDefault();
    }
}