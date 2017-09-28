using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_0h : BaseForm  //System.Web.UI.Page
    {
        private Ext.Net.Node root = new Ext.Net.Node();
        private Ext.Net.Node rA = new Ext.Net.Node();
        private Ext.Net.Node rB = new Ext.Net.Node();
        private Ext.Net.Node rC = new Ext.Net.Node();
        private Ext.Net.Node rD = new Ext.Net.Node();
        private Ext.Net.Node rE = new Ext.Net.Node();
        private Ext.Net.Node rF = new Ext.Net.Node();
        private Ext.Net.Node rG = new Ext.Net.Node();
        private Ext.Net.Node rH = new Ext.Net.Node();
        private Ext.Net.Node rA0 = new Ext.Net.Node();
        private Ext.Net.Node rB0 = new Ext.Net.Node();
        private Ext.Net.Node rC0 = new Ext.Net.Node();
        private Ext.Net.Node rD0 = new Ext.Net.Node();
        private Ext.Net.Node rE0 = new Ext.Net.Node();
        private Ext.Net.Node rF0 = new Ext.Net.Node();
        //private Ext.Net.Node rG0 = new Ext.Net.Node();
        private StatusBar statusBar1 = new StatusBar();
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.txtNODE_ID.Hidden = true;
                this.txtNODE_TEXT.Hidden = true;
                //Ext.Net.TreePanel tree = new Ext.Net.TreePanel();

                this.TreePanel1.ID = "TreePanel1";
                //this.TreePanel1.Width = Unit.Pixel(200);
                this.TreePanel1.Height = Unit.Pixel(400);
                this.TreePanel1.AutoScroll = true;
                //tree.Icon = Icon.BookOpen;
                //tree.Title = "Catalog";
                //tree.AutoScroll= true;
                //tree.Collapsible = true;
                //tree.Region = Region.West;  
                //tree.Split = true;
                //tree.RootVisible = false;

                Ext.Net.Button btnExpand = new Ext.Net.Button();
                btnExpand.Text = "展开";
                btnExpand.Listeners.Click.Handler = this.TreePanel1.ClientID + ".expandAll();";

                Ext.Net.Button btnCollapse = new Ext.Net.Button();
                btnCollapse.Text = "收合";
                btnCollapse.Listeners.Click.Handler = this.TreePanel1.ClientID + ".collapseAll();";

                Ext.Net.Button btnCount = new Ext.Net.Button();
                btnCount.Text = "前10";
                btnCount.Click += btnCount_Click;
                btnCount.AutoPostBack = true;

                Ext.Net.Button btnPrint = new Ext.Net.Button();
                btnPrint.Text = "打印";
                btnPrint.Icon = Icon.Printer;   
                btnPrint.Click += btnPrint_Click;
                //btnPrint.AutoPostBack = true;

                Ext.Net.Button btnDelete = new Ext.Net.Button() 
                { 
                    Text = "删除",
                    Icon = Icon.NoteDelete,  
                    AutoPostBack = true
                };
                //btnDelete.Text = "删除";
                //btnDelete.Icon = Icon.NoteDelete;  
                btnDelete.Click += btnDelete_Click;
                //btnDelete.AutoPostBack = true;
                
                Toolbar toolBar = new Toolbar();
                toolBar.ID = "Toolbar1";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                toolBar.Items.Add(btnCount);
                toolBar.Items.Add(btnPrint);
                toolBar.Items.Add(btnDelete);
                this.TreePanel1.TopBar.Add(toolBar);

                //StatusBar statusBar1 = new StatusBar();
                statusBar1.ID = "StatusBar1";
                statusBar1.AutoClear = 1000;
                this.TreePanel1.BottomBar.Add(statusBar1);


                this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemExpand.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + item.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemExpand.Buffer = 30;
                //tree.Listeners.ItemCollapse.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + item.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemCollapse.Buffer = 30;


                //Ext.Net.Node root = new Ext.Net.Node()
                //{
                //    Text = "血透评估表",
                //    NodeID = "__"
                //};
                //Ext.Net.Node rA = new Ext.Net.Node()
                //{
                //    Text = "1首次血液透析护理评估措施记录单",
                //    //Icon = "",
                //    NodeID = "A_"
                //};
                //Ext.Net.Node rB = new Ext.Net.Node()
                //{
                //    Text = "2血管通路动静脉内瘘物理检查评估表",
                //    //Icon = "",
                //    NodeID = "B_"
                //};
                //Ext.Net.Node rC = new Ext.Net.Node()
                //{
                //    Text = "3动静脉内瘘闭塞高危因素评估表",
                //    //Icon = "",
                //    NodeID = "C_"
                //};
                //Ext.Net.Node rD = new Ext.Net.Node()
                //{
                //    Text = "4血液透析患者皮肤瘙痒评估表(Sergio)",
                //    //Icon = "",
                //    NodeID = "D_"
                //};

                root.Text = "血透评估表";
                root.Icon = Icon.ReportUser; 
                root.NodeID = "__";
                rA.Text = "1首次血液透析护理评估措施记录单";
                rA.NodeID = "A_";
                rB.Text = "2血管通路动静脉内瘘物理检查评估表";
                rB.NodeID = "B_";
                rC.Text = "3动静脉内瘘闭塞高危因素评估表";
                rC.NodeID = "C_";
                rD.Text = "4血液透析患者皮肤瘙痒评估表(Sergio)";
                rD.NodeID = "D_";
                rE.Text = "5疼痛评分表";
                rE.NodeID = "E_";
                rF.Text = "6住院病人预防跌倒护理评估表";
                rF.NodeID = "F_";
                rG.Text = "7预防跌倒护理措施评估表";
                rG.NodeID = "G_";
                //rH.Text = "8血液透析患者评估表";
                //rH.NodeID = "H_";

                root.Children.Add(rA);
                root.Children.Add(rB);
                root.Children.Add(rC);
                root.Children.Add(rD);
                root.Children.Add(rE);
                root.Children.Add(rF);
                root.Children.Add(rG);
                //root.Children.Add(rH);

                Ext.Net.Node rA0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "A0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rA.Children.Add(rA0);
                Ext.Net.Node rB0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "B0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rB.Children.Add(rB0);
                Ext.Net.Node rC0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "C0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rC.Children.Add(rC0);
                Ext.Net.Node rD0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "D0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rD.Children.Add(rD0);
                Ext.Net.Node rE0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "E0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rE.Children.Add(rE0);
                Ext.Net.Node rF0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "F0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rF.Children.Add(rF0);
                Ext.Net.Node rG0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "G0",
                    Icon = Icon.NoteAdd,
                    Leaf = true
                };
                rG.Children.Add(rG0);
                //Ext.Net.Node rH0 = new Ext.Net.Node()
                //{
                //    Text = "添加",
                //    NodeID = "H0",
                //    Icon = Icon.NoteAdd,
                //    Leaf = true
                //};
                //rH.Children.Add(rH0);

                DataTable dt = new DataTable();
                //string sPAT_ID = _PAT_ID;// Request.QueryString["_PAT_ID"];

                string sql = "SELECT 'A' as type, pat_id, info_date FROM zinfo_h_01 " +
                              "WHERE pat_id=" + _PAT_ID + " " +
                              "UNION ALL " +
                             "SELECT 'B' as type, pat_id, info_date FROM zinfo_h_02 " +
                              "WHERE pat_id=" + _PAT_ID + " " +
                              "UNION ALL " +
                             "SELECT 'C' as type, pat_id, info_date FROM zinfo_h_03 " +
                              "WHERE pat_id=" + _PAT_ID + " " +
                              "UNION ALL " +
                             "SELECT 'D' as type, pat_id, info_date FROM zinfo_h_04 " +
                              "WHERE pat_id=" + _PAT_ID + " " +
                              "ORDER BY type, info_date DESC ";

                dt = db.Query(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Ext.Net.Node rr = new Ext.Net.Node()
                    {
                        Text = dt.Rows[i]["info_date"].ToString(),
                        NodeID = dt.Rows[i]["type"].ToString() + (i+1).ToString(),
                        Icon = Icon.Note,
                        Leaf = true
                    };

                    switch (dt.Rows[i]["type"].ToString())
                    {
                        case "A":
                            rA.Children.Add(rr);
                            break;
                        case "B":
                            rB.Children.Add(rr);
                            break;
                        case "C":
                            rC.Children.Add(rr);
                            break;
                        case "D":
                            rD.Children.Add(rr);
                            break;
                        case "E":
                            rE.Children.Add(rr);
                            break;
                        case "F":
                            rF.Children.Add(rr);
                            break;
                        case "G":
                            rG.Children.Add(rr);
                            break;
                        //case "H":
                        //    rH.Children.Add(rr);
                        //    break;
                    }
                }


                if (rA.Children.Count > 1)
                    rA.Children.Remove(rA0);  
                rA.Expanded = true;
                rB.Expanded = true;
                rC.Expanded = true;
                rD.Expanded = true;
                root.Expanded = true;
                this.TreePanel1.Root.Add(root);
            }


            //foreach (Composer composer in composers)
            //{
            //    Ext.Net.Node composerNode = new Ext.Net.Node()
            //    {
            //        Text = composer.Name,
            //        Icon = Icon.UserGray
            //    };
            //    root.Children.Add(composerNode);

            //    foreach (Composition composition in composer.Compositions)
            //    {
            //        Ext.Net.Node compositionNode = new Ext.Net.Node()
            //        {
            //            Text = composition.Type.ToString()
            //        };
            //        composerNode.Children.Add(compositionNode);

            //        foreach (Piece piece in composition.Pieces)
            //        {
            //            Ext.Net.Node pieceNode = new Ext.Net.Node()
            //            {
            //                Text = piece.Title,
            //                Icon = Icon.Music,
            //                Leaf = true
            //            };
            //            compositionNode.Children.Add(pieceNode);
            //        }
            //    }
            //}
        }



        #region Sample

        public class Composer
        {
            public Composer(string name) { this.Name = name; }
            public string Name { get; set; }

            private List<Composition> compositions;
            public List<Composition> Compositions
            {
                get
                {
                    if (this.compositions == null)
                    {
                        this.compositions = new List<Composition>();
                    }
                    return this.compositions;
                }
            }
        }

        public class Composition
        {
            public Composition() { }

            public Composition(CompositionType type)
            {
                this.Type = type;
            }

            public CompositionType Type { get; set; }

            private List<Piece> pieces;
            public List<Piece> Pieces
            {
                get
                {
                    if (this.pieces == null)
                    {
                        this.pieces = new List<Piece>();
                    }
                    return this.pieces;
                }
            }
        }

        public class Piece
        {
            public Piece() { }

            public Piece(string title)
            {
                this.Title = title;
            }
            public string Title { get; set; }
        }

        public enum CompositionType
        {
            Concertos,
            Quartets,
            Sonatas,
            Symphonies
        }

        public List<Composer> GetData()
        {
            //Composer beethoven = new Composer("1首次血液透析护理评估措施记录单");
            Composer beethoven = new Composer("Beethoven");
            Composition beethovenConcertos = new Composition(CompositionType.Concertos);
            Composition beethovenQuartets = new Composition(CompositionType.Quartets);
            Composition beethovenSonatas = new Composition(CompositionType.Sonatas);
            Composition beethovenSymphonies = new Composition(CompositionType.Symphonies);
            beethovenConcertos.Pieces.AddRange(new List<Piece> { 
            new Piece{ Title = "No. 1 - C" },
            new Piece{ Title = "No. 2 - B-Flat Major" },
            new Piece{ Title = "No. 3 - C Minor" },
            new Piece{ Title = "No. 4 - G Major" },
            new Piece{ Title = "No. 5 - E-Flat Major" }
        });
            beethovenQuartets.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Six String Quartets" },
            new Piece{ Title = "Three String Quartets" },
            new Piece{ Title = "Grosse Fugue for String Quartets" }
        });
            beethovenSonatas.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Sonata in A Minor" },
            new Piece{ Title = "sonata in F Major" }
        });
            beethovenSymphonies.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "No. 1 - C Major" },
            new Piece{ Title = "No. 2 - D Major" },
            new Piece{ Title = "No. 3 - E-Flat Major" },
            new Piece{ Title = "No. 4 - B-Flat Major" },
            new Piece{ Title = "No. 5 - C Minor" },
            new Piece{ Title = "No. 6 - F Major" },
            new Piece{ Title = "No. 7 - A Major" },
            new Piece{ Title = "No. 8 - F Major" },
            new Piece{ Title = "No. 9 - D Minor" }
        });
            beethoven.Compositions.AddRange(new List<Composition>{
            beethovenConcertos, 
            beethovenQuartets,
            beethovenSonatas,
            beethovenSymphonies 
        });

            //Composer brahms = new Composer("2血管通路动静脉内瘘物理检查评估表");
            Composer brahms = new Composer("Brahms");
            Composition brahmsConcertos = new Composition(CompositionType.Concertos);
            Composition brahmsQuartets = new Composition(CompositionType.Quartets);
            Composition brahmsSonatas = new Composition(CompositionType.Sonatas);
            Composition brahmsSymphonies = new Composition(CompositionType.Symphonies);
            brahmsConcertos.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Violin Concerto" },
            new Piece{ Title = "Double Concerto - A Minor" },
            new Piece{ Title = "Piano Concerto No. 1 - D Minor" },
            new Piece{ Title = "Piano Concerto No. 2 - B-Flat Major" }
        });
            brahmsQuartets.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Piano Quartet No. 1 - G Minor" },
            new Piece{ Title = "Piano Quartet No. 2 - A Major" },
            new Piece{ Title = "Piano Quartet No. 3 - C Minor" },
            new Piece{ Title = "Piano Quartet No. 3 - B-Flat Minor" }
        });

            brahmsSonatas.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Two Sonatas for Clarinet - F Minor" },
            new Piece{ Title = "Two Sonatas for Clarinet - E-Flat Major" }
        });
            brahmsSymphonies.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "No. 1 - C Minor" },
            new Piece{ Title = "No. 2 - D Minor" },
            new Piece{ Title = "No. 3 - F Major" },
            new Piece{ Title = "No. 4 - E Minor" }
        });
            brahms.Compositions.AddRange(new List<Composition>{
            brahmsConcertos, 
            brahmsQuartets,
            brahmsSonatas,
            brahmsSymphonies 
        });

            //Composer mozart = new Composer("3动静脉内瘘闭塞高危因素评估表");
            Composer mozart = new Composer("Mozart");
            Composition mozartConcertos = new Composition(CompositionType.Concertos);
            mozartConcertos.Pieces.AddRange(new List<Piece> {
            new Piece{ Title = "Piano Concerto No. 12" },
            new Piece{ Title = "Piano Concerto No. 17" },
            new Piece{ Title = "Clarinet Concerto" },
            new Piece{ Title = "Violin Concerto No. 5" },
            new Piece{ Title = "Violin Concerto No. 4" }
        });
            mozart.Compositions.Add(mozartConcertos);
            return new List<Composer> { beethoven, brahms, mozart };
        }
        #endregion

        protected void reload_page1(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_01.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page2(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_02.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }
        
        protected void reload_page3(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_03.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page4(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_04.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page5(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_05.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page6(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_06.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page7(object sender, EventArgs e)
        {
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_0h_07.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        //protected void reload_page8(object sender, EventArgs e)
        //{
        //    this.Panel1.Loader.SuspendScripting();
        //    this.Panel1.Loader.Url = "./Dialysis_0h_08.aspx";
        //    this.Panel1.Loader.DisableCaching = true;
        //    this.Panel1.LoadContent();
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Response.Write(sID + " , " + sTEXT);
            //Response.Write("<script>alert('PRN " + sID + " , " + sTEXT + "');</script>");
            statusBar1.Text = "123"; //.SetStatus("{text: ': <b>' + '123' + '</b>', clear: false}");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Response.Write(sID + " , " + sTEXT);
            //Response.Write("<script>alert('DEL " + sID + " , " + sTEXT + "');</script>");
        }

        protected void btnCount_Click(object sender, EventArgs e)
        {
            Ext.Net.Button bb = (Ext.Net.Button)sender;

            foreach (Node nn in root.Children)
                for (int i = nn.Children.Count - 1; i >= 0; i--)
                    if (nn.Children[i].Text != "添加")
                        nn.Children.RemoveAt(i);
            //for (int i = rB.Children.Count - 1; i >= 0; i--)
            //    if (rB.Children[i].Text != "添加")
            //        rB.Children.RemoveAt(i);
            //for (int i = rC.Children.Count - 1; i >= 0; i--)
            //    if (rC.Children[i].Text != "添加")
            //        rC.Children.RemoveAt(i);
            //for (int i = rD.Children.Count - 1; i >= 0; i--)
            //    if (rD.Children[i].Text != "添加")
            //        rD.Children.RemoveAt(i);

            DataTable dt = new DataTable();
            string sql;

            if (bb.Text == "前10")
            {
                bb.Text = "全部";
                sql = "SELECT 'A' as type, pat_id, info_date " +
                        "FROM zinfo_h_01 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "UNION ALL " +
                      "SELECT 'B' as type, pat_id, info_date " +
                        "FROM zinfo_h_02 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "UNION ALL " +
                      "SELECT 'C' as type, pat_id, info_date " +
                        "FROM zinfo_h_03 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "UNION ALL " +
                      "SELECT 'D' as type, pat_id, info_date " +
                        "FROM zinfo_h_04 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "ORDER BY type, info_date DESC ";
                dt = db.Query(sql);
            }
            else
            {
                bb.Text = "前10";
                sql = "SELECT 'A' as type, pat_id, info_date " +
                        "FROM zinfo_h_01 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "ORDER BY info_date DESC " +
                       "LIMIT 0,1 ";
                dt = db.Query2(sql,dt);
                sql = "SELECT 'B' as type, pat_id, info_date " +
                        "FROM zinfo_h_02 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "ORDER BY info_date DESC " +
                       "LIMIT 0,1 ";
                dt = db.Query2(sql, dt);
                sql = "SELECT 'C' as type, pat_id, info_date " +
                        "FROM zinfo_h_03 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "ORDER BY info_date DESC " +
                       "LIMIT 0,1 ";
                dt = db.Query2(sql, dt);
                sql = "SELECT 'D' as type, pat_id, info_date " +
                        "FROM zinfo_h_04 " +
                       "WHERE pat_id=" + _PAT_ID + " " +
                       "ORDER BY info_date DESC " +
                       "LIMIT 0,1 ";
                dt = db.Query2(sql, dt);
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Ext.Net.Node rr = new Ext.Net.Node()
                {
                    Text = dt.Rows[i]["info_date"].ToString(),
                    NodeID = dt.Rows[i]["type"].ToString() + (i + 1).ToString(),
                    Icon = Icon.Note,
                    Leaf = true
                };

                switch (dt.Rows[i]["type"].ToString())
                {
                    case "A":
                        rA.Children.Add(rr);
                        break;
                    case "B":
                        rB.Children.Add(rr);
                        break;
                    case "C":
                        rC.Children.Add(rr);
                        break;
                    case "D":
                        rD.Children.Add(rr);
                        break;
                    case "E":
                        rE.Children.Add(rr);
                        break;
                    case "F":
                        rF.Children.Add(rr);
                        break;
                    case "G":
                        rG.Children.Add(rr);
                        break;
                    //case "H":
                    //    rH.Children.Add(rr);
                    //    break;
                }
            }

        }

        protected void Node_Click(object sender, DirectEventArgs e)
        {
            this.txtNODE_ID.Text = e.ExtraParams["rID"];
            this.txtNODE_TEXT.Text = e.ExtraParams["rTEXT"];
            string sURL = "";
            string sDATE = "";

            switch (this.txtNODE_ID.Text.Substring(1, 1))
            {
                case "_":
                    sDATE = "x";
                    break;
                case "0":
                    sDATE = "";
                    break;
                default:
                    sDATE = this.txtNODE_TEXT.Text;
                    break;
            }

            if (sDATE !="x")
            {
                switch (this.txtNODE_ID.Text.Substring(0, 1))
                {
                    case "A":
                        sURL = "./Dialysis_0h_01.aspx";
                        break;
                    case "B":
                        sURL = "./Dialysis_0h_02.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    case "C":
                        sURL = "./Dialysis_0h_03.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    case "D":
                        sURL = "./Dialysis_0h_04.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    case "E":
                        sURL = "./Dialysis_0h_05.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    case "F":
                        sURL = "./Dialysis_0h_06.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    case "G":
                        sURL = "./Dialysis_0h_07.aspx?editmode=" + (sDATE != "" ? "show&sel_info_date=" + sDATE : "edit&editmode2=add");
                        break;
                    //case "H":
                    //    sURL = "./Dialysis_0h_08.aspx";
                    //    break;
                    default:
                        sURL = "";
                        break;
                }
                if (sURL != "")
                {
                    this.Panel1.Loader.SuspendScripting();
                    this.Panel1.Loader.Url = sURL;
                    this.Panel1.Loader.DisableCaching = true;
                    this.Panel1.LoadContent();
                }
            }
        }
    }
}