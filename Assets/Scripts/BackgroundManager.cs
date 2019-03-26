using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public enum State {
		PAUSE,
		PLAY
	};

	public GameObject[] resources = null;
	public int backgroundsMaxNumber = 0;
	// because it goes down it should have only negative 
	// speed;
	[Range(-100.00f, -1.00f)]
	public float backgroundSpeed = -1.00f;
	[Range(-100.00f, -15.00f)]
	public float backgroundDeadzone = -15.00f;

	private System.Collections.Generic.List<GameObject> backgrounds = null;
	private State state = State.PAUSE;

	private void Start() {
		// allocate new list;
		// because we want to store references to backgrounds;
		backgrounds = new System.Collections.Generic.List<GameObject>();

		for (int i = 0; i < resources.Length; i++) {
			for (int j = 0; j < backgroundsMaxNumber; j++) {
				GameObject background = Instantiate<GameObject>(resources[i], transform);
				Vector3 position = new Vector3(
					0, 
					background.GetComponentInChildren<Renderer>().bounds.size.y * j,
					0
				);
				background.transform.localPosition = position;
				Vector3 rotation = new Vector3(0f, 0f, 0f);
				background.transform.localEulerAngles = rotation;
				backgrounds.Add(background);
			}
		}

		// because we want to start playing all mechanics;
		state = State.PLAY;
	}

	private void Update() {
		UnityEngine.Profiling.Profiler.BeginSample("Background Manager Update");
		switch (state) {
			case State.PAUSE:
				break;
			case State.PLAY:
				Play();
				break;
			default:
				break;
		}
		UnityEngine.Profiling.Profiler.EndSample();
	}

	private void Play() {
		if (backgrounds.Count <= 0) {
			Debug.LogError("There is NO backgrounds to show!");
			// terminate Play();
			// because we do not have any images to show 
			// to the screen;
			return;
		}

		// because we want to change background over time;
		foreach (GameObject background in backgrounds) {

			// because we want to move things down;
			Vector3 translation = new Vector3(0f, backgroundSpeed * Time.deltaTime, 0f);
			background.transform.Translate(translation, Space.Self);

			// because we want to re-use assets but putting them at the top;
			if (background.transform.localPosition.y <= backgroundDeadzone) {

				// because we need the size of the each segment;
				Renderer renderer = background.GetComponentInChildren<Renderer>();
				float offset = renderer.bounds.size.y * backgrounds.Count;
				translation = new Vector3(0f, offset, 0f);
				background.transform.Translate(translation, Space.Self);
			}
		}
	}
}
