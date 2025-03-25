using UnityEngine;

public class PickupStars : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;


    void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

    }
    
   public void StarControllerEvent()
   {
       counter++;
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