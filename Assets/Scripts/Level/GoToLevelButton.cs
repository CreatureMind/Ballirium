
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevelButton : MonoBehaviour
{
    [SerializeField] private string goToLevel1 = "Kitchen";
    [SerializeField] private string goToLevel2 = "BedRoom";
    public void GoToLevel1()
    {
        SceneManager.LoadScene(goToLevel1);
        //Debug.Log("Button 1 Pressed");
    }
    
    public void GoToLevel2()
    {
        //SceneManager.LoadScene(goToLevel2);
        Debug.Log("Button 2 Pressed");
    }
}
    

