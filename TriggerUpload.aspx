<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="TriggerUpload.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <link rel="Stylesheet" type="text/css" href="/CSS/uploadify.css" />
 <link href="/CSS/site.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="scripts/jquery-1.4.1.min.js"></script>
 <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>

</head>
<body>
    <form id="form1" runat="server">
      <table style="width: 100%">
         <tr>
         <td colspan=2>
            <div style = "padding:10px">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
         </td>
         <td style="display:none">
           <a href="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadStart();">Start Upload</a>&nbsp; 
         </td>
         <td style="display:none">
           <a href="javascript:$('#<%=FileUpload1.ClientID%>').fileUploadClearQueue()">Clear Selection</a> 
         </td>
       </tr>
       <tr>
         <td colspan=4 style="text-align: center">
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <img src="images/BA-Email-Picture.gif" />
         </td>
       </tr>

      </table>

    </form>
</body>
</html>

<script type = "text/javascript">
$(window).load(
    function() {
        $("#<%=FileUpload1.ClientID%>").fileUpload({
        'uploader': '/scripts/uploader.swf',
        'cancelImg': '/images/cancel.png',
        'buttonText': 'Browse Files',
        'script': 'Upload.ashx',
        'folder': 'uploads',
        'fileDesc': 'Image Files',
        'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
        'multi': true,
        'auto': true,
        'onAllComplete': function (event, data) { parent.OpenImageEditForm(); }
    });
   }
);

</script> 
