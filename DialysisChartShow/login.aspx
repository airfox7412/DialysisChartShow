<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Dialysis_Chart_Show.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登入窗口</title>
    <script type="text/javascript">
        // Invalidate the login fields with the given reason.
        var invalidateLogin = function (reason) {
            App.txtUsername.setValidation(reason);
            App.txtPassword.setValidation(reason);
            App.txtUsername.validate();
            App.txtPassword.validate();

            Ext.MessageBox.show({
                title: '认证错误',
                msg: reason,
                buttons: Ext.MessageBox.OK,
                animateTarget: 'Window1',
                icon: 'Error'
            });
        };

        var handleLogin = function () {
            var form = App.Window1.el.up().dom; // Window1 is a direct child of the form element.
            var url = App.txtUrl.value;
            App.Window1.close();

            // Now this would work for Chrome, and we already triggered autoComplete for IE.
            // Chrome's is only triggered after the destination page is loaded.
            setForm(form, url, form.target);
            form.submit();
            restoreForm(form);
        };

        var orgFormAction, orgFormTarget,
            btn = null, iframe = null;

        // If we are on IE, we will create a button and click it (at once),
        // so autoComplete is triggered.
        var handleClientClick = function () {
            var form = App.Window1.el.up().dom; // Window1 is a direct child of the form element.

            if (Ext.isIE) {
                if (iframe == null) {
                    iframe = document.createElement("IFRAME");
                    iframe.name = "ie_login_flush";
                    iframe.style.display = "none";
                    form.appendChild(iframe);
                }

                if (btn == null) {
                    btn = document.createElement("BUTTON");
                    btn.type = "submit";
                    btn.id = "submitButton";
                    btn.style.display = "none";
                    form.appendChild(btn);
                }

                // On WebForms, we have to force set the form settings as we run or else we'll
                // break directEvent calls.
                setForm(form, "about:blank", "ie_login_flush");
                btn.click();
                restoreForm(form);
            }
        }

        var setForm = function (form, action, target) {
            // Back up original settings once per execution.
            if (typeof orgFormAction == 'undefined') {
                orgFormAction = form.action;
            }
            if (typeof orgFormTarget == 'undefined') {
                orgFormTarget = form.target;
            }

            form.action = action;
            form.target = target;
        };

        var restoreForm = function (form) {
            form.action = orgFormAction;
            form.target = orgFormTarget;
        };
    </script>

    <style type="text/css">
        .Label_copyright1 .x-label-value
        {
            color:White;
        }    
        .Label_copyright2 .x-label-value
        {
            color:Navy;
        }
        
        #Panel_North .x-autocontainer-innerCt
        {
            background:#5ABCE0;
        }
        #Panel_South .x-autocontainer-innerCt
        {
            background:#5ABCE0;
        }
        .Panel_Center .x-autocontainer-innerCt
        {
            /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
            background: #1e5799; /* Old browsers */
            background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
        }
        .x-form-text-default
        {
            font-size:18px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <ext:Hidden ID="txtUrl" runat="server" Text="" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" Locale="zh-CN" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Region="North">
                    <Items>
                        <ext:Container ID="Container3" runat="server" Layout="CenterLayout">
                            <Items>
                                <ext:Image ID="Image1" runat="server" ImageUrl="Styles/topbg6A.png" Width="1386" Height="210" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="PanelCenter" runat="server" Region="Center" Cls="Panel_Center" />
                <ext:Panel ID="Panel_South" runat="server" Region="South">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="CenterLayout">
                            <Items>
                                <ext:Label ID="Label1" runat="server" Text="Copyright © 2015 DATACOM TECHNOLOGY CORP. All rights reserved  (V4.1 20160913)" Cls="Label_copyright1"></ext:Label>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="CenterLayout">
                            <Items>
                                <ext:Label ID="Label2" runat="server" Text="本产品具备中华人民共和国专利第 3336979 号" Cls="Label_copyright2"></ext:Label>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Window ID="Window1" runat="server" Closable="false" Resizable="false" Icon="Lock" Title="登入窗口" Draggable="true" Width="350" Height="200" Modal="false" BodyPadding="5" Layout="Form" Hidden="true">
                    <Items>
                        <ext:TextField ID="txtUsername" runat="server" Name="username" FieldLabel="用户名" AllowBlank="false" BlankText="你的用户名是需要的." Text="" EnableKeyEvents="true">
                            <DirectEvents>
                                <KeyPress OnEvent="Next_KeyPress">
                                    <ExtraParams>
                                        <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                                    </ExtraParams>
                                </KeyPress>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:TextField ID="txtPassword" runat="server" Name="password" InputType="Password" FieldLabel="密码" AllowBlank="false" BlankText="你的密码是需要的." Text="" EnableKeyEvents="true">
                            <DirectEvents>
                                <KeyPress OnEvent="Login_KeyPress" Failure="invalidateLogin(result.errorMessage);" ShowWarningOnFailure="false">
                                    <ExtraParams>
                                        <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                                    </ExtraParams>
                                </KeyPress>
                            </DirectEvents>
                        </ext:TextField>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Text="登录" Icon="Accept" FormBind="true" Handler="handleClientClick">
                            <DirectEvents>
                                <Click OnEvent="Button1_Click" Failure="invalidateLogin(result.errorMessage);" ShowWarningOnFailure="false">
                                    <EventMask ShowMask="true" Msg="Verifying..." MinDelay="500" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
