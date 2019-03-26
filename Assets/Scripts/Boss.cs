using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

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
		UnityEngine.Profiling.Profiler.BeginSample("Boss Update Profiling");
		int i = Random.Range(1, 100);
		if (i > 20 && i < 80) { Fire(); }
		if (wpnum == 1) {
			moveTo1();
			if (transform.position == wp1.position) wpnum = 2;
		}
		if (wpnum == 2) {
			moveTo2();
			if (transform.position == wp2.position) wpnum = 3;
		}
		if (wpnum == 3) {
			moveTo3();
			if (transform.position == wp3.position) wpnum = 1;
		}
		Reposition();
		UnityEngine.Profiling.Profiler.EndSample();
	}
	
	void moveTo1() {
		transform.position = Vector3.MoveTowards(transform.position, wp1.position, kek);
	}

	void moveTo2() {
		transform.position = Vector3.MoveTowards(transform.position, wp2.position, kek);
	}

	void moveTo3() {
		transform.position = Vector3.MoveTowards(transform.position, wp3.position, kek);
	}

	void Reposition() {
		if (transform.position.y <= -70f) {
			transform.position = new Vector3(0, 100, 0);
		}
	}

	void Fire() {

		Vector3 cannon = new Vector3(
			transform.localPosition.x, 
			transform.localPosition.y, 
			transform.localPosition.z
		);

		var bulletClone = (GameObject)Instantiate(bullet, cannon, transform.rotation);
		bulletClone.GetComponent<Rigidbody>().velocity = (transform.up * -20f);

	}
}

