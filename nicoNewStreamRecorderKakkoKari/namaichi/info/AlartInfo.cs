/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/20
 * Time: 19:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace namaichi.info
{
	/// <summary>
	/// Description of RecInfo.
	/// </summary>
	public class AlartInfo
	{
		public string communityId = "";
		public string hostId = "";
		public string communityName = "";
		public string hostName = "";
		public string communityFollow = "";
		public string hostFollow = "";
		public string lastHostDate = "";
		public string addDate = "";

		public bool popup = false;
		public bool baloon = false;
		public bool browser = false;
		public bool mail = false;
		public bool sound = false;
		public bool appliA = false;
		public bool appliB = false;
		public bool appliC = false;
		public bool appliD = false;
		public bool appliE = false;
		public bool appliF = false;
		public bool appliG = false;
		public bool appliH = false;
		public bool appliI = false;
		public bool appliJ = false;
		public string memo = "";
		public string lastLvid = "";
		public string keyword = "";

		public DateTime lastHosoDt = DateTime.MinValue;
		public int recentColorMode = 0;//0-no 1-recent 2-followerOnly
		public int userIdColorType = 0;
		public int comIdColorType = 0;

		public Color textColor = Color.Black;
		public Color backColor = Color.FromArgb(255, 224, 255);
		public int soundType = 0;
		public bool isSoundId = true;

		//public bool isAnd = true;
		public bool isMustCom = true;
		public bool isMustUser = true;
		public bool isMustKeyword = true;
		public List<CustomKeywordInfo> cki = null;
		public bool isCustomKeyword = false;
		//public bool isSimpleKeywordOr = true;

		public AlartInfo(string communityId, string hostId,
				string communityName, string hostName,
				string lastHostDate, string addDate,
				bool popup, bool baloon, bool browser,
				bool mail, bool sound,
				bool appliA, bool appliB, bool appliC,
				bool appliD, bool appliE, bool appliF,
				bool appliG, bool appliH, bool appliI,
				bool appliJ,
				string memo, string communityFollow,
				string hostFollow, string lastLvid,
				string keyword, Color textColor,
				Color backColor, int defaultSound,
				bool isDefaultSoundId, bool isMustCom,
				bool isMustUser, bool isMustKeyword,
				List<CustomKeywordInfo> customKeyword,
				bool isCustomKeyword)
		{
			this.communityId = communityId;
			this.hostId = hostId;
			this.communityName = communityName;
			this.hostName = hostName;
			this.LastHostDate = lastHostDate;
			this.addDate = addDate;

			this.popup = popup;
			this.baloon = baloon;
			this.sound = sound;
			this.mail = mail;
			this.browser = browser;
			this.appliA = appliA;
			this.appliB = appliB;
			this.appliC = appliC;
			this.appliD = appliD;
			this.appliE = appliE;
			this.appliF = appliF;
			this.appliG = appliG;
			this.appliH = appliH;
			this.appliI = appliI;
			this.appliJ = appliJ;
			this.memo = memo;
			this.communityFollow = communityFollow;
			this.hostFollow = hostFollow;
			this.lastLvid = lastLvid;
			this.keyword = keyword;
			this.textColor = textColor;
			this.backColor = backColor;
			this.soundType = defaultSound;
			this.isSoundId = isDefaultSoundId;
			//this.isAnd = isAnd;
			this.isMustCom = isMustCom;
			this.isMustUser = isMustUser;
			this.isMustKeyword = isMustKeyword;
			this.cki = customKeyword;
			this.isCustomKeyword = isCustomKeyword;
			//this.isSimpleKeywordOr = isSimpleKeywordOr; 
		}
		public AlartInfo(string communityId, string hostId,
				string communityName, string hostName,
				string lastHostDate, string addDate,
				bool popup, bool baloon, bool browser,
				bool mail, bool sound,
				bool appliA, bool appliB, bool appliC,
				bool appliD, bool appliE, bool appliF,
				bool appliG, bool appliH, bool appliI,
				bool appliJ,
				string memo, string communityFollow,
				string hostFollow, string lastLvid,
				string keyword)
		{
			this.communityId = communityId;
			this.hostId = hostId;
			this.communityName = communityName;
			this.hostName = hostName;
			this.LastHostDate = lastHostDate;
			this.addDate = addDate;

			this.popup = popup;
			this.baloon = baloon;
			this.sound = sound;
			this.mail = mail;
			this.browser = browser;
			this.appliA = appliA;
			this.appliB = appliB;
			this.appliC = appliC;
			this.appliD = appliD;
			this.appliE = appliE;
			this.appliF = appliF;
			this.appliG = appliG;
			this.appliH = appliH;
			this.appliI = appliI;
			this.appliJ = appliJ;
			this.memo = memo;
			this.communityFollow = communityFollow;
			this.hostFollow = hostFollow;
			this.lastLvid = lastLvid;
			this.keyword = keyword;
		}
		public string CommunityId
		{
			get { return communityId; }
			set { this.communityId = value; }
		}
		public string HostId
		{
			get { return hostId; }
			set { this.hostId = value; }
		}
		public string CommunityName
		{
			get { return communityName; }
			set { this.communityName = value; }
		}
		public string HostName
		{
			get { return hostName; }
			set { this.hostName = value; }
		}
		public string Keyword
		{
			get
			{
				if (isCustomKeyword)
					return "カスタム設定(" + cki[0].name + ")";
				else return keyword;
			}
			set
			{
				if (isCustomKeyword) return;
				else this.keyword = value;
			}
		}

		public string IsAnd
		{
			//get { return isAnd ? "全て合致" : "いずれか"; }
			get { return ""; }
			//set { this.isAnd = value == "全て合致"; }
			set { }
		}

		public string CommunityFollow
		{
			get { return communityFollow; }
			set { this.communityFollow = value; }
		}
		public string HostFollow
		{
			get { return hostFollow; }
			set { this.hostFollow = value; }
		}
		public string LastHostDate
		{
			get { return lastHostDate; }
			set
			{
				this.lastHostDate = value;
				DateTime.TryParse(lastHostDate, out lastHosoDt);
				var i = 0;
			}
		}
		public string AddDate
		{
			get { return addDate; }
			set { this.addDate = value; }
		}
		public bool Popup
		{
			get { return popup; }
			set { this.popup = value; }
		}
		public bool Baloon
		{
			get { return baloon; }
			set { this.baloon = value; }
		}
		public bool Browser
		{
			get { return browser; }
			set { this.browser = value; }
		}
		public bool Mail
		{
			get { return mail; }
			set { this.mail = value; }
		}
		public bool Sound
		{
			get { return sound; }
			set { this.sound = value; }
		}
		public bool AppliA
		{
			get { return appliA; }
			set { this.appliA = value; }
		}
		public bool AppliB
		{
			get { return appliB; }
			set { this.appliB = value; }
		}
		public bool AppliC
		{
			get { return appliC; }
			set { this.appliC = value; }
		}
		public bool AppliD
		{
			get { return appliD; }
			set { this.appliD = value; }
		}
		public bool AppliE
		{
			get { return appliE; }
			set { this.appliE = value; }
		}
		public bool AppliF
		{
			get { return appliF; }
			set { this.appliF = value; }
		}
		public bool AppliG
		{
			get { return appliG; }
			set { this.appliG = value; }
		}
		public bool AppliH
		{
			get { return appliH; }
			set { this.appliH = value; }
		}
		public bool AppliI
		{
			get { return appliI; }
			set { this.appliI = value; }
		}
		public bool AppliJ
		{
			get { return appliJ; }
			set { this.appliJ = value; }
		}
		public string SoundType
		{
			get
			{
				if (soundType == 1) return "音A";
				else if (soundType == 2) return "音B";
				else if (soundType == 3) return "音C";
				else return "ﾃﾞﾌｫﾙﾄ";
			}
			set
			{
				if (value == "音A") this.soundType = 1;
				else if (value == "音B") this.soundType = 2;
				else if (value == "音C") this.soundType = 3;
				else this.soundType = 0;
			}
		}
		public string Memo
		{
			get { return memo; }
			set { this.memo = value; }
		}

		public string toString()
		{
			return "コミュニティID=" + communityId + " コミュニティ名=" + communityName + " ユーザーID=" + hostId + " ユーザー名=" + hostName;
		}
		public void setBehavior(bool[] arr)
		{
			try
			{
				this.popup = arr[0];
				this.baloon = arr[1];
				this.browser = arr[2];
				this.mail = arr[3];
				this.sound = arr[4];
				this.appliA = arr[5];
				this.appliB = arr[6];
				this.appliC = arr[7];
				this.appliD = arr[8];
				this.appliE = arr[9];
				this.appliF = arr[10];
				this.appliG = arr[11];
				this.appliH = arr[12];
				this.appliI = arr[13];
				this.appliJ = arr[14];
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
	}
}
