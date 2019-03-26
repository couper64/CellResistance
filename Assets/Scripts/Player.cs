using UnityEngine;
using CellRes;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	// player state;
	public enum State {
		IDLE = 0,
		FLYING = 1,
		DEATH = 2,
		PAUSE = 3
	};

	// color of the shield;
	public Material materialWhite;
	public Material materialBlack;

	public float hp = 100f;
	public float hpDeadzone = 25f;
	public float fuel = 100f;
	public float power = 0f;
	public float playerMovementSpeed = 3;
	public GameObject bulletInstance;
	public GameObject shield;
	public GameObject gunLeft;
	public GameObject gunRight;
	public Slider playerHealth;
	public Image playerFace;
	public Sprite playerFaceDefault;
	public Sprite playerFaceHurt;
	public Slider playerSpecialAbility;
	public Image playerPowerUpStatus;
	public Sprite playerPowerUpStatusLocked;
	public Sprite playerPowerUpStatusUnlocked;
	public GameObject whiteFuel;
	public GameObject gameOverMenu;
	public GameObject exp;
	public GameObject expPrefab;
	public GameObject gunCenter;
	public GameObject ship;

	private float timer = 3;
	private State state = State.IDLE;
	private CellRes.Type type = Type.WHITE;

	private void Awake() {
		Time.timeScale = 1.0f;
	}

	private void Start() {
		state = State.FLYING;
		hp = 100f;
		fuel = 100f;
		power = 0f;
		White();
	}

	public bool GetFlying() {
		if (state == State.DEATH) {
			return true;
		}
		return false;
	}

	void Update() {
		UnityEngine.Profiling.Profiler.BeginSample("Player Script Update Profiling");
		switch (state) { case State.IDLE: Idle(); break; case State.FLYING: Fly(); break;
			case State.DEATH: Death(); break; case State.PAUSE: break; // leave function; // do nothing;
			default: break; }
		switch (type) { case Type.WHITE: break; case Type.BLACK: break; default: break; }
		//Death condition // protect from going to negative; // because it does not look right;
		if (hp <= 0) { state = State.DEATH; hp = 0; } playerHealth.value = hp; timer += Time.deltaTime;
		if (timer > 1) { playerFace.sprite = playerFaceDefault; }
		playerSpecialAbility.value = power; //power up;
		if (power >= 100.00f) { power = 100f; playerPowerUpStatus.sprite = playerPowerUpStatusUnlocked; }
		else { playerPowerUpStatus.sprite = playerPowerUpStatusLocked; }
		power += 1f; fuel = Mathf.Clamp(fuel, 0, 100); //fuel;
		float percentage = fuel / 100; float scaleWidth = 0.4f; float scaleHeight = 0.3f;
		scaleWidth = scaleWidth * percentage; // assuming never more than 100;
		scaleHeight = scaleHeight * percentage; whiteFuel.transform.localScale = new Vector3(
			scaleWidth, scaleHeight, whiteFuel.transform.localScale.z
		); if (Input.GetButtonDown("Pause")) { Pause();}
		UnityEngine.Profiling.Profiler.EndSample();
	}

	private void Idle() {
		//If Idle, remove the shield
		if (state == State.IDLE) {
			shield.GetComponent<Renderer>().enabled = false;
		}
	}

	private void Fly() {
		// Movement Horizontal;
		float horizontal = Input.GetAxis("Horizontal");
		transform.Translate(
			horizontal * Time.deltaTime * playerMovementSpeed, 
			0f, 
			0f, 
			Space.Self
		);
		// Vertical;
		float vertical = Input.GetAxis("Vertical");
		transform.Translate(
			0f,
			vertical * Time.deltaTime * playerMovementSpeed, 
			0f, 
			Space.Self
		);
		Vector3 screenPosition = Camera.allCameras[0].WorldToScreenPoint(transform.position);
		// left border;
		if (screenPosition.x < 0 + (0.15625f * Screen.width)) {
			screenPosition.x = 0 + (0.15625f * Screen.width);
			Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
			transform.position = worldPosition;
		}
		// right border;
		else if (screenPosition.x > Screen.width - (0.15625f * Screen.width)) {
			screenPosition.x = Screen.width - (0.15625f * Screen.width);
			Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
			transform.position = worldPosition;
		}

		// top border;
		if (screenPosition.y > Screen.height) {
			screenPosition.y = Screen.height;
			Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
			transform.position = worldPosition;
		}
		// bottom border;
		else if (screenPosition.y < 0) {
			screenPosition.y = 0;
			Vector3 worldPosition = Camera.allCameras[0].ScreenToWorldPoint(screenPosition);
			transform.position = worldPosition;
		}
		float width = 4;
		float ratio = Mathf.Abs(transform.localPosition.x) / (width / 2);
		if (transform.localPosition.x < 0) {
			// left;
			// height;
			Vector3 position = transform.localPosition;
			position.z = (-10) - Mathf.Clamp(ratio, -5, 5);
			transform.localPosition = position;
			// angle;
			Vector3 angle = ship.transform.localEulerAngles;
			angle.y = -Mathf.Clamp(ratio, -1, 1) * 35.0f;
			ship.transform.localEulerAngles = angle;
		}
		else if (transform.localPosition.x > 0) {
			// right;
			// height;
			Vector3 position = transform.localPosition;
			position.z = (-10) - Mathf.Clamp(ratio, -5, 5);
			transform.localPosition = position;
			// angle;
			Vector3 angle = ship.transform.localEulerAngles;
			angle.y = Mathf.Clamp(ratio, -1, 1) * 35.0f;
			ship.transform.localEulerAngles = angle;
		}

		//ChangeColor
		if (Input.GetButtonDown("White")) {
			White();
		}
		if (Input.GetButtonDown("Black")) {
			Black();
		}

		//Shoot
		if (Input.GetButton("Shoot")) {
			Shoot();
		}
		if (power >= 100) {
			if (Input.GetButtonDown("PowerUp")) {
				power = 0;
				Special();
			}
		}
		
	}

	// should be done in game manager;
	private void Pause() {
		if (state != State.PAUSE) {
			state = State.PAUSE;
			Time.timeScale = 0;
		}
		else {
			state = State.FLYING;
			Time.timeScale = 1;
		}
	}

	private void Death() {
		if (exp == null)
		{
			exp = Instantiate(expPrefab, transform.position, transform.rotation);
		}

		hp = 0;
		if (!gameOverMenu.activeSelf) {
			gameOverMenu.SetActive(true);
			Time.timeScale = 0.1f;
		}
		transform.position = Vector3.zero;
		if (Input.GetButtonDown("Pause")) {
			gameOverMenu.SetActive(false);
			state = State.FLYING;
			Time.timeScale = 1.0f;
			hp = 100f;
			RestartGame();
		}
		else if (Input.GetButtonDown("PowerUp")) {
			gameOverMenu.SetActive(false);
			state = State.FLYING;
			Time.timeScale = 1.0f;
			hp = 100f;
			SceneManager.LoadScene("scene_main_menu");
		}
	}

	private void Shoot() {
		if (fuel >= 1) {
			fuel--;
			GameObject bullet = Instantiate(
				bulletInstance, 
				gunLeft.transform.position, 
				gunLeft.transform.rotation
			);
			bullet.GetComponent<Bullet>().bulletType = type;
			bullet.GetComponent<Bullet>().speed = 30f;
			
			GameObject bullet0 = Instantiate(
				bulletInstance,
				gunRight.transform.position,
				gunRight.transform.rotation
			);
			bullet0.GetComponent<Bullet>().bulletType = type;

			bullet0.GetComponent<Bullet>().speed = 30f;

			GameObject bullet1 = Instantiate(
				bulletInstance,
				gunCenter.transform.position,
				gunCenter.transform.rotation
			);
			bullet1.GetComponent<Bullet>().bulletType = type;

			bullet1.GetComponent<Bullet>().speed = 30f;

			GetComponent<AudioSource>().Play();
		}
	}

	private void White() {
		type = Type.WHITE;
		shield.gameObject.GetComponent<Renderer>().material = materialWhite;

	}

	private void Black() {
		type = Type.BLACK;
		shield.gameObject.GetComponent<Renderer>().material = materialBlack;

	}

	void Special() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Bullet");
		for (int i = 0; i < objects.Length; i++) {
			Destroy(objects[i]);
		}
	}

	private void RestartGame() {
		SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene_game_play"));
		SceneManager.LoadScene("scene_game_play");
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene_game_play"));
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Bullet") {
			//When hitting a bullet that has the enumerator set as EnemyWhite
			if (other.gameObject.GetComponent<Bullet>().bulletType == Type.ENEMY_WHITE) {

				if (type == Type.WHITE) {
					fuel = fuel + 50;
					
				}
				else if (type == Type.BLACK) {
					hp -= 30;
					
				}
			}
			//When hitting a bullet that has the enumerator set as EnemyBlack
			if (other.gameObject.GetComponent<Bullet>().bulletType == Type.ENEMY_BLACK) {
				if (type == Type.BLACK) {
					fuel += 50;
				}
				else if (type == Type.WHITE) {
					hp -= 30;
					
				}
			}
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "Enemy") {
			state = State.DEATH;
		}
		playerFace.sprite = playerFaceHurt;
		timer = 0;
	}
}
