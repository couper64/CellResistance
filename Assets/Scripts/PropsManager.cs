using UnityEngine;

public class PropsManager : MonoBehaviour {

	private System.Collections.Generic.List<GameObject> resources = null;
	private System.Collections.Generic.List<Transform> props = null;

	private void Start() {
		// allocate some memory;
		resources = new System.Collections.Generic.List<GameObject>();

		// feeding with data;
		resources.Add(Resources.Load<GameObject>("prefab_cancer_big_1"));
		resources.Add(Resources.Load<GameObject>("prefab_cancer_big_2"));
		resources.Add(Resources.Load<GameObject>("prefab_cancer_small_1"));
		resources.Add(Resources.Load<GameObject>("prefab_cancer_small_2"));
		resources.Add(Resources.Load<GameObject>("prefab_chol_1"));
		resources.Add(Resources.Load<GameObject>("prefab_crater_1"));
		resources.Add(Resources.Load<GameObject>("prefab_needle_1"));

		// props are vital for the game;
		string message = typeof(System.Collections.Generic.List<GameObject>).FullName;
		message += " is NULL";
		Debug.Assert(resources != null, message);
		message = "There are NO props to show in the game " + resources.Count.ToString();
		Debug.Assert(resources.Count != 0, message);

		// allocate some memory;
		props = new System.Collections.Generic.List<Transform>();
		for (int i = 0; i < transform.childCount; i++) {
			// store handle to current prop spawn position;
			Transform propSpawn = transform.GetChild(i);
			// avoid index out of array;
			if (resources.Count > 0) {
				// generate random number between number of 
				// props available;
				int propIndex = Random.Range(0, resources.Count);
				// create it from loaded resources;
				GameObject prop = Instantiate<GameObject>(resources[propIndex]);
				prop.transform.parent = propSpawn;
				// positioned it at the right place;
				prop.transform.position = propSpawn.position;
				// rotate as boxes;
				prop.transform.rotation = propSpawn.GetChild(0).rotation;
				float ranomize = Random.Range(0, 360);
				prop.transform.Rotate(new Vector3(0, 1, 0), ranomize);
			}
			// add to the list;
			props.Add(propSpawn);
		}
	}
}
