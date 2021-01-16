using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{ //This script is to change the color of the gameobject , in this case to blue
    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
