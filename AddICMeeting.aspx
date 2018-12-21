<div id="subTitle" class="subtitle">Create New Meeting <span style="margin-left:355px;cursor:pointer" onclick="hideForm();">X</span></div>


  <%--<div id="divResults">--%>
       
       
        <%--  <div style="width: 520px; height: 400px; overflow: auto;text-align:left;font-size:10pt">--%>
       <%-- <div style="height:500px;overflow:auto;text-align:left;font-size:10pt">--%>
         <div style="height:600px;overflow:auto;text-align:left;font-size:10pt">
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
              <label>Original length of notes(# of pages)</label>
              <input type="text" id="txtOrigNumOfPages" />
          </div>
           <div>
              <label>Final length of notes(# of pages)</label>
              <input type="text" id="txtFinNumOfPages" />
          </div>
          <div>
              <label>Link to meeting doc</label>
              <input type="text" id="txtLinkMeetingDoc" />
          </div>
          
          <div> <label style="cursor:pointer;width:100px;" onclick="AddMeetingAtributes()"><u>Add New</u></label></div>
         <div id="divMeetingAttributes" >
         <div style="border-radius: 10px; border: 1px solid grey; border-image: none;">
          <div>
                 <label>Choose A Symbol</label>
                 <select id="ddlSymbols1" style="width:215px;"></select>
                 <label style="cursor:pointer;width:10px;" onclick="AddSymbol()"><u>New</u></label>
            </div>
               <div id="divAddNewSymbol" style="display:none;margin-left:255px;">
                 <%-- <input type="text" id="txtNewSymbol" />  <label style="cursor:pointer;width:10px;" onclick="InsertNewSymbol()"><u style="color:deepskyblue;">Add</u></label>--%>
              </div>
          <div style="padding-top:5px;">
              <label style="vertical-align:top;">Attendees</label>
              <select id="ddlCandidatesMeeting1" style="width:215px;height:150px;" multiple="multiple">
               
              </select>
          </div>
          <div>
              <label>Position Recommendation</label>
             <input name="poistionReco1" type="radio" value="true" />
              <label style="width:20px;padding-left:0px">Yes</label>
              <input name="poistionReco1" type="radio" value="false"/>
              <label style="width:20px;padding-left:0px">No</label>
          </div>
         </div>
        </div>




          <div style="padding-top:10px;padding-left:250px;">
          <input type="button" id="btnSaveMeeting" value="Save" onclick="SaveMeeting()"/>    
          </div>
   </div>
       
<%-- </div>--%>