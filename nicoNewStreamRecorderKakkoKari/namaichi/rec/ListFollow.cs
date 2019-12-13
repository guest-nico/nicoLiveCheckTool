/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/02/17
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using namaichi;
using namaichi.alart;
using namaichi.info;

namespace namaichi.rec
{
	/// <summary>
	/// Description of ListFollow.
	/// </summary>
	public class ListFollow
	{
		private MainForm form;
		private Check check;
		
		public ListFollow(MainForm form, Check check)
		{
			this.form = form;
			this.check = check;
		}
		public void userFollow() {
			ikkatuFollow("user");
		}
		public void channelFollow() {
			ikkatuFollow("channel");
		}
		public void communityFollow() {
			ikkatuFollow("community");
		}
		private void ikkatuFollow(string followMode, List<AlartInfo> followList = null) {
			//followMode = user, community, channel, custom
			if (form.check.container == null) {
				form.addLogText("Cookieがありませんでした");
				return;
			}
			
			string modeStr = "";
			if (followMode == "user") modeStr = "ユーザーの";
			else if (followMode == "community") modeStr = "コミュニティの";
			else if (followMode == "channel") modeStr = "チャンネルの";
			form.addLogText(modeStr + "一括フォローを開始します");
			new FollowChecker(form, check.container).check();
			
			//var fu = new FollowChannel(false);
			while (true) {
				try {
					var okNum = 0;
					var ngNum = 0;
					var _followList = new List<AlartInfo>();
					if (followList != null) _followList = followList;
					else {
						foreach (var ai in form.alartListDataSource) {
							if ((followMode == "user" && ai.hostFollow == "フォローする") ||
									(followMode == "channel" && ai.communityId.StartsWith("ch") 
							     		&& ai.communityFollow == "フォローする") ||
							    	(followMode == "community" && ai.communityId.StartsWith("co") 
							     		&& ai.communityFollow == "フォローする")) {
							    _followList.Add(ai);
							}
						}
						foreach (var ai in form.userAlartListDataSource) {
							if (followMode == "user" && ai.hostFollow == "フォローする") {
							    _followList.Add(ai);
							}
						}
					}
					form.addLogText(_followList.Count + "件見つかりました");
					foreach (var ai in _followList) {
						if ((followMode == "user" || followMode == "custom") && ai.hostFollow == "フォローする") {
						   
							var res = form.userFollowCellClick(ai, form.alartListDataSource, form.alartList);
							res = form.userFollowCellClick(ai, form.userAlartListDataSource, form.userAlartList);
							if (res) okNum++;
							else {
								ngNum++;
								//failedList.Add(ai.hostId + "(" + ai.hostName + ")");
							}
							Thread.Sleep(2000);
						}
						if ((followMode == "community" || followMode == "channel" || followMode == "custom") && ai.communityFollow == "フォローする") {
						   
							var res = form.comChannelFollowCellClick(ai);
							if (res) okNum++;
							else {
								ngNum++;
								//failedList.Add(ai.hostId + "(" + ai.hostName + ")");
							}
							Thread.Sleep(2000);
						}
					}
					var t = okNum.ToString() + "件のフォローに成功しました";
					if (ngNum > 0) t += "\n" + ngNum + "件のフォローに失敗しました";//\n(" + string.Join(", ", failedList) + ")";
					form.showMessageBox(t, "一括" + (modeStr.Substring(0, modeStr.Length - 1)) + "フォロー結果");
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(10000);
				}
			}
		}
	}
}
