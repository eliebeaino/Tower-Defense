using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBase : MonoBehaviour
{
    public float baseHealth = 10;
    [SerializeField] TextMeshProUGUI baseHealthText;
    [SerializeField] TextMeshProUGUI EnemiesAliveText;

    private void Start()
    {
        baseHealthText.text = baseHealth.ToString();
    }

    private void Update()
    {
        EnemiesAlive();
    }

    // reduce health based on damage taken
    public void ProcessDmg(float damagetaken)
    {
        baseHealth -= damagetaken;
        baseHealthText.text = baseHealth.ToString();
    }

    public void EnemiesAlive()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();
        EnemiesAliveText.text = sceneEnemies.Length.ToString();
    }    
}
