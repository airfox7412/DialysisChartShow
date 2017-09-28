using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //原發病診斷
        protected void reload_page1(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_01.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //病理診斷
        protected void reload_page2(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_02.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //併發症診斷
        protected void reload_page3(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_03.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //傳染病診斷
        protected void reload_page4(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_04.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //腫瘤診斷
        protected void reload_page5(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_05.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //過敏診斷
        protected void reload_page6(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_06.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //轉歸情況
        protected void reload_page7(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_01_07.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //新診斷信息
        protected void reload_page8(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_081.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }
    
    }
}