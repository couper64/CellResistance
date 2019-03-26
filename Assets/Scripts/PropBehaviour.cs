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
	}

	private void Update() {
		UnityEngine.Profiling.Profiler.BeginSample("PropBehaviour Script Update Profiling");
		if (isPulsable) {
			Pulse();
		}
		UnityEngine.Profiling.Profiler.EndSample();
	}

	private void Pulse() {
		// pulsing animation;
		pulsingDelayTimer += Time.deltaTime;
		if (pulsingDelayTimer < pulsingDelay) {
			// not time to play animation;
			// leaving;
			return;
		}

		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).localScale.magnitude > pulsingMaxDuration) {
				// because we want to change the direction of growth;
				pulsingDirection = (-1);
				// check only single child;
				break;
			}
			else if (transform.GetChild(i).localScale.magnitude < pulsingMinDuration) {
				// because we want to change the direction of growth;
				pulsingDirection = (1);
				// check only single child;
				break;
			}
		}

		Vector3 scaleDelta = Vector3.zero;
		scaleDelta.x = pulsingSpeed;
		scaleDelta.y = pulsingSpeed;
		scaleDelta.z = pulsingSpeed;
		scaleDelta /= 100.00f;
		scaleDelta *= pulsingDirection;
		// we want to unbind it from timeframe;
		scaleDelta *= Time.deltaTime;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).localScale += scaleDelta;
		}
	}
}
