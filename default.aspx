<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ImageReport._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="/Styles/Tabs.css" rel="stylesheet" type="text/css" />

<style>

   .BodyTopPanel {
       background-color:silver;
       width:98%;
       height:30px;
   }
</style>

<script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {

        //Default Action
        $(".tab_content").hide(); //Hide all content
        $("ul.tabs li:first").addClass("active").show(); //Activate first tab
        $(".tab_content:first").show(); //Show first tab content
        LoadForm4Body("#tab1");

        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $(this).addClass("active"); //Add "active" class to selected tab
            //$(".tab_content").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            //$(activeTab).fadeIn(); //Fade in the active content

            LoadForm4Body(activeTab.toString());

            return false;
        });
    });

    //--------------
    function OpenImageEditForm() {
        $("ul.tabs li").removeClass("active");
        $('a[href$="#tab2"]').parent().addClass("active");

        LoadForm4Body("#tab2");
    }


    //--------------
    function LoadForm4Body(TabRref) {
        switch (TabRref) {
            case "#tab1":
                iFrame1.document.location = "TriggerUpload.aspx"; 
                break;
            case "#tab2":
                iFrame1.document.location = "ImageEdit.aspx";
                break;
            case "#tab3":
                iFrame1.document.location = "ReportOption.aspx";
                break;
            default:
        }
    }
</script>

</head>
<body scroll="no" style=" margin-top:0px; margin-left:4px; overflow: hidden" >
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="BodyTopPanel" BorderColor="DarkBlue" BorderWidth="0px" 
         style="margin-top:0px; margin-left:8px; text-align:left display:none ;">
            <table>
              <tr> 
                <td style="width: 160px; text-align: right;">Authorization Code: &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtPasscode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Submit" 
                        onclick="Button1_Click"  />
                </td>
                <td>
                  <asp:Label ID="lblPasscode" runat="server" ForeColor="red"></asp:Label>
                </td>
              </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" CssClass="BodyDetalPanel" BorderColor="DarkBlue" style="margin-top:-4px; display: none;">
            <div class="container">
                <ul class="tabs">
                    <li><a href="#tab1">Upload Images</a></li>
                    <li><a href="#tab2">Add Image Notes</a></li>
                    <li><a href="#tab3">Generate Report</a></li>
                </ul>
                    <div class="tab_container" style="height: 620px; background-color:  #fff;">
                      <iframe id="iFrame1" runat="server" src="" style="width:100%; height:99%; " frameBorder="0" scrolling="no"></iframe>
                    </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
