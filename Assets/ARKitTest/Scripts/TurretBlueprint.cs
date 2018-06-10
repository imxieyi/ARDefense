using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretLevel {
    public int cost;
    public int damage;
    public float speed;
    public int range;
}

[System.Serializable]
public class TurretBlueprint {

    public GameObject prefab;
    [HideInInspector]
    public int cost { get { return levels[0].cost; } }
    public TurretLevel[] levels;
};

