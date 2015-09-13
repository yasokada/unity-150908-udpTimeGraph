﻿using UnityEngine;
using System.Collections;
//using System.Net; // for Dns.XXX()
using System; // for TimeSpan

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
