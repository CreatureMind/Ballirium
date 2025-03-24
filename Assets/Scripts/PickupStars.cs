using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupStars : MonoBehaviour
{
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

   public void StarControllerEvent(GameObject star)
    {
        switch (star.name)
        {
            case "1#Star":
                star1.SetActive(true);
                break;
            case "2#Star":
                star2.SetActive(true);
                break;
            case "3#Star":
                star3.SetActive(true);
                break;
        }
    }
}