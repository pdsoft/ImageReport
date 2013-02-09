<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportOption.aspx.cs" Inherits="ImageReport.ReportOption" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/site.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function openPDF(URL) {
            window.open(URL, null, "left=60,top=100,height=400,width=800," +
 		        "resizable=yes,titlebar=yes,status=yes,toolbar=no," +
		        "menubar=yes,location=no,dependent=yes");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
       <div runat="server" id="frmOption">
       <table>
         <tr style="display:none">
            <td>
               Total Cost:
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                
            </td>
            <td>
               HST:
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                
            </td>
         </tr>
         <tr>
           <td>Report Title</td>
           <td colspan=3>
              <asp:TextBox ID="txtTitle" runat="server" Width="662px">Report Title</asp:TextBox>
           </td>
         </tr>
         <tr>
           <td>Report subtitle</td>
           <td colspan=3>
              <asp:TextBox ID="txtSubtitle" runat="server" Width="662px" Height="45px">Report subtitle</asp:TextBox>
           </td>
         </tr>
         <tr>
           <td>Report Footer</td>
           <td colspan=3>
              <asp:TextBox ID="txtFooter" runat="server" Width="662px" Height="50px">Report footer</asp:TextBox>
           </td>
         </tr>
         <tr>
           <td colspan=4 style="text-align: center" Height="45px">
              <asp:Button runat="server" text="Generate pdf" ID="cmdGenerateRpt" 
                   onclick="cmdGenerateRpt_Click" ></asp:Button>
           </td>
         </tr>
      </table>
      </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" style="display:none"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
            <LocalReport ReportPath="Report.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     <div runat="server" id="divLable">
     </div>
    </form>
</body>
</html>
