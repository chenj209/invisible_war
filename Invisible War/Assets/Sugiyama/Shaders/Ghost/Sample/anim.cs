using UnityEngine;
using System.Collections;

public class anim : MonoBehaviour {

	float DEFAULT_POWER = 0.8f;
	float MIN_TIME = 3.5f;
	float MAX_TIME = 4.5f;

	// Use this for initialization
	void Start () {
		prop_ = new MaterialPropertyBlock();
		a_ = DEFAULT_POWER;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time >= MIN_TIME && Time.time <= MAX_TIME) {
			float len = DEFAULT_POWER / (MAX_TIME - MIN_TIME);
 			float localTime = Time.time - MIN_TIME;	
			a_ -= len * localTime;
		}

		prop_.Clear();
		prop_.SetFloat("_EdgePower", a_);
		this.gameObject.GetComponent<Renderer>().SetPropertyBlock (prop_);
	}
	
	MaterialPropertyBlock prop_;
	float default_;
	float a_;
}