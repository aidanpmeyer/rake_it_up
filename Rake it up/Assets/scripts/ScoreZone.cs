using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreZone : MonoBehaviour

{
    public string targetTag;
    public int numLeaves = 0;
    public TMP_Text scoreText;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.CompareTag(targetTag))
        {
            numLeaves++;
            UpdateScoreText();
            Debug.Log("leaf enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            numLeaves--;
            UpdateScoreText();
            Debug.Log("leaf exit");
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + numLeaves.ToString();
    }
}


