using UnityEngine;
using System.Collections;

/* 
 * v0.11 2015/09/12
 *   - add data export feature (set,export command)
 * v0.10 2015/09/12
 *   - add monthly, yearly time scale
 *   - fix daily, weekly graph miscalculation
 * v0.9 2015/09/12
 *   - add Test related GameObjects and scripts
 * v0.8 2015/09/12
 *   - can show x scale in daily and weekly (monthly and yearly not yet)
 * v0.7 2015/09/12
 *   - graph date is kept as <System.DateTime, float> so that x axis scale can be changed to weekly, etc.
 * v0.6 2015/09/12
 *   - move several functions to MyPanelUtils.cs
 * v0.5 2015/09/11
 *   - display ymin and ymax on the left of the panel (graphScale)
 * v0.4 2015/09/10
 *   - can handle set,yrange command
 */ 

namespace NS_appInfo // NS stands for NameSpace
{
	public static class AppInfo
	{
		public const string Version = "v0.11";
		public const string Name = "udpTimeGraph";
	}
}
