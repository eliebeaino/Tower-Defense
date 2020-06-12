using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float baseHealth = 10;

    public void ProcessDmg(float damagetaken)
    {
        baseHealth -= damagetaken;
    }
}
