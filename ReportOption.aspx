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
              <asp:TextBox ID="txtTitle" runat="server" Width="949px">Report Title</asp:TextBox>
           </td>
         </tr>
         <tr style="display:none">
           <td>Report subtitle</td>
           <td colspan=3>
              <asp:TextBox ID="txtSubtitle" runat="server" Width="662px" Height="45px">Report subtitle</asp:TextBox>
           </td>
         </tr>
         <tr>
           <td valign=top><br />Overview
               <br />
               Summary:</td>
           <td colspan=3>
              <asp:TextBox ID="txtSummary" runat="server" Width="953px" Height="348px" 
                   TextMode="MultiLine">
HOW MUCH DOES IT COST FOR A ROOF REPORT ? 

Most roofing companies will charge up to $100 or more for a Roof Inspection and the issuance of the Roof  Report.   Often times, this inspection fee is paid for the Roof Inspection and the Roof Report only to realize that you just paid to find out that the roof requires a major repair or needs a roof replacement, with the only consolation being that you can now apply the cost of this inspection fee to a future roof repair or roof replacement.   The problem with this inspection fee is that it is owed to the roofing company regardless of how unreasonable the roof repair, roof maintenance or roof replacement estimates turn out to be in the Roof Report. And, if for whatever reason, the recommended repairs are never completed or a different roofing company is preferred, then someone ends up having to pay for this inspection fee.  Roofing contractors know that they book a small percentage of repair jobs out of all the roofs that they inspect.  Sadly, many roofing companies defend their collection of this inspection fee since it has become their primary source of income, due to the fact that they do very few actual repair jobs. 

WHAT IS A FREE ROOF REPORT ? 

At ROOF DOCTORS, we don't feel that it is good business practice to charge an inspection fee for a Roof Inspection and a Roof Report. The cost for a ROOF DOCTOR's Roof Inspection and Roof Report is FREE of charge.   There are no gimmicks and no games.   ROOF DOCTORS does not require the purchase of our roof repairs or our No-Dollar Limitation Roof Certification Leak Warranty in order for our Roof Inspection and Roof Report to be FREE of charge.   There is no obligation until we are authorized to perform any roof repairs or until we are authorized to issue our 2 or 3-Year No-Dollar Limitation Roof Certification Leak Warranty. A ROOF DOCTOR's Roof Inspection and Roof Report will describe the condition of the roof and any repairs that may be necessary to satisfy the requirement for a Roof Certification that may be needed to complete a real estate transaction.  
                   </asp:TextBox>
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
