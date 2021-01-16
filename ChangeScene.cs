using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// Dont forget to call the library "UnityEngine.SceneManagement" 
                                  //without this you cant acess the parameter


public class ChangeScene : MonoBehaviour
{ //This function is called to change scene 
  public void levelBowling()
    {
        SceneManager.LoadScene("vrbowling");
    }
}
