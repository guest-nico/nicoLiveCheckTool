/*
 * Created by SharpDevelop.
 * User: kogak
 * Date: 2025/04/11
 * Time: 5:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace namaichi.info
{
	/// <summary>
	/// Description of LiveListFilterInfo.
	/// </summary>
	public class LiveListFilterInfo
	{
		public string infoType = "タイトル";
		public string matchType = "必ず含む";
		public string str = null;
		public LiveListFilterInfo() {
			
		}
		public LiveListFilterInfo(string infoType, string matchType, string str)
		{
			this.infoType = infoType;
			this.matchType = matchType;
			this.str = str;
		}
		public string InfoType
        {
        	get { return infoType; }
            set { this.infoType = value; }
        }
		public string MatchType
        {
        	get { return matchType; }
            set { this.matchType = value; }
        }
		public string Str
        {
        	get { return str; }
            set { this.str = value; }
        }
	}
}
