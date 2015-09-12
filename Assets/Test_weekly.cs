using UnityEngine;
using System.Collections;

public class Test_weekly : MonoBehaviour {

	void Test_addWeeklyData() {

		string [] dts = new string[]{ 
			"2015/09/07 12:30",
			"2015/09/08 09:30", 
			"2015/09/10 11:30", 
			"2015/09/11 13:30",
			"2015/09/12 13:30",
		};

		System.DateTime curDt;
		float yval;
		int idx = 0;

		timeGraphScript.SetXType ((int)timeGraphScript.XType.Weekly);

		foreach(var dt in dts) {
			curDt = System.DateTime.Parse(dt);
//			yval = vals[idx];
			yval = Random.Range(-1.0f, 1.0f);
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
