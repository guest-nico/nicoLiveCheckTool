/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/12/11
 * Time: 10:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace namaichi.info
{
	/// <summary>
	/// Description of TanzakuOnAir.
	/// </summary>
	public class TanzakuOnAir
	{
		public meta meta;
		public TanzakuData data;
	}
	public class TanzakuData {
		public List<TanzakuItem> items;
		public string cursor;
		public object annotation;
	}
	public class TanzakuItem {
		public string id;
		//public string title;
		public string description;
		public TanzakuShowTime showTime; 
	}
	public class TanzakuShowTime {
		public DateTime beginAt;
		public DateTime endAt;
	}
}
