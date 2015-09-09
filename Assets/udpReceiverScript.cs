using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for Text

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NS_MyNetUtil; // for MyNetUtil.getMyIPAddress()

/*
 * v0.1 2015/09/09
 *   - send received data to timeGraph
 * above as updReceiver
 * -----------------------
 * below as UdpEchoServer
 * v0.4 2015/08/30
 *   - separate IP address get method to MyNetUtil.cs
 * v0.3 2015/08/30
 *   - show version info
 *   - correct .gitignore file
 * v0.2 2015/08/29
 *   - fix for negative value for delay_msec
 *   - fix for string to int
 *   - fix for android (splash freeze)
 * v0.1 2015/08/29
 *   following features have been implemented.
 *   - delay before echo back
 *   - echo back
 */

public class udpReceiverScript : MonoBehaviour {
	Thread rcvThr;
	UdpClient client;
	public int port = 6000;
	
	public const string kAppName = "UDP_Receiver";
	public const string kVersion = "v0.1";
	
	public string lastRcvd;
	
	public Text myipText; // to show my IP address(port)
	public Text recvdText;
	public Text versionText;
	
	private bool stopThr = false;

	void Start () {
		versionText.text = kAppName + " " + kVersion;
		myipText.text = MyNetUtil.getMyIPAddress() + " (" + port.ToString () + ")";
		startTread ();
	}
	
	string getTextMessage(string rcvd)
	{
		if (rcvd.Length == 0) {
			return "";
		}
		string msg = 
			"rx: " + rcvd + System.Environment.NewLine
				+ "tx: " + rcvd;
		return msg;
	}
	
	void Update() {
		recvdText.text = getTextMessage (lastRcvd);
	}
	
	void startTread() {
		Debug.Log ("init");
		rcvThr = new Thread( new ThreadStart(FuncRcvData));
		rcvThr.Start ();
	}
	
	void OnApplicationQuit() {
		stopThr = true;
		rcvThr.Abort ();
	}

	void sendToGraph(string text)
	{
		// udp text should be like "-0.2" in range [-1.0, 1.0]
		float yval = float.Parse (text);
		timeGraphScript.SetXYVal (System.DateTime.Now, yval);
	}

	private void FuncRcvData()
	{
		client = new UdpClient (port);
		client.Client.ReceiveTimeout = 300; // msec
		client.Client.Blocking = false;
		while (stopThr == false) {
			try {
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);
				string text = Encoding.ASCII.GetString(data);
				lastRcvd = text;

				if (lastRcvd.Length > 0) {
					sendToGraph(lastRcvd);
					client.Send(data, data.Length, anyIP); // echo
				}
			}
			catch (Exception err)
			{
				//              print(err.ToString());
			}
			
			// without this sleep, on adnroid, the app will not start (freeze at Unity splash)
			Thread.Sleep(20); // 200
		}
		client.Close ();
	}
}


