
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Animator animatorController;
    float currentTime;
    bool isWalking = false;

    void Start()
    {
        animatorController = GetComponent<Animator>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (currentTime + Time.deltaTime);

        if(currentTime > 2.0f)
        {
            currentTime = 0;

            if(isWalking)
            {
                isWalking = false;
                animatorController.SetFloat("Vitesse", 0.0f);
            }
            else
            {
               isWalking = true;
               animatorController.SetFloat("Vitesse", 1.0f);
               
            }
           
        }


    }
}
