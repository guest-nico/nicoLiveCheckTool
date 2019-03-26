/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/20
 * Time: 19:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace namaichi.info
{
	/// <summary>
	/// Description of RecInfo.
	/// </summary>
	public class AlartInfo {
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
		public bool isRecentColor = false;
		
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
				string keyword) {
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
            get { return keyword; }
            set { this.keyword = value; }
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
            set { this.lastHostDate = value; 
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
        public string Memo
        {
            get { return memo; }
            set { this.memo = value; }
        }
        public string toString() {
        	return "コミュニティID=" + communityId + " コミュニティ名=" + communityName + " ユーザーID=" + hostId + " ユーザー名=" + hostName;
        }
        //public void setLastHosoDate
	}
}
