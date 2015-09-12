using UnityEngine;
using System.Collections;

public class Test_weekly : MonoBehaviour {

	void Test_addWeeklyData() {

		string [] dts = new string[]{ "09:30", "11:30", "13:30" };
		float [] vals = new float[] { 0.5f, -0.9f, 0.9f };

		System.DateTime curDt;
		float yval;
		int idx = 0;

		foreach(var dt in dts) {
			curDt = System.DateTime.Parse(dt);
			yval = vals[idx];
			timeGraphScript.SetXYVal(curDt, yval);
			idx++;
		}
	}

	void Start () {
		Test_addWeeklyData ();
	}
	
	void Update () {
	
	}
}
