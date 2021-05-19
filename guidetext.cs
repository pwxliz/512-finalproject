using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class guidetext : MonoBehaviour
{
    // Start is called before the first frame update
    public static guidetext instance;
    public float Typespeed = 50f;
    Text showtext;
    private string content1;
    private string content2;
    private string content3;
    private string content4;
    private string content5;
    private int curPos;
    private string content;
    public GameObject hola;
    public GameObject startguid;
    public GameObject Grass;
    public GameObject Monster;
    public GameObject Fruit;
    public GameObject platehola;
    public static bool guidesituation;
    public AudioSource clickbutton;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        showtext = this.GetComponent<Text>();
        content1 = "Welcome to fruit town! It is originally a peace and lovely twon, but a evil treasure monster is trying to destroy our fruits.";
        content2 = "Your aim is try your best to protect those fuits.";
        content3 = "Click mouse left button to choose your cannon.";
        content4 = "Click here to set your cannon.";
        content5 = "Now, you know how to play the game.Enjoy the game!";
        content = content1;
        //showtext.text = content;
        setcontent();
        //Time.timeScale = 0;
        hola.SetActive(true);
        platehola.SetActive(false);
        hola.transform.position = Monster.transform.position + new Vector3(0, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
       if ( Input.GetMouseButtonDown(0)){
            clickbutton.Play();
            if (content == content1 )
            {
                content = content2;
                //showtext.text = content;
                setcontent();
                
                hola.transform.position = Fruit.transform.position;
                return;
            }

            if (content == content2 )
            {
                content = content3;
                //showtext.text = content;
                setcontent();
                platehola.SetActive(true);
                return;
            }

            if (content == content3 )
            {
                content = content4;
                //showtext.text = content;
                setcontent();
                platehola.SetActive(false);
                hola.transform.position = Grass.transform.position + new Vector3(1, 2, -1);
                
                Debug.Log(hola.transform.position);
                return;
            }

            if (content == content4)
            {
                content = content5;
                //showtext.text = content;
                hola.SetActive(false);
                setcontent();
                Debug.Log(content);
                
                return;
            }

            if (content == content5 )
            {
                setcontent();
                startguid.SetActive(false);
                WaveSpawn.instance.spawn();
                
            }
        }

    }
    void setcontent()
    {
        curPos = 0;

        showtext.text = string.Empty;
        InvokeRepeating("Typing", 0, Typespeed);
    }
    void Typing()
    {
        if (content.Length - 1 == curPos)
        {
            CancelInvoke("Typing");
        }
        if (curPos < content.Length)
        {
            showtext.text += content.Substring(curPos, 1);
            curPos++;
        }
    }
}
