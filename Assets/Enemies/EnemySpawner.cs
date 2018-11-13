using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	private float nextSpawn = 3.0f;
	public float spawnratemin;
	public float spawnratemax;
	private int count = 0;
	private int wave = 1;
	public Text waveValue;
	float timeLeft = 60f;

	private void Start() {
		nextSpawn = nextSpawn + Random.Range(spawnratemin, spawnratemax);
	}

	void spawn() {
		Vector3 spawner = new Vector3(
			transform.localPosition.x, 
			transform.localPosition.y, 
			transform.localPosition.z
		);

		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + Random.Range(spawnratemin, spawnratemax);
			Instantiate<GameObject>(enemy, spawner, transform.rotation);
			count++;
			if (count == 20) 
				{
				//nextSpawn = 60f;
				//float timeLeft = 60f;
				timeLeft -= Time.deltaTime;
				Debug.Log(timeLeft);
				if (timeLeft < 0) {
					count = 0;
					//nextSpawn = 3.0f;
					wave++;
					timeLeft = 60f;
				}
			}
			if (count == 20 && wave == 3) {
				//gameObject[];
			}



			//enemyClone.velocity = (transform.right * -20f);
		}
		waveValue.text = wave.ToString();
	}
	private void FixedUpdate() {
		spawn();
	}
	private void SpawnBoss() { }
}
