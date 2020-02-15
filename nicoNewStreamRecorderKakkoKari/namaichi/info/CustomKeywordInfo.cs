/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/04/07
 * Time: 1:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;

namespace namaichi.info
{
	/// <summary>
	/// Description of CustomKeywordInfo.
	/// </summary>
	public class CustomKeywordInfo
	{
		public string name = null;
		public string matchType = "必ず含む"; //必ず含む　いずれかを含む　含まない
		public string str = null;
		public string type = "ワード"; //ワード　条件の入れ子
		public List<CustomKeywordInfo> cki = null;
		public CustomKeywordInfo()
		{

		}
		public CustomKeywordInfo(string matchType, string str, List<CustomKeywordInfo> cki, string name = null)
		{
			this.matchType = matchType;
			this.str = str;
			this.cki = cki;
			this.name = name;
		}
		public string MatchType
		{
			get { return matchType; }
			set { this.matchType = value; }
		}

		public string Type
		{
			get { return type; }
			set { this.type = value; }
		}
		public string Str
		{
			get { return str; }
			set { this.str = value; }
		}
		public string CustomBtn
		{
			get { return "入れ子設定"; }
			set { }
		}
	}
}
