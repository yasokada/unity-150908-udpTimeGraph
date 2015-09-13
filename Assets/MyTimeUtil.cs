using UnityEngine;
using System.Collections;
using System; // for TimeSpan

// TODO: isOutOfWeek() . Daily,...


namespace NS_MyTimeUtil
{
	public static class MyTimeUtil {
		public static System.DateTime getSundayH00M00S00(System.DateTime dt) {
			System.DateTime res = dt;
			res -= new TimeSpan ((int)res.DayOfWeek, res.Hour, res.Minute, res.Second);
			return res;
		}
		public static double getDaysFrom(System.DateTime fromDt, System.DateTime baseDt) {
			return fromDt.Subtract (baseDt).TotalDays;
		}

		public static float getTimePosition_weekly(System.DateTime dt)
		{
			System.DateTime sunday = MyTimeUtil.getSundayH00M00S00 (dt);
			
			int daysdiff = dt.Subtract (sunday).Days;

			double daysFrom = getDaysFrom (dt, sunday);
			Debug.Log ("daysfrom: " + daysFrom.ToString ("0"));

			
			int hourMin_min = dt.Hour * 60 + dt.Minute;
			float hourMinFraction = (float)hourMin_min / (24f * 60f); // 24 hours x 60 minutes
			float ddhhmmFraction = (float)daysdiff + hourMinFraction;
			
			//		Debug.Log ("target: " + dt.ToString ());
			//		Debug.Log ("sunday: " + sunday.ToString ());
			//		Debug.Log ("days: " + daysdiff);
			//		Debug.Log ("ddhhmmFraction: " + ddhhmmFraction);
			
			float range01 = ddhhmmFraction / 7.0f; // 7 days a week
			return range01 * 2f - 1f; // [-1.0, 1.0]
		}

	}
	
}
