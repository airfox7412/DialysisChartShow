using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 由於 EXT.NET 對於 Visual Studio 來說, 算是外掛; 無法直接新增一個 EXT.NET 頁面
            // 給 忘了如何 新增一個EXT.NET頁面 參考用; 在這裡碰到 不同人 不同時間 問同樣的問題.
            // EXT.NET 從 Visual Studio 加入一個新網頁 SOP
            // 加入 -> 新增項目 -> Web Form -> WebForm1.aspx
            // 工具箱 -> 選擇 EXT NET  ResourceManager (拖曳到 設計 <div></div> 中)     
            // 工具箱 -> 選擇 EXT NET  GridPanel  (拖曳到 設計 ResourceManager 底下)
            // 全部儲存 -> 關閉整個專案 -> 開啟專案 -> 重建專案
            // 就有了這個不會出現錯誤訊息 for EXT.NET 執行階段版本 v2.0.50727 WebForm1.aspx 範例

        }



    }



}