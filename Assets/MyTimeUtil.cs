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
	}
	
}
