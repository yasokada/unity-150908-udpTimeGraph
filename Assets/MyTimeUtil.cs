using UnityEngine;
using System.Collections;
using System; // for TimeSpan

// TODO: isOutOfWeek() . Daily,...


namespace NS_MyTimeUtil
{
	public static class MyTimeUtil {

		const int kDaysOfDay = 1;
		const int kDaysOfWeek = 7;

		public static System.DateTime getSundayH00M00S00(System.DateTime dt) {
			System.DateTime res = dt;
			res -= new TimeSpan ((int)res.DayOfWeek, res.Hour, res.Minute, res.Second);
			return res;
		}
		public static System.DateTime getTodayH00M00S00() {
			System.DateTime res = System.DateTime.Now;
			res -= new TimeSpan (0, res.Hour, res.Minute, res.Second);
			return res;
		}
		public static int getDaysFrom(System.DateTime fromDt, System.DateTime baseDt) {
			return fromDt.Subtract (baseDt).Days;
		}
//		public static double getDoubleDaysFrom(System.DateTime fromDt, System.DateTime baseDt){
//			return fromDt.Subtract (baseDt).TotalDays;
//		}		                                   

	 	public static float getTimePosition_daily(System.DateTime dt) 
		{
			System.DateTime todayH00M00S00 = getTodayH00M00S00 ();
			int daysFrom = getDaysFrom (dt, todayH00M00S00);
			if (daysFrom < 0 || daysFrom >= kDaysOfDay) {
				return -2.0f; // error. return less than -1.0f
			}

			// to [0,1]
			float totalMin = dt.Hour * 60f + dt.Minute;
			float res = totalMin / (60f * 24);
			
			// to [-1,1]
			return res * (1.0f - (-1.0f)) + (-1.0f);
		}

		public static float getTimePosition_weekly(System.DateTime dt)
		{
			// find previous sunday based on Now
			System.DateTime thisSunday = MyTimeUtil.getSundayH00M00S00 (System.DateTime.Now);

			int daysFrom = getDaysFrom (dt, thisSunday);
			if (daysFrom < 0 || daysFrom >= kDaysOfWeek) {
				return -2.0f; // error. return less than -1.0f
			}
			
			int hourMin_min = dt.Hour * 60 + dt.Minute;
			float hourMinFraction = (float)hourMin_min / (24f * 60f); // 24 hours x 60 minutes
			float ddhhmmFraction = (float)daysFrom + hourMinFraction;
			
			//		Debug.Log ("target: " + dt.ToString ());
			//		Debug.Log ("sunday: " + sunday.ToString ());
			//		Debug.Log ("days: " + daysdiff);
			//		Debug.Log ("ddhhmmFraction: " + ddhhmmFraction);
			
			float range01 = ddhhmmFraction / 7.0f; // 7 days a week
			return range01 * 2f - 1f; // [-1.0, 1.0]
		}

	}
	
}
