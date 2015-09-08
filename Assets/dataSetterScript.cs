using UnityEngine;
using System.Collections;

public class dataSetterScript : MonoBehaviour {

	private float accTime = 0.0f;
	private System.DateTime curDt = System.DateTime.Parse("2015/09/08 00:00:00");

	void Start () {
	
	}

	private float yval = 0.5f;
	
	void Update () {
		accTime += Time.deltaTime;
		if (accTime < 0.3f) { // every 300msec
			return;
		}
		accTime = 0.0f;

		curDt = curDt.AddMinutes (10f);

		timeGraphScript.SetXYVal (curDt, yval);
		yval -= 0.001f;
	}
}
