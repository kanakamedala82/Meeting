<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPositionRecommendations.aspx.cs" MasterPageFile="~/ic.master" Inherits="AddPositionRecommendations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
    <link href="css/default.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/default.css")).Ticks.ToString() %>" rel="stylesheet" />
     <script src="js/positionrecommendations.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/positionrecommendations.js")).Ticks.ToString() %>"></script>
    <link href="css/select2.css?<%= System.IO.File.GetLastWriteTime(Server.MapPath("css/select2.css")).Ticks.ToString() %>" rel="stylesheet"/>
	
         <script src="js/select2.js?<%= System.IO.File.GetLastWriteTime(Server.MapPath("js/select2.js")).Ticks.ToString() %>"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <div  style="width: 1900px; padding-top: 50px;">
        <fieldset id="fldPositionRecommendation">
            <legend>Postion Recommendations</legend>
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
     
                 <div id="divPositionRecommendations" style="width:1400px;">
        
         <%--   <table style="width:100%">
                 <tr><td style="font-weight:bold;background-color:white;">TSLA</td></tr>
                <tr> <th>Date</th><th>TSLA 1Allocation</th><th>TSLA 2 Allocation</th><th>Pros</th><th>Cons</th></tr>
                <tr><td style="vertical-align:top;">09/16/2016</td><td style="vertical-align:top;"><input type="text" /></td><td style="vertical-align:top;"><input type="text" /></td><td><textarea style="width:700px;height:100px;"></textarea></td><td><textarea style="width:700px;height:100px;"></textarea></td></tr>
                <tr><td><u>05/15/2016</u></td><td>1%</td><td>4%</td><td>test</td><td>Bad</td></tr>
                <tr><td><u>01/15/2016</u></td><td>5%</td><td>10%</td><td>test1</td><td>Bad</td></tr>
                <tr><td style="font-weight:bold;background-color:white;">IBM</td></tr>
                <tr> <th>Date</th><th>IBM1 Allocation</th><th></th><th>Pros</th><th>Cons</th></tr>
                <tr><td style="vertical-align:top;">09/16/2016</td><td style="vertical-align:top;"><input type="text" /></td><td></td><td><textarea style="width:700px;height:100px;"></textarea></td><td><textarea style="width:700px;height:100px;"></textarea></td></tr>
                <tr><td><u>05/15/2016</u></td><td>1%</td><td></td><td>test</td><td>Bad</td></tr>
                <tr><td><u>01/15/2016</u></td><td>5%</td><td></td><td>test1</td><td>Bad</td></tr>
            </table>--%>
         </div>
        
              
            </div>
          <div>
         <%-- <div><label style="font-weight:bold;">IBM</label></div>
                 <div style="width:1400px;">
        
            <table style="width:100%">
               
                <tr> <th>Date</th><th>IBM1 Allocation</th><th>Pros</th><th>Cons</th></tr>
                <tr><td style="vertical-align:top;">09/16/2016</td><td style="vertical-align:top;"><input type="text" /></td><td><textarea style="width:700px;height:100px;"></textarea></td><td><textarea style="width:700px;height:100px;"></textarea></td></tr>
                <tr><td><u>05/15/2016</u></td><td>1%</td><td></td><td>test</td><td>Bad</td></tr>
                <tr><td><u>01/15/2016</u></td><td>5%</td><td></td><td>test1</td><td>Bad</td></tr>
            </table>
         </div>--%>
        
              
            </div>
        </fieldset>
    </div>

   
 

    <div id="overlay"></div>
  <%-- <div id="divPopUp" style="height:400px;width:450px;" class="shadow"></div>--%>
      <div id="divPopUp" class="shadow"></div>
</asp:Content>