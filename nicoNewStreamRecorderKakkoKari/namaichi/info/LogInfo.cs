/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/04/09
 * Time: 0:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace namaichi.info
{
	/// <summary>
	/// Description of LogInfo.
	/// </summary>
	public class LogInfo
	{
		public DateTime dt = DateTime.Now;
		public string dtStr;
		public string msg = null;
		public int type = 0; //0-log 1-alart log
		public string url = null;
		public string userId = null;
		public string comId = null;

		public LogInfo(string msg, int type = 0,
				string url = null, string userId = null,
				string comId = null)
		{
			this.msg = msg;
			this.type = type;
			this.url = url;
			this.userId = userId;
			this.comId = comId;
			dtStr = dt.ToString("yyyy/MM/dd HH:mm:ss");
		}
		public string Dt
		{
			get { return dtStr; }
			set { this.dtStr = value; }
		}
		public string Msg
		{
			get { return msg; }
			set { this.msg = value; }
		}
	}
}
