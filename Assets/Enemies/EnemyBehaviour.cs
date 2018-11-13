using UnityEngine;


public class EnemyBehaviour : Enemy {
	public GameObject bullet;
	private float nextFire = 3.0f;
	public float fireRatep;
	public float fireRatem;
	CellRes.Type type = CellRes.Type.ENEMY_WHITE;
	public GameObject exp;

	private void Start() {
		type = (CellRes.Type)(int)Random.Range(2, 4);
		hp = 100;
		currentState = State.ATTACK_PATTERN_1;
		nextFire = nextFire + Random.Range(fireRatep, fireRatem);
	}

	private void Update() {
		//hp--;
		Oscillating();
		StraightLine();
		Reposition();
		Death();
	}
	private void FixedUpdate() {
		Fire();
	}

	void StraightLine() {
		if (currentState == State.ATTACK_STRAIGHT_LINE) {
			transform.parent.Translate(-Vector3.up * (Time.deltaTime * 0.001f));
		}
	}
	float direction = 1;
	void Oscillating() {
		if (currentState == State.ATTACK_PATTERN_1) {
			float width = 4;
			// go left;

			Vector3 screenPosition = Camera.allCameras[0].WorldToScreenPoint(transform.position);

			// left border;
			if (screenPosition.x < 0 + (0.15625f * Screen.width)) {
				screenPosition.x = 0 + (0.15625f * Screen.width);
				direction = 1;
				//Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
				//transform.position = worldPosition;
				//Debug.Log("SCREEN " + screenPosition);
				//Debug.Log("WORLD " + worldPosition);
			}
			// right border;
			else if (screenPosition.x > Screen.width - (0.15625f * Screen.width)) {
				screenPosition.x = Screen.width - (0.15625f * Screen.width);
				direction = -1;
				//Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
				//transform.position = worldPosition;
			}

			//if (transform.parent.localPosition.x < (-1 * width / 2)) {
			//	direction = 1;
			//}
			//else if (transform.parent.localPosition.x > (width / 2)) {
			//	direction = -1;
			//}

			transform.parent.Translate(Vector3.right * direction * (Time.deltaTime * 2.0f), Space.Self);

			//float ratio = -(Mathf.Abs(transform.parent.localPosition.x) / (0.15f * width));

			//if (transform.parent.localPosition.x < 0)
			//{
			//    Vector3 localPosition = transform.localPosition;
			//    localPosition.z = ratio;
			//    transform.localPosition = localPosition;
			//    Vector3 eu = transform.localEulerAngles;
			//    eu.y = 90 + ratio * 10f;
			//    transform.localEulerAngles = eu;

			//}
			//else if (transform.parent.localPosition.x > 0)
			//{
			//    Vector3 localPosition = transform.localPosition;
			//    localPosition.z = ratio;
			//    transform.localPosition = localPosition;
			//    Vector3 eu = transform.localEulerAngles;
			//    eu.y = 90 - ratio * 10f;
			//    transform.localEulerAngles = eu;
			//}
			transform.parent.Translate(-Vector3.up * (Time.deltaTime * 2.0f));

			//float width = 4;
			float ratio = Mathf.Abs(transform.localPosition.x) / (width / 2);
			if (transform.localPosition.x < 0) {
				// left;
				// height;
				Vector3 position = transform.localPosition;
				position.z = (-10) - Mathf.Clamp(ratio, -5, 5);
				transform.localPosition = position;
				// angle;
				Vector3 angle = transform.localEulerAngles;
				angle.y = -Mathf.Clamp(ratio, -1, 1) * 50.0f;
				transform.localEulerAngles = angle;
			}
			else if (transform.localPosition.x > 0) {
				// right;
				// height;
				Vector3 position = transform.localPosition;
				position.z = (-10) - Mathf.Clamp(ratio, -5, 5);
				transform.localPosition = position;
				// angle;
				Vector3 angle = transform.localEulerAngles;
				angle.y = Mathf.Clamp(ratio, -1, 1) * 50.0f;
				transform.localEulerAngles = angle;
			}
		}
	}

	void Death() {
		if (hp <= 0) {
			currentState = State.DEATH;
			if (currentState == State.DEATH) {
				Destroy(gameObject);
				Debug.Log("dead");
			}
		}
	}

	void Reposition() {
		if (transform.parent.position.y <= -70f) {
			transform.parent.position = new Vector3(0, 100, 0);
		}
	}

	void Fire() {
		Vector3 cannon = new Vector3(
			transform.parent.localPosition.x,
			transform.parent.localPosition.y,
			transform.parent.localPosition.z
		);

		if (Time.time > nextFire) {
			nextFire = Time.time + Random.Range(fireRatep, fireRatem);
			var bulletClone = (GameObject)Instantiate(bullet, cannon, transform.parent.rotation);
			//bulletClone.GetComponent<Rigidbody>().velocity = (transform.right * -20f);
			bulletClone.GetComponent<Bullet>().bulletType = type;
			bulletClone.GetComponent<Bullet>().speed = -5f;
			bulletClone.GetComponent<Bullet>().x = -2000;
		}
	}

	private void OnDestroy() {
		Instantiate<GameObject>(exp, transform.position, transform.rotation);
	}
}
