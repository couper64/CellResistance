using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellRes;

public class Bullet : MonoBehaviour
{
    public Material whitem, blackm;
    public CellRes.Type bulletType;
    //public GameObject GameManager;
    public static int ScoreSave;
    public int x = 0;
	public float speed;
	public Text score;
	public Text highestScore;
	public static int HighestScore;
	public GameObject player;
	// Use this for initialization
	void Start ()
    {
		switch (bulletType) {
			case Type.ENEMY_WHITE:
				gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
				break;
			case Type.ENEMY_BLACK:
				gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
				break;
			case Type.WHITE:
				gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
				break;
			case Type.BLACK:
				gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
				break;
			default:
				break;
		}
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
		transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);

		if (x >= 100) {
			Destroy(gameObject);
		}

		x++;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
			if (bulletType == Type.ENEMY_BLACK || bulletType == Type.ENEMY_WHITE) {
				
			}
			else {
				ScoreSave = ScoreSave + 10;
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().score = ScoreSave;
				Debug.Log(other.name);
				Destroy(other.transform.parent.gameObject);
				Destroy(gameObject);
				score.text = ScoreSave.ToString();
			}

            
        }
    }

 //   private void White()
 //   {
 //       bulletType = Type.WHITE;
 //       gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;

 //   }

 //   private void Black()
 //   {
 //       bulletType = Type.BLACK;
	//	gameObject.GetComponent<SpriteRenderer>().material.color = Color.black;

	//}
}
