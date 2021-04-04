using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const float minTimeDiff = 3f;
    private float timeScale = 1f;
    private float lastUsage = 0f;
    [SerializeField]
    private TextMeshProUGUI timeScaleIndicator;

    public void toggleSlowMotion() {
        if (Time.time - lastUsage < minTimeDiff) {
            return;
        }
        lastUsage = Time.time;
        timeScale = timeScale == 1f ? 0.5f : 1f;
        Time.timeScale = timeScale;
        Debug.Log("Timescale changed to " + timeScale);
    }

    private void FixedUpdate() {
        timeScaleIndicator.text = generateUIString();
    }

    private string generateUIString() {
        string str = "Time = " + Time.time;
        str += "\nCurrent timeScale = " + timeScale;
        str += "\nCan toggle slowMotion = " + (Time.time - lastUsage > minTimeDiff ? "true" : "false");
        return str;
    }
}
