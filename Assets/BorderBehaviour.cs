using UnityEngine;

public class BorderBehaviour : MonoBehaviour {
	private Player player = null;

	private void Start() {
		player = transform.parent.parent.GetComponent<Player>();
		Debug.Assert(player != null, "Couldn't find instance of Player class", player);
	}

	private void OnTriggerEnter(Collider other) {
		player.OnTriggerEnter(other);
	}
}
