using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour {
    
    float deltaTime = 0.0f;
    public Text fpsText;

    void Start() {
#if !DEVELOPMENT_BUILD
        gameObject.SetActive(false);
#endif
    }

    void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }

}