<div id="subTitle" class="subtitle">Assign Meeting Attributes <span style="margin-left:295px;cursor:pointer" onclick="hideForm();">X</span></div>


 <%-- <div id="divResults">--%>
       
       
          <div style="width: 510px; height: 300px; overflow: auto;text-align:left;font-size:10pt">
           <div>
                 <label>Choose A Symbol</label>
                 <select id="ddlSymbols" style="width:215px;"></select>
                 <label style="cursor:pointer;width:10px;" onclick="AddSymbol()"><u>New</u></label>
            </div>
               <div id="divAddNewSymbol" style="display:none;padding-left:255px;">
                  <input type="text" id="txtNewSymbol" />  <label style="cursor:pointer;width:10px;" onclick="InsertNewSymbol()"><u style="color:deepskyblue;">Add</u></label>
              </div>
          <div style="padding-top:5px;">
              <label style="vertical-align:top;">Attendees</label>
              <select id="ddlCandidatesMeeting" style="width:215px;" multiple="multiple">
                  <option value="bjohnson">Brandon Johnson</option>
                  <option value="tfriel">Tom Friel</option>
                  <option value="jwalsh">Jed Walsh</option>
              </select>
          </div>
          <div>
              <label>Position Recommendation</label>
             <input name="poistionReco" type="radio" value="true" />
              <label style="width:20px;padding-left:0px">Yes</label>
              <input name="poistionReco" type="radio" value="false"/>
              <label style="width:20px;padding-left:0px">No</label>
          </div>
        
          <div style="padding-top:10px;padding-left:250px;">
          <input type="button" id="btnSaveMeetingAttributes" value="Save" onclick="SaveMeetingAttributes()"/>    
          </div>
          </div>
       
<%-- </div>--%>