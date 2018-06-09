using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public Text levelText;
    public Text damageText;
    public Text speedText;
    public Text rangeText;

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

}
