using UnityEngine;
using System.Collections;

public class dataSetterScript : MonoBehaviour {

	private float accTime = 0.0f;
	void Start () {
	
	}

	private float yval = 1.0f;
	
	void Update () {
		accTime += Time.deltaTime;
		if (accTime < 0.3f) { // every 300msec
			return;
		}
		accTime = 0.0f;

		graphDrawControl.setYval (yval);
		yval -= 0.015f;
	}
}
