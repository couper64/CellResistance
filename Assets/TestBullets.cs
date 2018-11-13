using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullets : MonoBehaviour {

	public GameObject bullets;
	public float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 0.5f) {
			timer = 0;
			GameObject bu = Instantiate<GameObject>(bullets);
			bu.GetComponent<Bullet>().bulletType = CellRes.Type.BLACK;
			bu.GetComponent<Bullet>().speed = 5f;
			bu.transform.position = transform.position;
			bu.transform.rotation = transform.rotation;
		}
	}
}
