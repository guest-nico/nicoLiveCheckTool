/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/12/11
 * Time: 0:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace namaichi.info
{
	/// <summary>
	/// Description of CategoryRecent.
	/// </summary>
	public class CategoryRecent {
		public meta meta;
		public List<_data> data;
		
	}
	public class meta {
		public string status;
		public string errorCode;
		public bool hasNext;
		public string maxId;
		public string minId;
	}
	public class _data {
		public string id;
		public string title;
		public string thumbnailUrl;
		public screenshotThumbnail screenshotThumbnail;
		
		public string watchPageUrl;
		public string watchPageUrlAtTopPlayer;
		public string providerType;//	"community"
		public string liveCycle;//	"ON_AIR"
		public long beginAt;//	1575928335000
		public long endAt;//	1575930135000
		public string isFollowerOnly;//	false
		public socialGroup socialGroup;
		public programProvider programProvider;
		public statistics statistics;
		public timeshift timeshift;
		public nicoad nicoad;
		public bool isPayProgram;
		public string getThumb() {
			if (socialGroup != null && !string.IsNullOrEmpty(socialGroup.thumbnailUrl) &&
					socialGroup.id != "co0")
				return socialGroup.thumbnailUrl;
			if (programProvider != null && !string.IsNullOrEmpty(programProvider.icon))
				return programProvider.icon;
			if (screenshotThumbnail != null && !string.IsNullOrEmpty(screenshotThumbnail.liveScreenshotThumbnailUrl)) 
				return screenshotThumbnail.liveScreenshotThumbnailUrl;
			return "";
		}
		public RssItem getRssItem(string categoryName) {
			try {
				var thumb = getThumb();
				var pubDt = util.getUnixToDatetime(beginAt / 1000);
				var socialName = socialGroup != null ? socialGroup.name : "削除されたコミュニティ";
				var socialId = socialGroup != null ? socialGroup.name : "co0";
				var item = new RssItem(title, id,
				                       pubDt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss"), "", socialName, socialId,
						programProvider.name, thumb, isFollowerOnly, "", isPayProgram
						);
				item.setUserId(programProvider.id);
				item.category = getCategoryName(categoryName);
				item.pubDateDt = pubDt;
				item.setTag(item.category.ToArray());
				item.type = providerType;
				return item;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source  + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private List<string> getCategoryName(string categoryName) {
			var mainCategories = new string[]{"一般", "やってみた",
					"ゲーム", "動画紹介"};
			var categoryNames = new string[]{"common", "try", "live", "req"};
			var cate = mainCategories[Array.IndexOf(categoryNames, categoryName)];
			return new List<string>{cate};
		}
	}
	public class screenshotThumbnail {
		public string liveScreenshotThumbnailUrl;
		public string tsScreenshotThumbnailUrl;
	}
	public class socialGroup {
		public string id;//	"co3861241"
		public string name;//	"市松みずはのお屋敷"
		public string thumbnailUrl;//	"https://secure-dcdn.cdn.nimg.jp/comch/community-icon/128x128/co3861241.jpg?1572912291"
	}
	public class programProvider {
		public string id;//	"85212564"
		public string name;//	"市松みずは"
		public string icon;//	"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/8521/85212564.jpg?1541349244"
		public string iconSmall;//	"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/s/8521/85212564.jpg?1541349244"
	}
	public class statistics {
		public string watchCount;//	15
		public string commentCount;//	8
		public string reservationCount;//	0
	}
	public class timeshift {
		public bool isPlayable;//	false
		public bool isPreparing;//	false
		public bool isReservable;//	true
	}
	public class nicoad {
		public string userName;//	""
		public string totalPoint;//	3900
		public string decoration;//	"silver"
	}
}
