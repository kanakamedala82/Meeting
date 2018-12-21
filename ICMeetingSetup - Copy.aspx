<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ICMeetingSetup.aspx.cs" MasterPageFile="~/ic.master" Inherits="ICMeetingSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/default.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/default.css")).Ticks.ToString() %>" rel="stylesheet" />
     <script src="js/meetingsetup.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/meetingsetup.js")).Ticks.ToString() %>"></script>
    <link href="css/select2.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/select2.css")).Ticks.ToString() %>" rel="stylesheet"/>
	
         <script src="js/select2.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/select2.js")).Ticks.ToString() %>"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <div id="divMeetingSetup" style="width: 1000px; padding-top: 50px; padding-left: 400px;">
        <fieldset id="fldMeeting">
            <legend>Setup A Meeting</legend>
          <%--  <div id="divMeeting">
                <label>Date of Meeting:</label>
                 <input type="text" id="txtMeetingDate" class="date" />
            </div>
            <div>
                 <label>Choose A Symbol</label>
                 <select id="ddlSymbols" style="width:250px;"></select>
                 <label style="cursor:pointer;" onclick="AddSymbol()"><u>Add Symbol</u></label>
            </div>
            <div>
                <input type="button" value="Export To Excel" id="btnExport"/>
            </div>--%>
           <div>
           <ol>
            
             <li><label style="cursor:pointer;" onclick="CreateMeeting()"><u>Create Meeting</u></label> </li>
                <li><div><label style="cursor:pointer;" onclick="EditMeeting()"><u>Edit Meeting</u></label></div>
                 <select id="ddlICMeetings" style="width:200px"></select>
             </li>
           
           </ol>
              
          </div>
            <input type="hidden" id="hiddenCreateUpdate" />
        </fieldset>
    </div>

   <%-- <div id="divSymbolSetup" style="width: 1000px; padding-top: 50px; padding-left: 400px;">
        <fieldset id="fldSymbol">
            <legend>Assign Symbols</legend>
      
             <div>
           <ol>
            
         
                <li><div><label style="cursor:pointer;" onclick="AssignMeetingAttributes()"><u>Assign Symbol to Meeting</u></label></div>
                 <select id="ddlICMeetingSymbols" style="width:200px;"></select>
             </li>
           
           </ol>
              
            </div>

            <div id="divMeetingAttributesOld" style="padding-left:40px;display:none;">
                <label style="font-weight:bold;">TSLA</label>
                <div id="divSymbolAttendees">

                    <label style="font-weight:bold;">Attendees:</label>
                    <label>Brandon Johnson,Jed Walsh</label>
                </div>
                 <div id="divPositReco">

                    <label style="font-weight:bold;">Require Recommendations:</label>
                    <label>Yes</label>
                </div>

            </div>
        </fieldset>
    </div>

     <div id="divEmailAttendess" style="width: 1000px; padding-top: 50px; padding-left: 400px;">
        <fieldset id="fldEmailAttendees">
            <legend>Email Attendees</legend>
        
             <div>
             <ol>
            
          
             <li><div><label>Select Meeting</label></div>
                 <select id="ddlICMeetingsEmail" style="width:200px"></select>
             </li>
                 <li><div><label>Select Attendees</label></div>
                 <select id="ddlCandidate" style="width:215px;" multiple="multiple">
                  <option>Brandon Johnson</option>
                  <option>Tom Friel</option>
                  <option>Jed Walsh</option>
              </select>
             </li>
           
           </ol>
              <div>  <input type="button" id="btnSaveMeetingAttributes" value="Send Email" onclick="SaveMeetingAttributes()"/> </div>
            </div>
        </fieldset>
    </div>--%>
 

    <div id="overlay"></div>
  <%-- <div id="divPopUp" style="height:400px;width:450px;" class="shadow"></div>--%>
      <div id="divPopUp" class="shadow"></div>
</asp:Content>
