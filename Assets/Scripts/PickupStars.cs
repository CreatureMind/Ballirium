using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupStars : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    // Start is called before the first frame update
    void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }

   public void StarControllerEvent()
   {
       Debug.Log("inside  StarControllerEvent counter: " + counter);
       
       counter++;
       Debug.Log("inside  StarControllerEvent counter: " + counter);

        switch (counter)
        {
            case 1:
                star1.SetActive(true);
                break;
            
            case 2:
                star2.SetActive(true);
                break;
            
            case 3:
                star3.SetActive(true);
                break;
        }
        
    }
}