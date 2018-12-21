<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MeetingNonAttendeeReason.aspx.cs" MasterPageFile="~/ic.master" Inherits="MeetingNonAttendeeReason" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/default.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/default.css")).Ticks.ToString() %>" rel="stylesheet" />
     <script src="js/meetingnonattendee.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/meetingnonattendee.js")).Ticks.ToString() %>"></script>
    <link href="css/select2.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/select2.css")).Ticks.ToString() %>" rel="stylesheet"/>
	
         <script src="js/select2.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/select2.js")).Ticks.ToString() %>"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <div  style="width: 1900px; padding-top: 50px;">
       <div id="divMeetingHeading" style="font-weight:bold;"></div>
        <div><textarea id="txtNonAttendeeReason" style="width:500px;height:100px;"></textarea></div>
        <div><input type="button" style="margin-top:10px;" value="Save" onclick="SaveReason()" /></div>
    </div>

   
 

    <div id="overlay"></div>
  <%-- <div id="divPopUp" style="height:400px;width:450px;" class="shadow"></div>--%>
      <div id="divPopUp" class="shadow"></div>
</asp:Content>