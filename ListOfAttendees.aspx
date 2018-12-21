<div id="subTitle" class="subtitle">Select List of Attendees <span style="margin-left:295px;cursor:pointer" onclick="hideForm();">X</span></div>


  <div id="divResults">
       
       
          <div style="width: 450px; height: 250px; overflow: auto;text-align:left;font-size:10pt">
          <div>
              <label>Symbol</label>
               <label id="lblSymbol" />
          </div>
          <div>
              <label>Attendees</label>
              <select id="ddlCandidate" style="width:215px;"></select>
          </div>
          <div>
              <label>Interview Date</label>
              <input type="text" id="txtInterviewDate" />
          </div>
        
          <div style="padding-top:10px;padding-left:150px;">
          <input type="button" id="btnSaveAttendees" value="Save" onclick="SaveAttendees()"/>    
          </div>
          </div>
       
 </div>