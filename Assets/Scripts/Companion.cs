using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour {

    public GameObject player;
    public Transform playerTransform;
    public GameObject bulletInstance;
    public Vector3 playerpos;
    

    private float TargetTime = 5f;
    private float spawnTime = 5f;

    private bool isActive;
    public float NewXPos;
    public float NewXNeg;
    
    public float IncrementingX;
    // Use this for initialization


    void Start () {
        player = GameObject.Find("prefab_player");
        //playerpos = player.transform.position;
        bulletInstance = Resources.Load<GameObject>("prefab_bullet");
        isActive = false;
        transform.position = new Vector3(Random.Range(-9.8f, 9.8f), 50, -10);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BorderController")
        {
            isActive = true;
            Debug.Log("hit");
        }
    }


    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;

       
        

        if (!isActive)
        {
            spawnTime -= Time.deltaTime;
            Debug.Log("time before respawn" + "  " + spawnTime);

        }

        if (transform.position.y < -50)
        {
            transform.position = new Vector3(Random.Range(-6.0f, 6.0f), 50, -10);
            spawnTime = 5f;
        }

        //if (TargetTime <= 0)
        //{
        //    transform.position = new Vector3(Random.Range(-6.0f, 6.0f), 50, -10);
        //}


        if (spawnTime <= 0)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);


        if (isActive)
        {
            TargetTime -= Time.deltaTime;
            Debug.Log("time before reposition" + "  " + TargetTime);




            if (playerpos.x > 0f)
                transform.position = new Vector3(playerpos.x - 1f, playerpos.y -1, playerpos.z);
           else if (playerpos.x < 0f)
                transform.position = new Vector3(playerpos.x + 1f, playerpos.y- 1, playerpos.z);
            else
               transform.position = new Vector3(playerpos.x, playerpos.y + 2f, playerpos.z);

            if (TargetTime <= 0.0f)
            {
                TimerEnded();
            }
        }
                 


        if (Input.GetButton("Shoot") && isActive)
        {
            shoot();
        }
        
    }

    void shoot()
    {
        if (player.GetComponent<Player>().fuel >= 1)
        {
            player.GetComponent<Player>().fuel--;
        

        GameObject bullet = Instantiate(
                bulletInstance,
                transform.position,
               transform.rotation);
        bullet.GetComponent<Bullet>().bulletType = player.GetComponent<Player>().GetCellType;
        bullet.GetComponent<Bullet>().speed = 30f;
        bullet.GetComponent<Bullet>().x = -100;
        }
     }

    void TimerEnded()
        {

        isActive = false;
        TargetTime = 5;
        transform.position = new Vector3(Random.Range(-6.0f,6.0f), 50, -10);
        spawnTime = 5f;
        }

        


	




}
