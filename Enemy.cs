using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public Transform shootElement;
    public GameObject bullet;
    public GameObject Enemybug;
    public int Creature_Damage = 10;    
    public float Speed;
    // 
    public Transform[] waypoints;
    int curWaypointIndex = 0;
    public float previous_Speed;
    public Animator anim;
    public EnemyHp Enemy_Hp;
    public Transform target;
    public GameObject EnemyTarget;
    private bool isDead;
    

    void Start()
    {            
        anim = GetComponent<Animator>();
        Enemy_Hp = Enemybug.GetComponent<EnemyHp>();
        previous_Speed = Speed;
        isDead = false;
    }

    // Attack

    void OnTriggerEnter(Collider other)

    {
        if ((other.tag == "Castle") || (other.tag == "Tank"))
        {

            

            Speed = 0;
            EnemyTarget = other.gameObject;
            target = other.gameObject.transform;
            Vector3 targetPosition = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z);            
            transform.LookAt(targetPosition);
            anim.SetBool("RUN", false);
            anim.SetBool("Attack", true);
            
        }

    }

    // Attack
    void Shooting ()
    {
                  
            GameObject с = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            с.GetComponent<EnemyBullet>().target = target;
            с.GetComponent<EnemyBullet>().twr = this; 

    }



    void GetDamage()

    {
        if (EnemyTarget.CompareTag("Castle")) // get it from BuildingHp

        {            
                EnemyTarget.GetComponent<TowerHP>().Dmg_2(Creature_Damage);            
        }

        if (EnemyTarget.CompareTag("Tank")) // get it from BuildingHp

        {
            EnemyTarget.GetComponent<TowerHP>().Dmg_2(Creature_Damage);
        }
    }


    void Update () 
	{

        // MOVING

        if (curWaypointIndex < waypoints.Length)
        {
	        transform.position = Vector3.MoveTowards(transform.position,waypoints[curWaypointIndex].position,Time.deltaTime*Speed);
            
            if (!EnemyTarget)
            {
                transform.LookAt(waypoints[curWaypointIndex].position);
            }
	
	        if(Vector3.Distance(transform.position,waypoints[curWaypointIndex].position) < 0.5f)
	        {
		    curWaypointIndex++;
	        }    
	    }

        //else
        //{
        //    anim.SetBool("Victory", true);  
        //}
        // Victory
        if (target != null)
        {
            if (target.GetComponent<TowerHP>().CastleHp <= 0) 
            {
                Debug.Log("aa");
                anim.SetBool("RUN", true);
                anim.SetBool("Victory", true);
            }
        }
        
        // DEATH

        if (Enemy_Hp.EnemyHP <= 0)
        {
            Speed = 0;
            
            anim.SetBool("Death", true);

            if (isDead == false)
            {
                if (transform.tag == "Dead2")
                {
                    switch (gamemanager.instance.level)
                    {
                        case gamemanager.Level.level1:
                            WaveSpawn.maxsize -= 1;
                            break;
                        case gamemanager.Level.level2:
                            wavespwan2.maxsize -= 1; ;
                            break;
                        case gamemanager.Level.level3:
                            wavespwan2.maxsize -= 1;
                            break;

                    }
                    

                    isDead = true;
                    gamemanager.instance.monsternumber--;
                }
                if (transform.tag == "Dead")
                {
                    switch (gamemanager.instance.level)
                    {
                        case gamemanager.Level.level1:
                            WaveSpawn.maxsize -= 1;
                            break;
                        case gamemanager.Level.level2:
                            wavespwan2.maxsize -= 1; ;
                            break;
                        case gamemanager.Level.level3:
                            wavespwan2.maxsize -= 1;
                            break;

                    }
                    isDead = true;
                    gamemanager.instance.monsternumber--;
                }
            }

            Destroy(gameObject, 5f);
        }

        


    }
       
   
}

