using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour {

    public int reward = 1;
    public Animator animator;

    public void OnTouch() {
        animator.Play("Bump");
        PlayerStats.Money += reward;
    }

}
