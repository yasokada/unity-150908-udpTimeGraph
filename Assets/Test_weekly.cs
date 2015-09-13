using UnityEngine;
using System.Collections;

public class Test_weekly : MonoBehaviour {

	enum xscaletype {
		Daily = 0,
		Weekly,
		Monthly,
		Yearly,
	};

	[Range((int)xscaletype.Daily, (int)xscaletype.Yearly)]
	public int xtype = 0;

	void Test_addData() {
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

		foreach(var dt in dts) {
			curDt = System.DateTime.Parse(dt);
			yval = Random.Range(-1.0f, 1.0f);
			timeGraphScript.SetXYVal(curDt, yval);
			idx++;
		}
	}

	void Test_run() {
		timeGraphScript.SetXType ((int)xtype);		
		Test_addData ();
	}

	void Start () {
		Test_run ();
	}
	
	void Update () {
	
	}
}
