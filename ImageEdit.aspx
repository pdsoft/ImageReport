<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageEdit.aspx.cs" Inherits="ImageReport.ImageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="ImgEdit" TagName="EditForm" Src="/WebUserControl/uc_EditForm.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <%--<script src="scripts/DivMouseMov.js" type="text/javascript"></script>--%>

    <link href="/CSS/site.css" rel="stylesheet" type="text/css" />

 <script type="text/javascript">

     //------------------------------------
     //----------------------------------------
     function calculateSumOffset(idItem, offsetName) {
         var totalOffset = 0;
         var item = eval('idItem');
         do {
             totalOffset += eval('item.' + offsetName);
             item = eval('item.offsetParent');
         } while (item != null);
         return totalOffset;
     }
     
     //-----------------------------------------
     function ShowEditTextbox(ImageNo, fileName) {
         //---------------- ajax call to get the description text
         var txt1=null;

         $.ajax({
             type: "POST",
             async: false, 
             contentType: "application/json",
             url: "ImageEdit.aspx/GetDescription",
             data: '{"fileName":"' + fileName + '"}',
             dataType: "text",
             success: function (d) { txt1 = d; }
         });

         var img = document.getElementById("img" + ImageNo);
         //         leftX = calculateSumOffset(event.srcElement, 'offsetLeft') - 350;
         //         leftY = calculateSumOffset(event.srcElement, 'offsetTop') + 5;
         leftX = calculateSumOffset(img, 'offsetLeft') + 10;
         leftY = calculateSumOffset(img, 'offsetTop') + 10;

         divNote.style.pixelLeft = leftX;
         divNote.style.pixelTop = leftY;
         divNote.style.display = 'inline';

         document.all("_FileName").value = fileName;

         var obj = jQuery.parseJSON(txt1);
         //document.getElementById("edtForm_txtRecommendation").value = obj.d;

         document.getElementById("edtForm_ddObservation").value = $(obj.d).find("Observation").text();
         document.getElementById("edtForm_ddDeficiency").value = $(obj.d).find("Deficiency").text();
         document.getElementById("edtForm_txtRecommendation").value = $(obj.d).find("Recommendation").text();

         //-----------------------------------
         //$("divNote").mousedown(function (e) { alert("ff"); dragBegin(e); });

         // by Fred, 1-19-2013
        // $("#ddImageTextList").val("");
     }



     //-----------------------------------------
     function RemoveImage(ImageNo, fileName) {
         var a = confirm("Confirm to remove image #" + ImageNo);

         if (a) {
             document.all("_Action").value = "REMOVE";
             document.all("_FileName").value = fileName;
             form1.submit();
         }
     }

     //-------------------------------------------
     function HideTextbox() {
        divNote.style.display="none";
     }

     //-------------------------------------------
     function SubmitTextbox() {
         var txt1 = null;

         var desc = "<Summary>" + "<Observation>" + document.getElementById("edtForm_ddObservation").value + "</Observation>"
                     + "<Deficiency>" + document.getElementById("edtForm_ddDeficiency").value + "</Deficiency>"
                     + "<Recommendation>" + document.getElementById("edtForm_txtRecommendation").value + "</Recommendation>"
                     + "</Summary>";
                     
         //alert(desc);

         $.ajax({
             type: "POST",
             async: false,
             contentType: "application/json",
             url: "ImageEdit.aspx/SubmitNewDesc",
             data: 
                  "{\"fileName\":\"" + document.all("_FileName").value + "\"," +
                   "\"Desc\":\"" + desc + "\"}",       
             dataType: "text",
             success: function (d) { txt1 = d; },
             error: function (xhr, ajaxOptions, thrownError) 
              {
                //alert(xhr.status);
                alert(thrownError);
              }
         });

        //form1.submit();

         //document.all("TextBox1").value = txt1.toString();

        // alert(document.all("TextBox1").value);

         HideTextbox();
     }

     //---------------------------------------------
     function PopulateDropdowText() {
         var dd = document.all("ddImageTextList");
         document.all("TextBox1").value = dd.options[dd.selectedIndex].value + "\r\n" +
                                          document.all("TextBox1").value;

     }

     //---------------------------------------------
     function AddItem2PickList()
     {
         alert("ok");

     }

 </script>
    </head>
<body style="width:99%; overflow:hidden"  >
    <form id="form1" runat="server" style="text-align: center; overflow:hidden">
           <div ID="ColumnOption" runat="server" style="display:">
              <asp:DropDownList ID="DropDownList1" runat="server" Width="60px" CssClass="textbox"
                  onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
              <asp:ListItem>1</asp:ListItem>
              <asp:ListItem Selected>2</asp:ListItem>
               <%--<asp:ListItem>3</asp:ListItem>
               <asp:ListItem>4</asp:ListItem>--%>
          </asp:DropDownList> &nbsp; 
          <span class="textbox" style="top:-4px">Column(s) per row</span>
          </div>
      <div ID="divTable" runat="server" style="height:580px; text-align: center; overflow:auto">
      </div>

      <input id="_Action" type="hidden" runat="server" />
      <input id="_FileName" type="hidden" runat="server" />

      <div id="divNote" 
               style="border: 2px outset ; padding: 0px; position:absolute; background-color:Silver; display:none" 
               runat="server">
        <ImgEdit:EditForm runat="server" ID="edtForm" />
      </div>
      
    </form>
</body>
</html>
