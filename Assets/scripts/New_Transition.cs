using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class New_Transition : MonoBehaviour
{
    public void Transition(int number)
    {
        SceneManager.LoadScene(number);
    }
}
