using UnityEngine;
using System.Collections;
using System; // for TimeSpan

// TODO: isOutOfWeek() . Daily,...


namespace NS_MyTimeUtil
{
	public static class MyTimeUtil {

		const int kDaysOfDay = 1;
		const int kDaysOfWeek = 7;

		enum xscaletype {
			Daily = 0,
			Weekly,
			Monthly,
			Yearly,
		};

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
		public static System.DateTime getFirstDayOfMonth(System.DateTime dt) {
			return new DateTime (dt.Year, dt.Month, 1);
		}
		public static System.DateTime getLastDayOfMonth(System.DateTime dt) {
			var firstDay = getFirstDayOfMonth (dt);
			return firstDay.AddMonths (1).AddDays (-1);
		}
		public static int getDaysInMonth(System.DateTime dt) {
			var firstDay = getFirstDayOfMonth (dt);
			var lastDay = getLastDayOfMonth (dt);
			return getDaysFrom (lastDay, firstDay) + 1;
		}

		public static int getDaysInYear(System.DateTime dt) {
			System.DateTime firstday = new DateTime (dt.Year, 1, 1);
			System.DateTime firstDayOfNextYear = new DateTime (dt.Year + 1, 1, 1);
			return getDaysFrom (firstDayOfNextYear, firstday);
		}

		//--------------------------------------------------

		static int getDaysWithOutOfRangeCheck(System.DateTime dt, int xstype, out bool outOfRange) {
			int daysFrom = 0;

			// Daily
			if (xstype == (int)xscaletype.Daily) {
				System.DateTime todayH00M00S00 = getTodayH00M00S00 ();
				daysFrom = getDaysFrom(dt, todayH00M00S00);
				if (daysFrom >= 1) {
					outOfRange = true;
					return daysFrom;
				}
			}

			// Weekly
			if (xstype == (int)xscaletype.Weekly) {
				// find previous sunday based on Now
				System.DateTime thisSunday = MyTimeUtil.getSundayH00M00S00 (System.DateTime.Now);
				daysFrom = getDaysFrom (dt, thisSunday);
				if (daysFrom >= kDaysOfWeek) {
					outOfRange = true;
					return daysFrom;
				}
			}

			// Monthly
			if (xstype == (int)xscaletype.Monthly) {
				var firstDay = getFirstDayOfMonth(System.DateTime.Now);
				daysFrom = getDaysFrom(dt, firstDay);
				if (daysFrom >= getDaysInMonth(firstDay)) {
					outOfRange = true;
					return daysFrom;
				}
			}

			// Yearly
			if (xstype == (int)xscaletype.Yearly) {
				var firstDay = new DateTime(dt.Year, 1, 1);
				daysFrom = getDaysFrom(dt, firstDay);
				if (daysFrom >= getDaysInYear(firstDay)) {
					outOfRange = true;
					return daysFrom;
				}
			}

			if (daysFrom < 0) {
				outOfRange = true;
				return daysFrom;
			}
			outOfRange = false;
			return daysFrom;
		}

	 	public static float getTimePosition_daily(System.DateTime dt) 
		{
			bool isOutOfRange = false;
			int daysFrom = getDaysWithOutOfRangeCheck (dt, (int)xscaletype.Daily, out isOutOfRange);
			if (isOutOfRange) {
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
			bool isOutOfRange = false;
			int daysFrom = getDaysWithOutOfRangeCheck (dt, (int)xscaletype.Weekly, out isOutOfRange);
			if (isOutOfRange) {
				return -2.0f; // error. return less than -1.0f
			}

			int hourMin_min = dt.Hour * 60 + dt.Minute;
			float hourMinFraction = (float)hourMin_min / (24f * 60f); // 24 hours x 60 minutes
			float ddhhmmFraction = (float)daysFrom + hourMinFraction;

			float range01 = ddhhmmFraction / 7.0f; // 7 days a week
			return range01 * 2f - 1f; // [-1.0, 1.0]
		}

		public static float getTimePosition_monthly(System.DateTime dt)
		{
			bool isOutOfRange = false;
			int daysFrom = getDaysWithOutOfRangeCheck (dt, (int)xscaletype.Monthly, out isOutOfRange);
			if (isOutOfRange) {
				return -2.0f; // error. return less than -1.0f
			}

			int hourMin_min = dt.Hour * 60 + dt.Minute;
			float hourMinFraction = (float)hourMin_min / (24f * 60f); // 24 hours x 60 minutes
			float ddhhmmFraction = (float)daysFrom + hourMinFraction;

			var daysInMonth = getDaysInMonth (dt);
			float range01 = ddhhmmFraction / (float)daysInMonth;
			return range01 * 2f - 1f; // [-1.0, 1.0]
		}
		public static float getTimePosition_yearly(System.DateTime dt)
		{
			bool isOutOfRange = false;
			int daysFrom = getDaysWithOutOfRangeCheck (dt, (int)xscaletype.Yearly, out isOutOfRange);
			if (isOutOfRange) {
				return -2.0f; // error. return less than -1.0f
			}

//			Debug.Log (daysFrom.ToString () + " " + dt.ToString ());
			
//			int hourMin_min = dt.Hour * 60 + dt.Minute;
//			float hourMinFraction = (float)hourMin_min / (24f * 60f); // 24 hours x 60 minutes
//			float ddhhmmFraction = (float)daysFrom + hourMinFraction;
			
			var daysInYear = getDaysInYear (dt);
			float range01 = (float)daysFrom / (float)daysInYear;

			return range01 * 2f - 1f; // [-1.0, 1.0]
		}

	}
	
}
