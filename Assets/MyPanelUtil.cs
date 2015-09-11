using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * v0.1 2015/09/12
 *   - add isHide()
 *   - add getMyParentCanvasName()
 */

namespace NS_MyPanelUtil
{
	public static class MyPanelUtil {
		// { --- isHide
		public static bool isHide(GameObject panel) {
			RectTransform rect = panel.GetComponent (typeof(RectTransform)) as RectTransform;
			Vector2 scale = rect.localScale;
			if (scale.x < 0.1) {
				return true;
			}
			return false;
		}
		// { --- isHide

		// { --- getMyParentCanvasName
		public static string getMyParentCanvasName(GameObject panel) {
			GameObject parentGO;
			GameObject targetGO = panel;
			
			for (int loop=0; loop<3; loop++) { // search upper three levels
				parentGO = targetGO.transform.parent.gameObject;
				if (parentGO.GetComponent<Canvas> () != null) {
					return parentGO.name;
				}
				targetGO = parentGO;
			}
			return "";
		}
		public static void Test_each_getMyParentCanvasName(string name) {
			GameObject myPanel = GameObject.Find (name);
			string canvasName = getMyParentCanvasName (myPanel);
			Debug.Log (canvasName + " - " + myPanel.name);
		}
		public static void Test_getMyParentCanvasName() {
			Test_each_getMyParentCanvasName ("Panel_1");
			Test_each_getMyParentCanvasName ("Panel_1_1");
			Test_each_getMyParentCanvasName ("Panel_1_2_1");
			
			Test_each_getMyParentCanvasName ("Panel_2");
			Test_each_getMyParentCanvasName ("Panel_2_1");
			Test_each_getMyParentCanvasName ("Panel_2_2_1");
		}
		public static Canvas getMyParentCanvasObject(GameObject panel) {
			string canvasName = MyPanelUtil.getMyParentCanvasName (panel); 
			return GameObject.Find (canvasName).GetComponent<Canvas> ();
		}
		// } --- getMyParentCanvasName

	}
}
