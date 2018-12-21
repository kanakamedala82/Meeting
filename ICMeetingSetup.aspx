<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ICMeetingSetup.aspx.cs" MasterPageFile="~/ic.master" Inherits="ICMeetingSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/default.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/default.css")).Ticks.ToString() %>" rel="stylesheet" />
     <script src="js/meetingsetup.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/meetingsetup.js")).Ticks.ToString() %>"></script>
    <link href="css/select2.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/select2.css")).Ticks.ToString() %>" rel="stylesheet"/>
	
    <script src="js/select2.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/select2.js")).Ticks.ToString() %>"></script>
    <link href="css/jquery.timepicker.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/jquery.timepicker.css")).Ticks.ToString() %>" rel="stylesheet"/>
	
   <script src="js/jquery.timepicker.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/jquery.timepicker.js")).Ticks.ToString() %>"></script>
     <script src="js/jquery.alphanum.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/jquery.alphanum.js")).Ticks.ToString() %>"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <div id="divMeetingSetup" style="width: 1400px; padding-top: 50px; padding-left: 200px;">
        <fieldset id="fldMeeting">
            <legend>Setup A Meeting</legend>
         
        
         <div style="height:500px;overflow:auto;text-align:left;font-size:10pt">
          <div>
              <label>Date of Meeting:</label>
               <input type="text" id="txtMeetingDate" class="date" />
          </div>
          <div>
              <label>Did IC meeting start at 3pm</label>
            
              <input name="meetStart3Pm" type="radio" value="true" />
              <label style="width:20px;padding-left:0px">Yes</label>
               <input name="meetStart3Pm" type="radio" value="false" />
              <label style="width:20px;padding-left:0px">No</label>
                  
          </div>
          <div>
              <label>IC Meeting Start Time</label>
              <input type="text" id="txtMeetingStartTime" />
          </div>
         <div>
              <label>IC Meeting End Time</label>
              <input type="text" id="txtMeetingEndTime" />
          </div>
          <div>
              <label>Were Original notes 1page in length per 15 minutes of meeting</label>
             <input name="origNotes1Page" type="radio" value="true"/>
              <label style="width:20px;padding-left:0px">Yes</label>
              <input name="origNotes1Page" type="radio" value="false"/>
              <label style="width:20px;padding-left:0px">No</label>
          </div>
          <div>
              <label>Original length of notes (# of pages)</label>
              <input type="text" id="txtOrigNumOfPages" />
          </div>
           <div>
              <label>Final length of notes (# of pages)</label>
              <input type="text" id="txtFinNumOfPages" />
          </div>
          <div>
              <label>Link to meeting doc</label>
              <input type="file" id="txtLinkMeetingDoc" name="txtLinkMeetingDoc" />
              <label id="lblLinkMeetingDoc"></label>
         </div>
         <div>
              <label>Lock Meeting</label>
             <input name="lockMeeting" type="radio" value="true"/>
              <label style="width:20px;padding-left:0px">Yes</label>
              <input name="lockMeeting" type="radio" value="false"/>
              <label style="width:20px;padding-left:0px">No</label>
          </div>
          
          <div> <label style="cursor:pointer;width:100px;" onclick="AddMeetingAtributes()"><u>Add New</u></label></div>
         <div id="divMeetingAttributes" >
         <div style="border-radius: 10px; border: 1px solid grey; border-image: none;">
          <div>
                 <label>Choose A Symbol</label>
                 <select id="ddlSymbols1" style="width:215px;" onchange="PopulateStocks(1)" multiple="multiple"></select>
                 <%--<label style="cursor:pointer;width:10px;" onclick="AddSymbol()"><u>New</u></label> need to add back Venkat--%>
         </div>
               <div id="divAddNewSymbol" style="display:none;margin-left:255px;">
                 <%-- <input type="text" id="txtNewSymbol" />  <label style="cursor:pointer;width:10px;" onclick="InsertNewSymbol()"><u style="color:deepskyblue;">Add</u></label>--%>
              </div>
          <div style="padding-top:5px">
              <label style="vertical-align:top;">Choose A Security</label>
              <select id="ddlSecurities1" style="width:215px;height:100px;" multiple="multiple">
              </select>

          </div>
        <%--  <div style="padding-top:5px;">--%>
            
             <%-- <label style="vertical-align:top;">Attendees</label>--%>
             <%-- <select id="ddlCandidatesMeeting1" style="width:215px;height:150px;" multiple="multiple">
               
              </select>--%>
              <div id="divAttendees1" style="padding-top:5px;">
                   <label style="vertical-align:top;">Attendees</label>
              </div>
                <%--  </div>--%>
            
         
          <div style="padding-top:5px;padding-left:20px;">
              <label>Position Recommendation</label>
              <input name="poistionReco1" type="radio" value="true" />
              <span style="width:20px;padding-left:0px">Yes</span>
              <input name="poistionReco1" type="radio" value="false"/>
              <span style="width:20px;padding-left:0px">No</span>
          </div>
         </div>
        </div>




         <%-- <div style="padding-top:10px;padding-left:250px;">
          <input type="button" id="btnSaveMeeting" value="Save" onclick="SaveMeeting()"/>    
          </div>--%>
   </div>
            
        <div style="padding-top:10px;">
          <input type="button" id="btnSaveMeeting" value="Save" onclick="SaveMeeting()"/> 
          <input type="button" id="btnSendEmails" value="Send Meeting Email" onclick="SendICMeetingNotesEmail()"/>   
          <input type="button" id="btnSendPositionRecommendations" value="Send Position Recommendation Email" onclick="OpenPositionRecommendationsPopup()"/>   
        </div>   
            
            
            
            
            
             <%--  <div>
           <ol>
            
             <li><label style="cursor:pointer;" onclick="CreateMeeting()"><u>Create Meeting</u></label> </li>
                <li><div><label style="cursor:pointer;" onclick="EditMeeting()"><u>Edit Meeting</u></label></div>
                 <select id="ddlICMeetings" style="width:200px"></select>
             </li>
           
           </ol>
              
          </div>--%>
            <input type="hidden" id="hiddenCreateUpdate" />
        </fieldset>
    </div>


     <div id="divMeetingEdit" style="width: 1400px; padding-top: 50px; padding-left: 200px;">
        <fieldset id="fldMeetingEdit">
            <legend>Edit Meeting</legend>
  <%-- <div><label style="cursor:pointer;" onclick="EditMeeting()"><u>Edit Meeting</u></label></div>--%>
   <div><select id="ddlICMeetings" style="width:220px" onchange="EditMeeting()"></select></div>
     </fieldset>
    </div>
 

    <div id="overlay"></div>
  <%-- <div id="divPopUp" style="height:400px;width:450px;" class="shadow"></div>--%>
      <div id="divPopUp" class="shadow"></div>
</asp:Content>
