<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="AutoUpload.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="Stylesheet" type="text/css" href="CSS/uploadify.css" />
     <script type="text/javascript" src="scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <div style = "padding:40px">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
</form>
</body>
</html>
<script type = "text/javascript">
$(window).load(
    function() {
    $("#<%=FileUpload1.ClientID %>").fileUpload({
        'uploader': 'scripts/uploader.swf',
        'cancelImg': 'images/cancel.png',
        'buttonText': 'Browse Files',
        'script': 'Upload.ashx',
        'folder': 'uploads',
        'fileDesc': 'Image Files',
        'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
        'multi': true,
        'auto': true
    });
   }
);
</script> 
