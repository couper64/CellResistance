using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour {

    [Header("Timers")]

    [Range(0.0f, 20.0f)]
    public float DurationTime = 5.0f;
    private float TargetTime = 0f;

    [Range(0.0f, 20.0f)]
    public float spawnTime = 5.0f;
    private float RespawnTime;
    
    [Header("CompanionSettings")]

    [Range(0.0f, 2.0f)]
    public float fallingSpeed = 0.0f;
    [Range(0.0f, 100.0f)]
    public float HeightOnRespawn = 0.0f;
    [Range(-100.0f, 0.0f)]
    public float YbeforeRespawn = 0.0f;


    [Header("SpawnBorders")]
    [Range(0.0f,-15.0f)]
    public float leftBorder;

    [Range(0.0f, 15.0f)]
    public float rightBorder;


    [Header("References")]
    public GameObject player;
    public Transform playerTransform;
    public GameObject bulletInstance;
    public Vector3 playerpos;

    bool isActive;
    
    void Start () {
        player = GameObject.Find("prefab_player");
        bulletInstance = Resources.Load<GameObject>("prefab_bullet");
        isActive = false;
        transform.position = new Vector3(Random.Range(leftBorder, rightBorder), 50, -10);
        RespawnTime = spawnTime;
        TargetTime = DurationTime;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "BorderController"){
            isActive = true;
             }
    }
    
    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
            if (!isActive){ RespawnTime -= Time.deltaTime;
            Debug.Log(RespawnTime + "  " + "Until respawn");}

                if (transform.position.y < YbeforeRespawn){
                    transform.position = new Vector3(Random.Range(leftBorder, rightBorder), HeightOnRespawn, -10);
                     RespawnTime = spawnTime;
                }

        if (RespawnTime <= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);
            Debug.Log("respawn");
        }
        
        if (isActive){
            TargetTime -= Time.deltaTime;

                Debug.Log(DurationTime + "  " + "time before reposition" );
            
                if (playerpos.x > 0f)
                    transform.position = new Vector3(playerpos.x - 1f, playerpos.y -1, playerpos.z);
                else if (playerpos.x < 0f)
                    transform.position = new Vector3(playerpos.x + 1f, playerpos.y- 1, playerpos.z);
                else
                   transform.position = new Vector3(playerpos.x, playerpos.y + 2f, playerpos.z);

                if (TargetTime <= 0.0f){
                    TimerEnded();
                }
        }
        
        if (Input.GetButton("Shoot") && isActive)
            shoot();
        
    }

    void shoot()
    {
        if (player.GetComponent<Player>().fuel >= 1) { 
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
        TargetTime = DurationTime;
        transform.position = new Vector3(Random.Range(leftBorder, rightBorder), HeightOnRespawn, -10);
        RespawnTime = spawnTime;
        }

        


	




}
