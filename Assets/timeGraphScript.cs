using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for List<>

/*
 * v0.2 2015/09/10
 *   - add ymin,ymax
 * v0.1 2015/09/09
 *   - change to timeGraph
 * above as timeGraph
 * ------------------
 * below as graphDraw
 * 
 * v0.5 2015/09/07
 *   - do not draw graph for ( canvas.localScale < 0.1 ) [hide mode]
 * v0.4 2015/09/07
 *   - fix: was destroying child[] of Panel other than "LineGroup" at clearGraph()
 * v0.3 2015/09/06
 *   - moving cosine graph
 * v0.2 2015/09/06
 *   - add sine and cosine graph
 * v0.1 2015/09/06
 *   - draw line over Panel
 */ 

public class timeGraphScript : MonoBehaviour {

	private GameObject lineGroup; // for grouping
	public GameObject timeGraphPanel;
	public Canvas myCanvas; // to obtain canvas.scale
	
	private float accTime = 0.0f;
	
	private List<Vector2> timeData;

	// TODO: do not work correctly for [-2.0f,2.0f]
	static private float m_ymin =  -1.0f; // [-1.0f]
	static private float m_ymax =  1.0f; // [1.0f]

	float getNormalizedYValue(float val_, float min_, float max_)
	{
		// convert the value to [-1.0, 1.0] using [min,max]
		float res;
		res = (val_ - min_) * (1.0f - (-1.0f)) / (max_ - min_) + (-1.0f);
		return res;
	}

	void DrawLine(List<Vector2> my2DVec, int startPos) {
		List<Vector3> myPoint = new List<Vector3>();
		for(int idx=0; idx<2; idx++) {
			myPoint.Add(new Vector3(my2DVec[startPos+idx].x, my2DVec[startPos+idx].y, 0.0f));
		}
		
		GameObject newLine = new GameObject ("Line" + startPos.ToString() );
		LineRenderer lRend = newLine.AddComponent<LineRenderer> ();
		lRend.SetVertexCount(2);
		lRend.SetWidth (0.05f, 0.05f);
		Vector3 startVec = myPoint[0];
		Vector3 endVec   = myPoint[1];

		startVec.y = getNormalizedYValue (startVec.y, m_ymin, m_ymax);
		endVec.y = getNormalizedYValue (endVec.y, m_ymin, m_ymax);

		lRend.SetPosition (0, startVec);
		lRend.SetPosition (1, endVec);
		
		newLine.transform.parent = lineGroup.transform; // for grouping
	}
	
	bool isHide(GameObject panel) {
		RectTransform rect = panel.GetComponent (typeof(RectTransform)) as RectTransform;
		Vector2 scale = rect.localScale;
		if (scale.x < 0.1) {
			return true;
		}
		return false;
	}
	
	void drawGraph(List<Vector2> my2DVec, GameObject panel) {
		if (isHide (panel)) {
			return;
		}
		
		lineGroup = new GameObject ("LineGroup");
		
		for (int idx=0; idx < my2DVec.Count - 1; idx++) {
			DrawLine (my2DVec, /* startPos=*/idx);
		}
		
		lineGroup.transform.parent = panel.transform; // to belong to panel
	}
	
	void clearGraph(GameObject panel) {
		foreach (Transform line in panel.transform) {
			if (line.gameObject.name.Contains("LineGroup")) {
				Destroy(line.gameObject);
			}
		}
	}
	
	void addPointNormalized(List<Vector2> my2DVec, GameObject panel, Vector2 point)
	{
		// point: normalized point data [-1.0, 1.0] for each of x, y
		
		RectTransform panelRect = panel.GetComponent<RectTransform> ();
		float width = panelRect.rect.width;
		float height = panelRect.rect.height;
		
		RectTransform canvasRect = myCanvas.GetComponent<RectTransform> ();
		
		Vector2 pointPos;
		
		// Bottom Left
		pointPos = panel.transform.position;
		pointPos.x += point.x * width * 0.5f * canvasRect.localScale.x;
		pointPos.y += point.y * height * 0.5f * canvasRect.localScale.y;
		my2DVec.Add (pointPos);
	}
	
	void Test_drawBox(List<Vector2> my2DVec, GameObject panel)
	{
		addPointNormalized (my2DVec, panel, new Vector2 (-1.0f, -1.0f));
		addPointNormalized (my2DVec, panel, new Vector2 (-1.0f, 1.0f));
		addPointNormalized (my2DVec, panel, new Vector2 (1.0f, 1.0f));
		addPointNormalized (my2DVec, panel, new Vector2 (1.0f, -1.0f));
		addPointNormalized (my2DVec, panel, new Vector2 (-1.0f, -1.0f));
		
		drawGraph (my2DVec, panel);
	}
	
	void Start () {
		timeData = new List<Vector2>();
	}
	
	void timeGraph_xy(List<Vector2> my2DVec, GameObject panel, float xval, float yval)
	{
		if (xval < -1.0f || xval > 1.0f) {
			return;
		}
		if (yval < m_ymin || yval > m_ymax) {
			return;
		}
		addPointNormalized (my2DVec, panel, new Vector2 (xval, yval));
	}
	
	static public float getTimePosition_float(System.DateTime dt) 
	{
		// to [0,1]
		float totalMin = dt.Hour * 60f + dt.Minute;
		float res = totalMin / (60f * 24);
		
		// to [-1,1]
		return res * (m_ymax - m_ymin) + m_ymin;
	}
	
	static private float xval = -1.0f; // dummy value at first
	static private float yval =  0.5f; // dummy value at first
	static private bool isSet = false;
	static private float preX = -2.0f; // should have less than -1.0f at first 
	
	static public void SetXYVal(System.DateTime time, float yval_)
	{
		float xwork = getTimePosition_float (time);
		if (xwork < preX) {
			return; // revert time
		}
		xval = xwork;
		yval = yval_;
		if (xval >= -1.0f) { 
			preX = xval;
			isSet = true;
		}
	}
	
	void Update() {
		accTime += Time.deltaTime;
		if (accTime < 0.3f) { // for every 300 msec
			return;
		}
		accTime = 0.0f;
		
		clearGraph (timeGraphPanel);
		//		Test_drawBox (timeData, timeGraphPanel);
		
		if (isSet) {
			isSet = false;
			timeGraph_xy (timeData, timeGraphPanel, xval, yval);
		}
		
		drawGraph (timeData, timeGraphPanel);
	}
}
