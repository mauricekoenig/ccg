



using UnityEngine;

[CreateAssetMenu]

public class Villain : ScriptableObject, IIdentifiable {

    public int id;
    public string Name;
    public int health;
    public Sprite artwork;

    public BaseVillainAbility ability;

    public int ID => id;
}