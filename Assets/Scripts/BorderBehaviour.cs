using UnityEngine;

public class BorderBehaviour : MonoBehaviour {
	private Player player = null;

	private void Start() {
		player = transform.GetComponentInParent<Player>();
		Debug.Assert(player != null, "Couldn't find instance of Player class", player);
	}

	private void OnTriggerEnter(Collider other) {
		player.OnTriggerEnter(other);
	}
}
