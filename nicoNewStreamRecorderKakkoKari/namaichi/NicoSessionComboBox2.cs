/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/08/24
 * Time: 3:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SunokoLibrary.Windows.Forms;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace namaichi
{
	using SunokoLibrary.Application;
	using SunokoLibrary.Windows.ViewModels;

	/// <summary>
	/// ニコニコ動画アカウント一覧の表示用コンボボックス。
	/// </summary>
	public class NicoSessionComboBox2 : BrowserComboBox
	{
#pragma warning disable 1591
		protected override void InitLayout()
		{
			base.InitLayout();
			Initialize(new CookieSourceSelector(CookieGetters.Default, importer => new NicoAccountSelectorItem(importer)));
		}
#pragma warning restore 1591

		/*
		[Browsable(false), DefaultValue(null)]
        public CookieSourceSelector Selector { get; private set; }
        
		/// <summary>
        /// 指定したViewModelでコントロールを初期化します。
        /// </summary>
        public void Initialize(CookieSourceSelector viewModel)
        {
            if (DesignMode)
                return;
            if (Selector != null)
            {
                Selector.PropertyChanged -= _selector_PropertyChanged;
                Selector.Items.CollectionChanged -= _selector_Items_CollectionChanged;
            }
            Selector = viewModel;
            Items.Clear();
            if (Selector != null)
            {
                Selector.PropertyChanged += _selector_PropertyChanged;
                Selector.Items.CollectionChanged += _selector_Items_CollectionChanged;
                SelectedIndex = Selector.SelectedIndex;
                var tsk = Selector.UpdateAsync();
            }
        }
		void _selector_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "IsUpdating":
                    Enabled = !Selector.IsUpdating;
                    if (Selector.IsUpdating)
                        BeginUpdate();
                    else
                        EndUpdate();
                    break;
                case "SelectedIndex":
                    SelectedIndex = Selector.SelectedIndex;
                    break;
            }
        }
		void _selector_Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (var i = 0; i < e.NewItems.Count; i++)
                    {
                        var item = (CookieSourceItem)e.NewItems[i];
                        Items.Insert(e.NewStartingIndex + i, item.DisplayText ?? string.Empty);
                        item.PropertyChanged += _selector_item_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (var i = 0; i < e.OldItems.Count; i++)
                    {
                        var item = (CookieSourceItem)e.OldItems[i];
                        item.PropertyChanged -= _selector_item_PropertyChanged;
                        Items.RemoveAt(e.OldStartingIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var oldItem = (CookieSourceItem)e.OldItems[0];
                    var newItem = (CookieSourceItem)e.NewItems[0];
                    oldItem.PropertyChanged -= _selector_item_PropertyChanged;
                    newItem.PropertyChanged += _selector_item_PropertyChanged;
                    Items[e.NewStartingIndex] = newItem.DisplayText ?? string.Empty;
                    break;
                case NotifyCollectionChangedAction.Move:
                    var mvItem = (string)Items[e.NewStartingIndex];
                    Items.RemoveAt(e.OldStartingIndex);
                    Items.Insert(e.NewStartingIndex, mvItem);
                    break;
            }
        }
		void _selector_item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "DisplayText":
                    var item = (CookieSourceItem)sender;
                    var idx = Selector.Items.IndexOf(item);
                    Items[idx] = item.DisplayText;
                    break;
            }
        }
		*/
		class NicoAccountSelectorItem : CookieSourceItem
		{
			public NicoAccountSelectorItem(ICookieImporter importer) : base(importer) { }
			string _accountName, _displayText;
			public string AccountName
			{
				get { return _accountName; }
				private set
				{
					_accountName = value;
					OnPropertyChanged();
				}
			}
			public override string DisplayText
			{
				get { return _displayText; }
				protected set
				{
					_displayText = value;
					OnPropertyChanged();
				}
			}
			public async override void Initialize()
			{
				var baseText = string.Format("{0}{1}{2}",
					Importer.SourceInfo.IsCustomized ? "カスタム設定 " : string.Empty,
					Importer.SourceInfo.BrowserName,
					Importer.SourceInfo.ProfileName.ToLowerInvariant() == "default" ? string.Empty : string.Format(" {0}", Importer.SourceInfo.ProfileName));
				DisplayText = string.Format("{0} (loading...)", baseText);
				AccountName = await GetUserName(Importer);
				DisplayText = string.IsNullOrEmpty(AccountName) == false
					? string.Format("{0} ({1})", baseText, AccountName) : baseText;
			}
			static async Task<string> GetUserName(ICookieImporter cookieImporter)
			{
				try
				{
					var url = new Uri("https://www.nicovideo.jp/my/channel");
					var container = new CookieContainer();
					var client = new HttpClient(new HttpClientHandler() { CookieContainer = container });
					var result = await cookieImporter.GetCookiesAsync(url);

					if (result.Status != CookieImportState.Success) return null;
					foreach (Cookie c in result.Cookies)
					{
						if (Regex.IsMatch(c.Name, "[^0-9a-zA-Z\\._\\-\\[\\]%#&=\":\\{\\} \\(\\)/\\?\\|]") ||
								   Regex.IsMatch(c.Value, "[^0-9a-zA-Z\\._\\-\\[\\]%#&=\":\\{\\} \\(\\)/\\?\\|]"))
						{
							util.debugWriteLine(c.Name + " " + c.Value);
							continue;
						}
						try
						{
							container.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
						}
						catch (Exception e)
						{
							util.debugWriteLine(e.Message + e.StackTrace + e.TargetSite + e.Source);
						}

					}

					//                    if (result.AddTo(container) != CookieImportState.Success)
					//                        return null;

					var res = await client.GetStringAsync(url);
					if (string.IsNullOrEmpty(res))
						return null;
					var namem = Regex.Match(res, "nickname = \"([^<>]+)\";", RegexOptions.Singleline);
					if (namem.Success)
						return namem.Groups[1].Value;
					else
						return null;
				}
				catch (System.Net.Http.HttpRequestException) { return null; }
			}
		}
	}
}
