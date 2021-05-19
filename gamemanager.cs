using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static gamemanager instance;
    public GameObject currencetower;
    public button currentButton;
    public int monsternumber;
    public bool stillwave;
    public GameObject victorypage;
    public GameObject losepage;
    public Transform target;
    public AudioSource victorysound;
    public AudioSource losesound;
    public enum Level
    {
        level1, level2, level3
    }
    public Level level;

    void Awake() 
    {
        instance = this;

    }
    void Start()
    {
        stillwave = true;
        monsternumber = 0;
        victorypage.SetActive(false);
        losepage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checklose();
        checkcvictory();
        if (currencetower != null) 
        {
           
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity,LayerMask.GetMask("grass")))
            {

                //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.point.z);
                Vector3 mousePos = hit.collider.gameObject.transform.position;
                mousePos.x += 1f;
                mousePos.z -= 1f;
                mousePos.y = 1.5f;
                currencetower.transform.position = mousePos;
                if (Input.GetMouseButtonDown(0)) {
                    currencetower.GetComponent<Tower>().isset = true;
                    currencetower = null;
                    currentButton.SetDisable(5);

                }

                
            }
        } 
    }
    void checkcvictory() 
    {
        if (monsternumber <= 0 && stillwave == false) 
        {
            Debug.Log("Win");
            victorysound.Play();
            Time.timeScale = 1;
            victorypage.SetActive(true);

        }
    }
    void checklose() 
    {
        if (target == null) { return; }
        if (target.GetComponent<TowerHP>().CastleHp <= 0) 
        {
            losesound.Play();
            Time.timeScale = 1;
            losepage.SetActive(true);
        }
    }
}
