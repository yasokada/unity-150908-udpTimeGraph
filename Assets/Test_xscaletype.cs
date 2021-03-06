﻿using UnityEngine;
using System.Collections;

public class Test_xscaletype : MonoBehaviour {

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
			"2015/01/10 05:30",
			"2015/02/05 12:30",
			"2015/03/01 08:30",
			"2015/04/07 23:50",
			"2015/07/11 09:30",
			"2015/08/12 12:30",
			// for today (as of Sep.13, 2015)
			"2015/09/13 8:30",
			"2015/09/13 10:00",
			"2015/09/13 11:30",
			"2015/09/13 16:30",
			"2015/09/13 20:10",
			"2015/09/13 23:40",
			//
			"2015/09/14 09:30", 
			"2015/09/15 11:30", 
			"2015/09/16 13:30",
			"2015/09/17 13:30",
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
