using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
