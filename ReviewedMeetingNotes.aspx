<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewedMeetingNotes.aspx.cs" MasterPageFile="~/ic.master" Inherits="ReviewedMeetingNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/default.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/default.css")).Ticks.ToString() %>" rel="stylesheet" />
     <script src="js/reviewedmeetingnotes.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/reviewedmeetingnotes.js")).Ticks.ToString() %>"></script>
    
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
   

   
 

  
</asp:Content>