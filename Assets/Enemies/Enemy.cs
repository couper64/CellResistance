using UnityEngine;

public class Enemy : MonoBehaviour {

	protected enum State {
		IDLE = 0,
		DEATH,
		ATTACK_STRAIGHT_LINE = 2,
		ATTACK_HOMING = 3,
		ATTACK_PATTERN_1 = 4
	}

	protected State currentState;
	protected float hp = 0f;
}