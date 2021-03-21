/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/01
 * Time: 16:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace namaichi.info
{
	/// <summary>
	/// Description of TwitterInfo.
	/// </summary>
	public class TwitterInfo
	{
		public string account = "";
		public string id = null;
		public string addDate;
		
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
		
		public TwitterInfo(string account, string addDate,
				bool popup, bool baloon, bool browser,
				bool mail, bool sound,
				bool appliA, bool appliB, bool appliC, 
				bool appliD, bool appliE, bool appliF,
				bool appliG, bool appliH, bool appliI,
				bool appliJ,
				string memo, string id) {
			this.account = account;
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
			this.id = id;
		}
		public string AccountId
        {
            get { return account; }
            set { this.account = value; }
        }
		/*
		public string LastHostDate
        {
            get { return lastHostDate; }
            set { this.lastHostDate = value; 
            	DateTime.TryParse(lastHostDate, out lastHosoDt);
            }
        }
        */
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
	}
}
