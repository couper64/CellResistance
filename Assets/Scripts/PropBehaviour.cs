using UnityEngine;

public class PropBehaviour : MonoBehaviour {

	public float pulsingMinDelay = 0;
	public float pulsingMinDuration = 0;
	public float pulsingMinSpeed = 0;
	public float pulsingMaxDelay = 0;
	public float pulsingMaxDuration = 0;
	public float pulsingMaxSpeed = 0;

	private float pulsingDelayTimer = 0;
	private float pulsingDurationTimer = 0;
	private float pulsingDirection = 1;
	private float pulsingDelay = 0;
	private float pulsingDuration = 0;
	private float pulsingSpeed = 0;
	private bool isPulsable = false;
    public GameObject player;
   
    


    private void Start() {
		// because we want them to pulse at different times;
		pulsingDelay = Random.Range(pulsingMinDelay, pulsingMaxDelay);
		pulsingDuration = Random.Range(pulsingMinDuration, pulsingMaxDuration);
		pulsingSpeed = Random.Range(pulsingMinSpeed, pulsingMaxSpeed);

		// setting pulsing timers;
		pulsingDelayTimer = 0;
		pulsingDurationTimer = 0;

		// prop will grow first;
		pulsingDirection = 1;

		isPulsable = pulsingDuration >= 0.00f;
		isPulsable = pulsingSpeed >= 0.00f;

        player = GameObject.Find("prefab_player");
        


    }

    private void Update() {

        if (Time.timeScale == 1)
        {
            Pulse();
        }

        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (player.GetComponent<Player>().hp <= 0)
        {
            Time.timeScale = 0.1f;
        }

        //if (ispulsable) {
        //	pulse();
        //}
    }

	private void Pulse() {
		// pulsing animation;
		pulsingDelayTimer += Time.deltaTime;
		if (pulsingDelayTimer < pulsingDelay) {
			// not time to play animation;
			// leaving;
			return;
		}
		// at this point we are sure that it is 
		// the time to play pulsing animation;
		pulsingDurationTimer += Time.deltaTime;
		if (pulsingDurationTimer > pulsingDuration) {
			// because we want to change the direction of growth;
			pulsingDirection = (pulsingDirection < 0) ? (1) : (-1);
			pulsingDurationTimer = 0;
		}
		Vector3 scaleDelta = Vector3.zero;
		scaleDelta.x = pulsingSpeed;
		scaleDelta.y = pulsingSpeed;
		scaleDelta.z = pulsingSpeed;
		scaleDelta /= 100.00f;
		scaleDelta *= pulsingDirection;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).localScale += scaleDelta;
		}
	}


    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;

        }
    }

}
