using UnityEngine;
using System.Collections;

// Test_hello GameObject activated/deactivated checker

public class Test_helloGoChecker : MonoBehaviour {

	float accTime = 0.0f;

	bool preState = false;

	void CheckGameObjectAndRun() {
		GameObject weekly = GameObject.Find ("Test_hello");
		if (weekly == null) { // null if gameobject is deactivated
			preState = false;
			return;
		}
		if (preState == true) {
			return;
		}
		preState = true;

		Test_hello.Test_showHelloMessage ();
	}

	void Update () {
		accTime += Time.deltaTime;
		if (accTime < 0.3f) { // every 300msec
			return;
		}
		accTime = 0.0f;

		CheckGameObjectAndRun ();

	}
}
