/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/05/29
 * Time: 14:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace namaichi.rec
{
	/// <summary>
	/// Description of FollowCommunity.
	/// </summary>
	public class FollowChannel
	{
		private bool isSub;
		public FollowChannel(bool isSub)
		{
			this.isSub = isSub;
		}
		public bool followChannel(string comId, CookieContainer cc, MainForm form, config.config cfg)
		{
			//			var isJikken = res.IndexOf("siteId&quot;:&quot;nicocas") > -1;
			//			var comId = (isJikken) ? util.getRegGroup(res, "&quot;followPageUrl&quot;\\:&quot;.+?motion/(.+?)&quot;") :
			//					util.getRegGroup(res, "Nicolive_JS_Conf\\.Recommend = \\{type\\: 'community', community_id\\: '(co\\d+)'");
			if (comId == null)
			{
				form.addLogText("このチャンネルはフォローできませんでした。" + util.getMainSubStr(isSub, true));
				return false;
			}

			var isJoinedTask = join(comId, cc, form, cfg, isSub);
			//			isJoinedTask.Wait();
			return isJoinedTask;
			//			return false;
		}
		private bool join(string comId, CookieContainer cc, MainForm form, config.config cfg, bool isSub)
		{
			for (int i = 0; i < 2; i++)
			{
				//				var myPageUrl = "http://www.nicovideo.jp/my";
				var comUrl = "https://ch.nicovideo.jp/" + comId;
				var url = "https://ch.nicovideo.jp/api/addbookmark?";
				var headers = new WebHeaderCollection();
				headers.Add("X-Requested-With", "XMLHttpRequest");
				headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");

				try
				{
					var pageRes = util.getPageSource(comUrl, cc);
					var param = util.getRegGroup(pageRes, "params=\"(channel_id.+?)\"");
					if (param == null) return false;
					param = param.Replace("&amp;", "&");
					param += "&_=" + util.getUnixTime();

					var res = util.getPageSource(url + param, cc);
					return res.IndexOf("status\":\"succeed\"") > -1;

				}
				catch (Exception e)
				{
					form.addLogText("フォローに失敗しました。" + util.getMainSubStr(isSub, true));
					util.debugWriteLine(e.Message + e.StackTrace);
					continue;
					//					return false;
				}
			}
			form.addLogText("フォローに失敗しました。" + util.getMainSubStr(isSub, true));
			util.debugWriteLine("フォロー失敗");
			return false;
		}
		public bool unFollowChannel(string comId, CookieContainer cc, MainForm form, config.config cfg)
		{
			if (comId == null)
			{
				form.addLogText("このチャンネルはフォロー解除できませんでした。" + util.getMainSubStr(isSub, true));
				return false;
			}

			var isUnJoinedTask = unJoin(comId, cc, form, cfg, isSub);
			//			isJoinedTask.Wait();
			return isUnJoinedTask;
			//			return false;
		}
		private bool unJoin(string comId, CookieContainer cc, MainForm form, config.config cfg, bool isSub)
		{
			for (int i = 0; i < 5; i++)
			{
				//				var myPageUrl = "http://www.nicovideo.jp/my";
				var comUrl = "https://ch.nicovideo.jp/" + comId;
				var url = "https://ch.nicovideo.jp/api/deletebookmark?";
				var headers = new WebHeaderCollection();
				headers.Add("X-Requested-With", "XMLHttpRequest");
				headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");

				try
				{
					var pageRes = util.getPageSource(comUrl, cc);
					var param = util.getRegGroup(pageRes, "params=\"(channel_id.+?)\"");
					if (param == null) return false;
					param = param.Replace("&amp;", "&");
					param += "&_=" + util.getUnixTime();

					var res = util.getPageSource(url + param, cc);
					return res.IndexOf("status\":\"succeed\"") > -1;

				}
				catch (Exception e)
				{
					form.addLogText("フォローに失敗しました。" + util.getMainSubStr(isSub, true));
					util.debugWriteLine(e.Message + e.StackTrace);
					continue;
					//					return false;
				}
			}
			form.addLogText("フォローに失敗しました。" + util.getMainSubStr(isSub, true));
			util.debugWriteLine("フォロー失敗");
			return false;
		}
	}
}
