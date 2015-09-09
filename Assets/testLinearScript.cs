using UnityEngine;
using System.Collections;

public class testLinearScript : MonoBehaviour {

	static private float m_ymin =  -2.0f; // [-1.0f]
	static private float m_ymax =  2.0f; // [1.0f]
	
	float getNormalizedYValue(float val_, float min_, float max_)
	{
		// convert the value to [-1.0, 1.0] using [min,max]
		float res;
		res = (val_ - min_) * (1.0f - (-1.0f)) / (max_ - min_) + (-1.0f);
		return res;
	}

	void test_linear() {
		float [] yval = new float [] { 0.2f, -0.9f, 0.3f, -0.9f };

		float work;
		for (int idx=0; idx < 4; idx++) {
			work = getNormalizedYValue(yval[idx], m_ymin, m_ymax);

			Debug.Log(work.ToString());
		}
	}

	void Start () {
		test_linear ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
