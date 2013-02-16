<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_EditForm.ascx.cs" Inherits="ImageReport.uc_EditForm" %>
      
      
<table width="560px">
<tr>
<td style="text-align: right">
    &nbsp;</td>
<td align="right" style="text-align: right">
    <%--<img alt="" class="style1" Style='cursor:hand' src="images/checked.gif" onclick="SubmitTextbox();" />--%>
    <input id="Button2" type="button" value="Save" onclick="SubmitTextbox();"
        style="color: #CC3300; font-size: 11px" />&nbsp;&nbsp;&nbsp;
    <img src="images/del2.gif" Style="cursor:pointer" onclick="HideTextbox() " />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
</tr>
<tr>
    <td Class="textbox" style="text-align: right">
        Observation:
    </td>
    <td>
        <asp:TextBox ID="txtObservation" runat="server" Width="330px" CssClass="textbox"></asp:TextBox>
<%--        <asp:DropDownList ID="ddObservation" runat="server" Width="330px" CssClass="textbox">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>&nbsp;--%>
        <asp:LinkButton ID="LinkButton1" CssClass="link" runat="server" OnClientClick="AddItem2PickList();return false;">Add to list</asp:LinkButton>
    </td>
</tr>
<tr>
    <td Class="textbox" style="text-align: right">
        Deficiency:
    </td>
    <td>
        <asp:TextBox ID="txtDeficiency" runat="server" Width="330px" CssClass="textbox"></asp:TextBox>
<%--        <asp:DropDownList ID="ddDeficiency" runat="server" Width="330px" CssClass="textbox">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>&nbsp;--%>
        <asp:LinkButton ID="LinkButton2" CssClass="link" runat="server" OnClientClick="AddItem2PickList();return false;">Add to list</asp:LinkButton>
    </td>
</tr>
<tr>
    <td Class="textbox" style="text-align: right" valign="top">
        <br />
        Recommendation:
    </td>
    <td valign="top">
        <asp:TextBox ID="txtRecommendation" runat="server" Width="330px" Height="80px" 
        cssclass="textbox" TextMode="MultiLine"></asp:TextBox>&nbsp;
        <asp:LinkButton ID="LinkButton3" CssClass="link" runat="server" OnClientClick="AddItem2PickList();return false;">Add to list</asp:LinkButton>
    </td>
</tr>
<tr>
<td colspan=2 style="text-align: center">
    &nbsp;</td>
</tr>
</table>