using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Ebola : Enemy {


	public GameObject bullet;
	private float nextFire = 3.0f;
	public float fireRatep;
	public float fireRatem;
	private float kek = 0;
	public float speed = 0;

	//waypoints
	public Transform wp1;
	public Transform wp2;
	public Transform wp3;
    public Transform wp4;

    public GameObject[] WP;
	private int wpnum;
	private void Start() {
		wpnum = 1;
		kek = speed * Time.deltaTime;
		hp = 500;
		currentState = State.ATTACK_PATTERN_1;
		nextFire = nextFire + Random.Range(fireRatep, fireRatem);
	}

	private void Update() {

		int i = Random.Range(1, 100);

		if (i > 2000000 && i < 80)
        {
			Fire();
		}


		if (wpnum == 1)
         {
            moveTo1();
            if (transform.position == wp1.position) wpnum = 2;

         }
		else if (wpnum == 2)
        {
			moveTo2();
			if (transform.position == wp2.position) wpnum = 3;
		}
		else if (wpnum == 3)
        {
			moveTo3();
			if (transform.position == wp3.position) wpnum = 1;
		}


	}


	



	void moveTo1()
    {
		transform.position = Vector3.MoveTowards(transform.position, wp1.position, kek);
	}
	void moveTo2()
    {
        transform.position = Vector3.MoveTowards(transform.position, wp2.position, kek);
	}
	void moveTo3()
    {
		transform.position = Vector3.MoveTowards(transform.position, wp3.position, kek);
	}

	void Fire()
    {
		Vector3 cannon = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

		//nextFire = Time.time + Random.Range(fireRatep, fireRatem);
		var bulletClone = (GameObject)Instantiate(bullet, cannon, transform.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = (transform.up * -20f);
	}
}

