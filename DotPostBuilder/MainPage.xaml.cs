using System.Collections.ObjectModel;
using System.Security.AccessControl;
using Settings;
using Emojis;

namespace DotPostBuilder
{
    public static class DateOperations
    {
        public static DateTime GetDefaultDate()
        {
            return DateTime.Now.AddDays(1);
        }
        public static string StringifyDates(DateTime startDate, DateTime publishDate)
        {
            string publishDateLine = publishDate.Date.ToShortDateString();
            string startDateLine = startDate.Date.ToShortDateString();
            if (publishDate == GetDefaultDate()) return GetDefaultDate().ToShortDateString();
            else if (startDate == GetDefaultDate()) return publishDate.ToShortDateString();
            return startDateLine + " – " + publishDateLine;
        }
    }
    public static class ListViewOperations
    {
        public static string StringifyElements(this ObservableCollection<object> elements, string separator)
        {
            return string.Join(separator, elements);
        }
        public static string StringifyElements(this ObservableCollection<object> elements, char separator)
        {
            return elements.StringifyElements(separator.ToString());
        }
        public static bool AddIfValid(this ObservableCollection<object> elements, string item)
        {
            if (item.Trim(' ') == string.Empty) return false;

            elements.Add(item);
            return true;
        }
        public static string AsTagLine(this ObservableCollection<object> elements)
        {
            string[] hashtags = new string[elements.Count];
            for (int i = 0; i < elements.Count; i++)
                hashtags[i] = $"#{elements[i]}";
            return string.Join(' ', hashtags);
        }
        public static int IndexOf(this ObservableCollection<object> elements, ref string line)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].GetHashCode() == line.GetHashCode())
                {
                    return i;
                }
            }
            return -1;
        }
        public static int RemoveLine(this ObservableCollection<object> elements, ref string line)
        {
            int lineIndex = elements.IndexOf(ref line);
            if (lineIndex != -1)
                elements.RemoveAt(lineIndex);
            return lineIndex;
        }
    }
    class Post
    {
        public string titleEmoji;
        public string titleValue;
        public string episodeId;
        public string episodeEmoji;
        public string episodeValue;
        public string studioEmoji;
        public string studioValue;
        public string dateEmoji;
        public string dateValue;
        public string translatorEmoji;
        public string translatorValue;
        public string processorEmoji;
        public string processorValue;
        public string actorEmoji;
        public string actorValue;
        public string watchLinkEmoji;
        public string watchLinkValue;
        public string commentEmoji;
        public string commentValue;
        public string tags;
        public Post(string titleEmoji, string titleValue, string episodeId, string episodeEmoji,
            string episodeValue, string studioEmoji, string studioValue, string dateEmoji,
            string dateValue, string translatorEmoji, string translatorValue,
            string processorEmoji, string processorValue, string actorEmoji,
            string actorValue, string watchLinkEmoji, string watchLinkValue,
            string commentEmoji, string commentValue, string tags)
        {
            this.titleEmoji = titleEmoji;
            this.titleValue = titleValue;
            this.episodeId = episodeId;
            this.episodeEmoji = episodeEmoji;
            this.episodeValue = episodeValue;
            this.studioEmoji = studioEmoji;
            this.studioValue = studioValue;
            this.dateEmoji = dateEmoji;
            this.dateValue = dateValue;
            this.translatorEmoji = translatorEmoji;
            this.translatorValue = translatorValue;
            this.processorEmoji = processorEmoji;
            this.processorValue = processorValue;
            this.actorEmoji = actorEmoji;
            this.actorValue = actorValue;
            this.watchLinkEmoji = watchLinkEmoji;
            this.watchLinkValue = watchLinkValue;
            this.commentEmoji = commentEmoji;
            this.commentValue = commentValue;
            this.tags = tags;
        }
        public override string ToString()
        {
            return $"{titleEmoji} {titleValue}\n{episodeEmoji} EP{episodeId}: {episodeValue}\n\n{studioEmoji} {studioValue}\n{dateEmoji} {dateValue}\n{translatorEmoji} Переклад: {translatorValue}\n{processorEmoji} Обробка: {processorValue}\n{actorEmoji} Озвучення: {actorValue}\n{watchLinkEmoji} {watchLinkValue}\n\n{commentEmoji} {commentValue}\n\n{tags}";
        }
    }
    public partial class MainPage : ContentPage
    {
        int count = 0;
        /// As we work with list-views, it is simplier to compare objects to objects
        public ObservableCollection<object> titles = new ObservableCollection<object>();
        public ObservableCollection<object> episodeNames = new ObservableCollection<object>();
        public ObservableCollection<object> studios = new ObservableCollection<object>();
        public ObservableCollection<object> translators = new ObservableCollection<object>();
        public ObservableCollection<object> processors = new ObservableCollection<object>();
        public ObservableCollection<object> actors = new ObservableCollection<object>();
        public ObservableCollection<object> watchLinks = new ObservableCollection<object>();
        public ObservableCollection<object> tags = new ObservableCollection<object>();
        public MainPage()
        {
            InitializeComponent();

            lvTitles.ItemsSource = titles;
            lvEpisodeNames.ItemsSource = episodeNames;
            lvStudios.ItemsSource = studios;
            lvProcessors.ItemsSource = processors;
            lvTranslators.ItemsSource = translators;
            lvActors.ItemsSource = actors;
            lvWatchLinks.ItemsSource = watchLinks;
            lvTags.ItemsSource = tags;

            tbNextTitle.Text = string.Empty;
            tbNextEpsiodeName.Text = string.Empty;
            tbNextStudio.Text = string.Empty;
            tbNextTranslator.Text = string.Empty;
            tbNextProcessor.Text = string.Empty;
            tbNextActor.Text = string.Empty;
            tbNextWatchLink.Text = string.Empty;
            tbComment.Text = string.Empty;

            dpPublishDate.Date = DateOperations.GetDefaultDate();
            dpStartDate.Date = DateOperations.GetDefaultDate();

            SaveDefaultPostEmojis();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";
        }

        private void btnAddActor_Clicked(object sender, EventArgs e)
        {
            actors.AddIfValid(tbNextActor.Text);
            tbNextActor.Text = string.Empty;
        }

        private void btnRemoveActor_Clicked(object sender, EventArgs e)
        {
            if (!actors.Remove(lvActors.SelectedItem))
                DisplayAlert("Unavailable", "Actor not selected", "OK");
        }
        string GetCompoundTitle()
        {
            return titles.StringifyElements(" | ");
        }
        string GetEpisodeNames()
        {
            return episodeNames.StringifyElements(" | ");
        }
        string GetStudioNames()
        {
            return studios.StringifyElements(" & ");
        }
        string GetTranslatorNames()
        {
            return translators.StringifyElements(", ");
        }
        string GetProcessorNames()
        {
            return processors.StringifyElements(", ");
        }
        string GetActorNames()
        {
            return actors.StringifyElements(", ");
        }
        string GetWatchLinks()
        {
            return watchLinks.StringifyElements(", ");
        }
        string GetComment()
        {
            return tbComment.Text.Trim(' ');
        }

        private void lvActors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnAddWatchLink_Clicked(object sender, EventArgs e)
        {
            watchLinks.AddIfValid(tbNextWatchLink.Text);

            tbNextWatchLink.Text = string.Empty;
        }

        private void btnRemoveWatchLink_Clicked(object sender, EventArgs e)
        {
            if (watchLinks.Remove(lvWatchLinks.SelectedItem))
                DisplayAlert("Alarm", "Watch link not selected", "Got it");
        }

        private void lvWatchLinks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnAddProcessor_Clicked(object sender, EventArgs e)
        {
            processors.AddIfValid(tbNextProcessor.Text);

            tbNextProcessor.Text = string.Empty;
        }

        private void lvProcessors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveProcessor_Clicked(object sender, EventArgs e)
        {
            if (processors.Remove(lvProcessors.SelectedItem))
                DisplayAlert("Alarm", "Processor not selected", "Got it");
        }

        private void btnAddTranslator_Clicked(object sender, EventArgs e)
        {
            translators.AddIfValid(tbNextTranslator.Text);

            tbNextTranslator.Text = string.Empty;
        }

        private void lvTranslators_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveTranslator_Clicked(object sender, EventArgs e)
        {
            if (translators.Remove(lvTranslators.SelectedItem))
                DisplayAlert("Alarm", "Translator not selected", "Got it");
        }

        private void btnAddStudio_Clicked(object sender, EventArgs e)
        {
            studios.AddIfValid(tbNextStudio.Text);

            tbNextStudio.Text = string.Empty;
        }

        private void lvStudios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveStudio_Clicked(object sender, EventArgs e)
        {
            studios.Remove(lvStudios.SelectedItem);
        }

        private void btnAddTitle_Clicked(object sender, EventArgs e)
        {
            titles.AddIfValid(tbNextTitle.Text);

            tbNextTitle.Text = string.Empty;
        }

        private void lvTitle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveTitle_Clicked(object sender, EventArgs e)
        {
            if (!titles.Remove(lvTitles.SelectedItem))
                DisplayAlert("Alarm", "Title not selected", "Got it");
        }

        private void btnAddEpisodeName_Clicked(object sender, EventArgs e)
        {
            episodeNames.AddIfValid(tbNextEpsiodeName.Text);

            tbNextEpsiodeName.Text = string.Empty;
        }

        private void lvEpisodeNames_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveEpisodeName_Clicked(object sender, EventArgs e)
        {
            episodeNames.Remove(lvEpisodeNames.SelectedItem);
        }
        int GetEpisodeNumber()
        {
            int episode;
            if (int.TryParse(tbEpisodeNumber.Text, out episode))
                return episode;
            return 0;
        }
        string GetFormatedEpisode()
        {
            int episode = GetEpisodeNumber();
            string formated = episode < 10 ? string.Format("0{0}", episode) : episode.ToString();
            return formated;
        }
        void WriteEpisode(int episode)
        {
            tbEpisodeNumber.Text = episode.ToString();
        }
        private void btnIncrementEpisode_Clicked(object sender, EventArgs e)
        {
            WriteEpisode(GetEpisodeNumber() + 1);
        }

        private void btnDecrementEpisode_Clicked(object sender, EventArgs e)
        {
            int episode = GetEpisodeNumber();
            if (episode > 0)
                WriteEpisode(episode - 1);
            else
                WriteEpisode(episode);
        }

        private void tbEpisodeNumber_Unfocused(object sender, FocusEventArgs e)
        {
            tbEpisodeNumber.Text = 1.ToString();
        }

        private void btnAddTag_Clicked(object sender, EventArgs e)
        {
            tags.AddIfValid(tbNextTag.Text);

            tbNextTag.Text = string.Empty;
        }

        private void lvTags_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnRemoveTag_Clicked(object sender, EventArgs e)
        {
            if (!tags.Remove(lvTags.SelectedItem))
                DisplayAlert("Alarm", "Tag not selected", "Got it");
        }

        private void tbTitleEmoji_Unfocused(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    //https://youtu.be/9purmgQuIqs
            //    string settingName = Settings.Settings.SettingDictionary.TitleEmojiSetting.ToString();
            //    if (!Settings.Settings.Exists(FileSystem.CacheDirectory, settingName)) Settings.Settings.AddSetting(settingName, tbTitleEmoji.Text);
            //    else Settings.Settings.EditSettings(FileSystem.CacheDirectory, settingName, tbTitleEmoji.Text);
            //}
            //catch (Exception ex) { DisplayAlert("Alert", ex.Message, "Ok"); }
        }
        void SaveDefaultPostEmojis()
        {
            string mainDir = FileSystem.Current.AppDataDirectory;
            string postPath = PostDataSettings.GetStoredPostPath(mainDir);

            string? dir = Path.GetDirectoryName(postPath);
            string filename = Path.GetFileName(postPath);
            if (string.IsNullOrEmpty(dir) || string.IsNullOrEmpty(filename))
            {
                filename = postPath;
                dir = mainDir;
            }

            try
            {
                ////Minimal hierarchy tesing code
                //List<KeyValuePair<Settings.Settings.SettingDictionary, string>> settingLines = Settings.Settings.ReadSettings(mainDir);
                //settingLines.Add(new KeyValuePair<Settings.Settings.SettingDictionary, string>(Settings.Settings.SettingDictionary.TitleEmojiSetting, "🎬"));
                //Settings.Settings.WriteSettings(mainDir, settingLines);
                PostDataSettings.SetTitleEmoji(dir, filename, "🎬");
                PostDataSettings.SetEpisodeEmoji(dir, filename, "📼");
                PostDataSettings.SetStudioEmoji(dir, filename, "📡");
                PostDataSettings.SetDateEmoji(dir, filename, "📅");
                PostDataSettings.SetTranslatorEmoji(mainDir, filename, "📄");
                PostDataSettings.SetProcessorEmoji(mainDir, filename, "🎧");
                PostDataSettings.SetActorEmoji(mainDir, filename, "🎙");
                PostDataSettings.SetWatchLinkEmoji(mainDir, filename, "📺");
                PostDataSettings.SetCommentEmoji(mainDir, filename, "💬");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error on setting writing", ex.Message, "OK");
            }
        }
        void RestorePostEmojis()
        {
            string mainDir = FileSystem.Current.AppDataDirectory;

            string postPath = PostDataSettings.GetStoredPostPath(mainDir);

            string dir = Path.GetDirectoryName(postPath).ToString();
            string filename = Path.GetFileName(postPath);

            DisplayAlert("Stored path", Path.Combine(dir, filename), "ok");
            try
            {
                tbTitleEmoji.Text = PostDataSettings.GetTitleEmoji(dir, filename);
                tbEpisodeNameEmoji.Text = PostDataSettings.GetEpisodeEmoji(dir, filename);
                tbStudioEmoji.Text = PostDataSettings.GetStudioEmoji(dir, filename);
                tbDateEmoji.Text = PostDataSettings.GetDateEmoji(dir, filename);
                tbTranslatorEmoji.Text = PostDataSettings.GetTranslatorEmoji(dir, filename);
                tbProcessorEmoji.Text = PostDataSettings.GetProcessorEmoji(dir, filename);
                tbActorEmoji.Text = PostDataSettings.GetActorEmoji(dir, filename);
                tbWatchEmoji.Text = PostDataSettings.GetWatchLinkEmoji(dir, filename);
                tbCommentEmoji.Text = PostDataSettings.GetCommentEmoji(dir, filename);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error on setting reading", ex.Message, "OK");
            }
        }
        private async void btnChooseTemplate_Clicked(object sender, EventArgs e)
        {

            // SEPARATE LOAD FUNCTION AND USE ON CHANGE

            string mainDir = FileSystem.Current.AppDataDirectory;
            string settingsfn = "settings.txt";
            PickOptions options = new PickOptions();
            var result = await FilePicker.PickAsync(options);
            if (result != null)
            {
                await DisplayAlert("You picked a file", result.FullPath, "Ok");
                AppSettings.SetPostPath(mainDir, settingsfn, result.FullPath);
                RestorePostEmojis();
            }
        }
        private void btnGeneratePost_Clicked(object sender, EventArgs e)
        {
            Post post = new Post(titleEmoji: tbTitleEmoji.Text, titleValue: GetCompoundTitle(), episodeId: GetFormatedEpisode(), episodeEmoji: tbEpisodeNameEmoji.Text, episodeValue: GetEpisodeNames(), studioEmoji: tbStudioEmoji.Text, studioValue: GetStudioNames(), dateEmoji: tbDateEmoji.Text, dateValue: DateOperations.StringifyDates(dpStartDate.Date, dpPublishDate.Date), translatorEmoji: tbTranslatorEmoji.Text, translatorValue: GetTranslatorNames(), processorEmoji: tbProcessorEmoji.Text, processorValue: GetProcessorNames(), actorEmoji: tbActorEmoji.Text, actorValue: GetActorNames(), watchLinkEmoji: tbWatchEmoji.Text, watchLinkValue: GetWatchLinks(), commentEmoji: tbCommentEmoji.Text, commentValue: GetComment(), tags.AsTagLine());
            string postText = post.ToString();
            tbPostText.Text = postText;

            RestorePostEmojis();
            //SaveDefaultPostEmojis();

            //if (!File.Exists(sourceFile))
            //    File.Create(sourceFile).Close();
        }

    }
}
