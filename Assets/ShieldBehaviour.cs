using UnityEngine;

public class ShieldBehaviour : MonoBehaviour {
	private Player player;

	private void Start() {
		player = transform.parent.GetComponent<Player>();
		Debug.Assert(player != null, "Player is NULL");
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Bullet") {
			player.OnTriggerEnter(other);
		}
	}
}
