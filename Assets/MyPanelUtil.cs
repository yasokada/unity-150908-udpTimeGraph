using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * v0.1 2015/09/12
 *   - add isHide()
 *   - add getMyParentCanvasName()
 */

// TODO: add doHide()

namespace NS_MyPanelUtil
{
	public static class MyPanelUtil {
		// { --- corner position related
		public static void calcCornerPosition(GameObject panel, bool atLeft, bool atBottom, out Vector3 dst) {
			Canvas aCanvas = MyPanelUtil.getMyParentCanvasObject (panel);
			if (aCanvas == null) {
				Debug.Log ("canvas not found");
				dst = new Vector3(0f, 0f, 0f);
				return; // error
			}
			RectTransform canvasRect = aCanvas.GetComponent<RectTransform> ();
			
			RectTransform panelRect = panel.GetComponent<RectTransform> ();
			float width = panelRect.rect.width;
			float height = panelRect.rect.height;
			
			dst = panel.transform.position;
			
			if (atLeft) {
				dst.x -= width * 0.5f * canvasRect.localScale.x;
			} else {
				dst.x += width * 0.5f * canvasRect.localScale.x;			
			}
			if (atBottom) {
				dst.y -= height * 0.5f * canvasRect.localScale.y;
			} else {
				dst.y += height * 0.5f * canvasRect.localScale.y;
			}
		}

		// } --- corner position related

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
