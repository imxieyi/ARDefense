using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    public SubWave[] subWaves;

}

[System.Serializable]
public class SubWave {

    public GameObject enemy;
    public int count;
    public float rate;

}
