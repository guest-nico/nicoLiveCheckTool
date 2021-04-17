/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/25
 * Time: 18:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
//using reser

namespace namaichi.info
{
	/// <summary>
	/// Description of ActivityTimeline.
	/// </summary>
	public class ActivityTimeline
	{
		public meta meta;
		//public TimelineData data;
		public List<TimelineItem> data;
	}
	public class TimelineData {
		public List<TimelineItem> items;
		//public string errors;
	}
	public class TimelineItem {
		public string id;
		public DateTime updated;
		public Actor actor;
		public string title;
		public TimelineObject @object;
	}
	public class Actor {
		public string url = null;
		public string name;
		public string icon;
	}
	public class TimelineObject {
		public string url;
		public string name;
	}
}
