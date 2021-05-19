using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tower;
    public AudioSource clickbutton;
    Button btower;

    float coolTimer;
    float currentCoolTimer;

    void Start()
    {
        btower = GetComponent<Button>();
        btower.onClick.AddListener(addtower);


    }

    // Update is called once per frame
    void Update()
    {
        CoolDown();
    }
    void addtower() 
    {
        clickbutton.Play();
        GameObject newtower = GameObject.Instantiate(tower);
        gamemanager.instance.currencetower = newtower;

        gamemanager.instance.currentButton = btower.GetComponent<button>();

    }

    public void SetDisable(float time)
    {
        coolTimer = time;
        currentCoolTimer = coolTimer;
        btower.interactable = false;
    }


    void CoolDown()
    {
        if (currentCoolTimer > 0 && btower.interactable == false)
        {
            currentCoolTimer -= Time.deltaTime;
            if (currentCoolTimer <=0)
            {
                btower.interactable = true;
            }
        }
        
    }
}
