using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;

namespace Dialysis_Chart_Show
{
    public class Message
    {
        //可以根據需求來決定，要不要區分大小寫
        private ResourceManager messageResourceManager = new ResourceManager("Dialysis_Chart_Show.Properties.MessageFile", Assembly.GetExecutingAssembly());

        public Message()
        {
            //可以根據情況來決定，是否要強制給一個預設的語系
        }

        public Message(Language language)
        {
            this.Format = language;
        }

        public enum Language
        {
            None = 0,
            [Description("zh-TW")]
            zh_tw,
            [Description("zh-CN")]
            zh_cn,
            [Description("en-US")]
            en_us
        }
        private Language Format { get; set; }
        public string GetMessage(string name)
        {
            //可以根據情況來決定，是否要用try/catch來處理，當name不存在於resource檔時，是否要攔錯，回傳空字串或null
            var cultureInfo = this.Format == Language.None ? System.Threading.Thread.CurrentThread.CurrentCulture : new CultureInfo(this.Format.GetDescription());
            var result = this.messageResourceManager.GetString(name, cultureInfo);
            return result;
        }

    }
}
