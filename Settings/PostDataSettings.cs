using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    public static class PostDataSettings
    {
        public enum PostEmojis
        {
            None = 0,
            TitleEmojiSetting,
            EpisodeEmojiSetting,
            StudioEmojiSetting,
            DateEmojiSetting,
            TranslaterEmojiSetting,
            ProcessorEmojiSetting,
            DubbingEmojiSetting,
            WatchLinkEmojiSetting,
            CommentEmojiSetting
        }

        public static string GetValueByKey(string appDataFolder, string postFileName, PostEmojis emojiID)
        {
            return BaseSettings.GetValueByKey(appDataFolder, postFileName, emojiID.ToString());
        }
        public static bool Exists(string appDataFolder, string postFileName, PostEmojis emojiID)
        {
            return BaseSettings.Exists(appDataFolder, postFileName, emojiID.ToString());
        }
        public static void AddSetting(string appDataFolder, string postFileName, PostEmojis emojiID, string settingValue)
        {
            BaseSettings.AddSetting(appDataFolder, postFileName, emojiID.ToString(), settingValue);
        }
        public static bool EditSetting(string appDataFolder, string postFileName, PostEmojis emojiID, string settingValue)
        {
            return BaseSettings.EditSetting(appDataFolder, postFileName, emojiID.ToString(), settingValue);
        }
        #region Custom simplication methods
        #region GET
        public static string GetTitleEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.TitleEmojiSetting);
        }
        public static string GetEpisodeEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.EpisodeEmojiSetting);
        }
        public static string GetStudioEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.StudioEmojiSetting);
        }
        public static string GetDateEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.DateEmojiSetting);
        }
        public static string GetTranslatorEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.TranslaterEmojiSetting);
        }
        public static string GetProcessorEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.ProcessorEmojiSetting);
        }
        public static string GetActorEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.DubbingEmojiSetting);
        }
        public static string GetWatchLinkEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.WatchLinkEmojiSetting);
        }
        public static string GetCommentEmoji(string appDataFolder, string postFileName)
        {
            return GetValueByKey(appDataFolder, postFileName, PostEmojis.CommentEmojiSetting);
        }
        #endregion
        #region SET
        public static void SetTitleEmoji(string appDataFolder, string postFileName, string titleEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.TitleEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.TitleEmojiSetting, titleEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.TitleEmojiSetting, titleEmoji);
        }
        public static void SetEpisodeEmoji(string appDataFolder, string postFileName, string episodeEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.EpisodeEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.EpisodeEmojiSetting, episodeEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.EpisodeEmojiSetting, episodeEmoji);
        }
        public static void SetStudioEmoji(string appDataFolder, string postFileName, string studioEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.StudioEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.StudioEmojiSetting, studioEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.StudioEmojiSetting, studioEmoji);
        }
        public static void SetDateEmoji(string appDataFolder, string postFileName, string dateEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.DateEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.DateEmojiSetting, dateEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.DateEmojiSetting, dateEmoji);
        }
        public static void SetTranslatorEmoji(string appDataFolder, string postFileName, string translatorEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.TranslaterEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.TranslaterEmojiSetting, translatorEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.TranslaterEmojiSetting, translatorEmoji);
        }
        public static void SetProcessorEmoji(string appDataFolder, string postFileName, string processorEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.ProcessorEmojiSetting))
                AddSetting(appDataFolder, postFileName,PostEmojis.ProcessorEmojiSetting, processorEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.ProcessorEmojiSetting, processorEmoji);

        }
        public static void SetActorEmoji(string appDataFolder, string postFileName, string actorEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.DubbingEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.DubbingEmojiSetting, actorEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.DubbingEmojiSetting, actorEmoji);
        }
        public static void SetWatchLinkEmoji(string appDataFolder, string postFileName, string watchLinkEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.WatchLinkEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.WatchLinkEmojiSetting, watchLinkEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.WatchLinkEmojiSetting, watchLinkEmoji);
        }
        public static void SetCommentEmoji(string appDataFolder, string postFileName, string commentEmoji)
        {
            if (!Exists(appDataFolder, postFileName, PostEmojis.CommentEmojiSetting))
                AddSetting(appDataFolder, postFileName, PostEmojis.CommentEmojiSetting, commentEmoji);
            else
                EditSetting(appDataFolder, postFileName, PostEmojis.CommentEmojiSetting, commentEmoji);
        }
        #endregion
        #endregion
    }
}
