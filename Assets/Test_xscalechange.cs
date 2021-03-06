﻿using UnityEngine;
using System.Collections;

public class Test_xscalechange : MonoBehaviour {

	enum xscaletype {
		Daily = 0,
		Weekly,
		Monthly,
		Yearly,
	};
	
	[Range((int)xscaletype.Daily, (int)xscaletype.Yearly)]
	public int xtype = 0;

	private int pretype = -1;

	float accTime = 0.0f;
	
	void Update () {
		accTime += Time.deltaTime;
		if (accTime < 0.3f) { // every 300 msec
			return;
		}
		accTime = 0.0f;

		if (xtype != pretype) {
			pretype = xtype;
			timeGraphScript.SetXType((int)xtype);
		}
	
	}
}
