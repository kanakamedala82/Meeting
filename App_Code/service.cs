using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;


/// <summary>
/// Summary description for service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class service : System.Web.Services.WebService
{

    public service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetSymbolList()
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("GetCompanyList"))
        {
            sb.Append("<option value=-1>***Choose Company***</option>");
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {

                sb.Append("<option value=\"" + r["SymbolCode"].ToBlank() + "\">" + r["SymbolText"] + "</option>");
            }
        }
        return sb.ToString();
    }

    [WebMethod]
    public string GetSecurityList(string stockSymbol)
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("GetBondList"))
        {
            db.AddParameter("@stockSymbol", stockSymbol);
            // sb.Append("<option value=-1>***Choose Company***</option>");
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {

                sb.Append("<option value=\"" + r["BondID"].ToBlank() + "\">" + r["IssueDesc"] + "</option>");
            }
        }
        return sb.ToString();
    }


    [WebMethod]
    public string GetICMeetings()
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("GetICMeetings"))
        {
            sb.Append("<option value=-1>***Choose Meeting***</option>");
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {

                sb.Append("<option value=\"" + r["MeetingId"].ToBlank() + "\">" + r["MeetingDate"].ToShortDateFormatEx()+"-"+ r["MeetingId"].ToString() + "::ICMeeting" + "</option>");
            }
        }
        return sb.ToString();
    }


    [WebMethod]
    public void InsertICMeeting(string meetingDate, bool? meetingStart3PM, string meetingStartTime, string meetingEndTime, bool? origNotes1PageLength, int? origLengthOfNotes, int? finalLengthOfNotes, string linkToMeetingDoc,bool? isLocked ,List<MeetingAttributesModel> meetingAttributesList)
    {
        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];
        long meetingId;

        using (var db = new DBCommand("ICMeeting_GetMeetingId"))
        {
            db.AddParameter("@meetingDate", meetingDate);
            db.AddParameter("@meetingId", System.Data.ParameterDirection.Output, System.Data.DbType.String);
            db.ExecuteNonQuery();
            meetingId = db.GetOutputParameter("@meetingId").ToLng();
        }
        if (meetingId == 0)
        {
            using (var db = new DBCommand("InsertICMeeting"))
            {
                db.AddParameter("@meetingDate", meetingDate);
                db.AddParameter("@meetingStart3PM", meetingStart3PM);
                db.AddParameter("@meetingStartTime", meetingStartTime);
                db.AddParameter("@meetingEndTime", meetingEndTime);
                db.AddParameter("@origNotes1PageLength", origNotes1PageLength);
                db.AddParameter("@orgiLengthOfNotes", origLengthOfNotes);
                db.AddParameter("@finalLengthOfNotes", finalLengthOfNotes);
                db.AddParameter("@linkToMeetingDoc", linkToMeetingDoc);
                db.AddParameter("@isLocked", isLocked);

                db.AddParameter("@meetingId", System.Data.ParameterDirection.Output, System.Data.DbType.String);
                db.AddParameter("@createdBy", login);


                db.ExecuteNonQuery();
                meetingId = Int64.Parse(db.GetOutputParameter("@meetingId").ToString());
            }

            for (int i = 0; i < meetingAttributesList.Count; i++)
            {
                InsertMeetingAttributes(meetingAttributesList[i].Symbol, meetingId, meetingAttributesList[i].Attendees, meetingAttributesList[i].NonAttendees, meetingAttributesList[i].RequiredRecommendation, meetingAttributesList[i].Securities);

            }
        }
        else
        {
            UpdateICMeeting(meetingId, meetingDate, meetingStart3PM, meetingStartTime, meetingEndTime, origNotes1PageLength, origLengthOfNotes, finalLengthOfNotes, linkToMeetingDoc,  isLocked, meetingAttributesList);
        }
    }
    [WebMethod]
    public void UpdateICMeeting(long meetingId, string meetingDate, bool? meetingStart3PM, string meetingStartTime, string meetingEndTime, bool? origNotes1PageLength, int? origLengthOfNotes, int? finalLengthOfNotes, string linkToMeetingDoc, bool? isLocked, List<MeetingAttributesModel> meetingAttributesList)
    {
        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];

        using (var db = new DBCommand("UpdateICMeeting"))
        {
            db.AddParameter("@meetingId", meetingId);
            db.AddParameter("@meetingDate", meetingDate);
            db.AddParameter("@meetingStart3PM", meetingStart3PM);
            db.AddParameter("@meetingStartTime", meetingStartTime);
            db.AddParameter("@meetingEndTime", meetingEndTime);
            db.AddParameter("@origNotes1PageLength", origNotes1PageLength);
            db.AddParameter("@orgiLengthOfNotes", origLengthOfNotes);
            db.AddParameter("@finalLengthOfNotes", finalLengthOfNotes);
            db.AddParameter("@linkToMeetingDoc", linkToMeetingDoc);
            db.AddParameter("@isLocked", isLocked);


            // db.AddParameter("@modifiedBy", login);


            db.ExecuteNonQuery();

        }

        //DeleteMeetingAttributes(meetingId);
        string symbolList = string.Empty;
        for (int i = 0; i < meetingAttributesList.Count; i++)
        {
           
            InsertMeetingAttributes(meetingAttributesList[i].Symbol, meetingId, meetingAttributesList[i].Attendees, meetingAttributesList[i].NonAttendees, meetingAttributesList[i].RequiredRecommendation, meetingAttributesList[i].Securities);
            if (i == 0)
            {
                symbolList += "'" + meetingAttributesList[i].Symbol + "'";
            }
            else
            {
                symbolList += "," + "'" + meetingAttributesList[i].Symbol + "'";
            }
        }

        using (var db = new DBCommand("ICMeeting_DeleteSymbolList"))
        {
            db.AddParameter("@symbolList", symbolList);
            db.AddParameter("@meetingId", meetingId);
           

            db.ExecuteNonQuery();
        }
    }

    [WebMethod]
    public void InsertMeetingAttributes(string symbol, Int64 meetingId, List<string> attendees, List<string> nonAttendees, bool requiredPositionRecommendation, List<long> securities)
    {
        Int64 tagId;
        using (var db = new DBCommand("InsertICMeetingSymbols"))
        {
            db.AddParameter("@symbol", symbol);
            db.AddParameter("@meetingId", meetingId);
            db.AddParameter("@requiredRecommendation", requiredPositionRecommendation);
            db.AddParameter("@tagId", System.Data.ParameterDirection.Output, System.Data.DbType.String);
            db.ExecuteNonQuery();
            tagId = Int64.Parse(db.GetOutputParameter("@tagId").ToString());
        }
        // string[] attendeesList = attendees.Split(',');
        if (attendees != null)
        {
            string attendeesList = string.Empty;
            for (int i = 0; i < attendees.Count(); i++)
            {
                using (var db = new DBCommand("InsertICMeetingAttendees"))
                {
                    db.AddParameter("@attendee", attendees[i]);
                    db.AddParameter("@tagId", tagId);
                    db.AddParameter("@attendedMeeting", true);

                    db.ExecuteNonQuery();

                    if(i==0)
                    {
                        attendeesList += "'"+attendees[i]+"'";
                    }
                    else
                    {
                        attendeesList += "," + "'"+attendees[i]+"'";
                    }

                }
            }

            if (attendees.Count() > 0)
            {
                using (var db = new DBCommand("ICMeeting_DeleteAttendeesList"))
                {
                    db.AddParameter("@attendeeList", attendeesList);
                    db.AddParameter("@tagId", tagId);
                    db.AddParameter("@attendedMeeting", 1);

                    db.ExecuteNonQuery();
                }
            }

       }
        if (nonAttendees != null)
        {
            string nonAttendeesList = string.Empty;
            for (int i = 0; i < nonAttendees.Count(); i++)
            {
                using (var db = new DBCommand("InsertICMeetingAttendees"))
                {
                    db.AddParameter("@attendee", nonAttendees[i]);
                    db.AddParameter("@tagId", tagId);
                    db.AddParameter("@attendedMeeting", false);

                    db.ExecuteNonQuery();

                }
                if (i == 0)
                {
                    nonAttendeesList += "'" + nonAttendees[i] + "'";
                }
                else
                {
                    nonAttendeesList += "," + "'" + nonAttendees[i] + "'";
                }
            }
            if (nonAttendees.Count() > 0)
            {
                using (var db = new DBCommand("ICMeeting_DeleteAttendeesList"))
                {
                    db.AddParameter("@attendeeList", nonAttendeesList);
                    db.AddParameter("@tagId", tagId);
                    db.AddParameter("@attendedMeeting", 0);

                    db.ExecuteNonQuery();
                }
            }
        }

        if (securities != null)
        {
            string listOfSecurities = string.Empty; ;
            for (int i = 0; i < securities.Count(); i++)
            {
                using (var db = new DBCommand("InsertICMeetingSecurities"))
                {
                    db.AddParameter("@securityId", securities[i]);
                    db.AddParameter("@tagId", tagId);

                    db.ExecuteNonQuery();
                    if (i == 0)
                    {
                        listOfSecurities += securities[i];
                    }
                    else
                    {
                        listOfSecurities += "," + securities[i];
                    }

                }
            }

            using (var db = new DBCommand("ICMeeting_DeleteSecurities"))//delete securities which were added previously.This will be needed during edits
            {
                db.AddParameter("@securityList", listOfSecurities);
                db.AddParameter("@tagId", tagId);
                db.ExecuteNonQuery();
            }
        }
    }

    [WebMethod]
    public void DeleteMeetingAttributes(long meetingId)
    {

        using (var db = new DBCommand("DeleteMeetingAttributes"))
        {

            db.AddParameter("@meetingId", meetingId);

            db.ExecuteNonQuery();

        }

    }


    [WebMethod]
    public string GetMeetignAttributesHtml()
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("GetMeetignAttributes"))
        {
            var ds = db.ExecuteDataSet().Tables[0];
            if (ds.Rows.Count > 0)
            {
                int counter = 0;
                string attendees = string.Empty;
                foreach (System.Data.DataRow r in ds.Rows)
                {
                    if (counter == 0)
                    {
                        sb.Append("<label style='font-weight:bold;'>" + r["Symbol"] + "</label>");
                        sb.Append("<div>");
                        sb.Append("<label style='font-weight:bold;'>Require Recommendations:</label>");
                        sb.Append("<label>" + r["RequiredRecommendation"].ToBool() + "</label>");
                        sb.Append("</div>");
                        attendees += r["Attendee"];
                    }
                    else
                    {
                        attendees += "," + r["Attendee"];

                    }


                }

                sb.Append("<div>");
                sb.Append("<label style='font-weight:bold;'>Attendees:</label>");
                sb.Append("<label>" + attendees + "</label>");
                sb.Append("</div>");
            }
        }
        return sb.ToString();
    }

    [WebMethod]
    public List<MeetingAttributesModel> GetMeetignAttributes(long meetingId)
    {
        var lstmeetingAttributesInfo = new List<MeetingAttributesModel>();
        using (var db = new DBCommand("GetMeetignAttributes"))
        {
            db.AddParameter("@meetingId", meetingId);
            var ds = db.ExecuteDataSet().Tables[0].AsEnumerable();
            if (ds.Count() > 0)
            {
                var groups = ds.GroupBy(rec => rec["Symbol"].ToString());
                foreach (var group in groups)
                {
                    MeetingAttributesModel meeting = new MeetingAttributesModel();
                    meeting.Symbol = group.ElementAt(0).Field<string>("Symbol");
                    meeting.RequiredRecommendation = group.ElementAt(0).Field<bool>("RequiredRecommendation");
                    string attendees = string.Empty;
                    meeting.Securities = new List<long>();
                    for (int i = 0; i < group.Count(); i++)
                    {
                        if (i == 0)
                        {
                            attendees += group.ElementAt(i).Field<string>("Attendee");
                        }
                        else
                        {
                            attendees += "," + group.ElementAt(i).Field<string>("Attendee");

                        }


                    }
                    var secdb = db.ExecuteDataSet().Tables[1];
                    for(int i=0;i<secdb.Rows.Count;i++)
                    {

                        meeting.Securities.Add(secdb.Rows[i]["SecurityId"].ToLng());

                    }
                   
                    meeting.Attendee = attendees;
                    lstmeetingAttributesInfo.Add(meeting);

                }
            }


            //foreach (System.Data.DataRow r in ds.Rows)
            //{
            //    MeetingAttributesModel meeting = new MeetingAttributesModel();
            //    meeting.Symbol = r["Symbol"].ToString() ;
            //    meeting.RequiredRecommendation = r["RequiredRecommendation"].ToBool();
            //    meeting.Attendee = r["Attendee"].ToString();

            //    lstmeetingAttributesInfo.Add(meeting);

            //}
            //var groups = lstmeetingAttributesInfo.GroupBy(rec => rec.Symbol);


        }
        return lstmeetingAttributesInfo;

    }





    [WebMethod]
    public string GetICMeetingEmployeeList()
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("GetICMeetingAttendeeEmployees"))
        {
            //sb.Append("<option value=-1>***Choose Symbol***</option>");
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {

                sb.Append("<option value=\"" + r["NetworkLogin"].ToBlank() + "\">" + r["FullName"] + "</option>");
            }
        }
        return sb.ToString();
    }

    [WebMethod]
    public string GetICMeetingEmployeeListHtml()
    {
        var sb = new System.Text.StringBuilder();
        int numOfRows = 3;
        using (var db = new DBCommand("GetICMeetingAttendeeEmployees"))
        {
            sb.Append("<table>");
            int counter = 0;
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {
                if (counter == 0)
                {
                    sb.Append("<tr>");
                }

                sb.Append("<td> <input id='" + r["NetworkLogin"] + "' value='" + r["NetworkLogin"] + "' type='checkbox'  checked/></td><td>" + r["FullName"] + "</td>");
                if (counter == numOfRows)
                {
                    sb.Append("</tr>");
                }
                counter++;
                if (counter == numOfRows)
                {
                    counter = 0;
                }
            }
            if (counter < numOfRows && counter>0)//this is for filling the rows
            {
                for (int i = counter; i < numOfRows; i++)
                {
                    sb.Append("<td><td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
        }
        return sb.ToString();
    }


    [WebMethod]
    public MeetingModel GetMeetingInfo(long meetingId)
    {
        var meetingInfo = new MeetingModel();

        using (var db = new DBCommand("GetICMeetingsById"))
        {
            db.AddParameter("@meetingId", meetingId);


            var ds = db.ExecuteDataSet().Tables[0];

            if (ds.Rows.Count > 0)
            {
                foreach (System.Data.DataRow r in ds.Rows)
                {


                    meetingInfo.MeetingId = r["MeetingId"].ToLng();
                    meetingInfo.MeetingDate = r["MeetingDate"].ToShortDateFormatEx();
                    meetingInfo.MeetingDocLocation = r["MeetingDocLocation"].ToString();
                    meetingInfo.MeetingStartAt3PM = r["MeetingStartAt3PM"].ToBool();
                    meetingInfo.MeetingStartTime = r["MeetingStartTime"].ToString();
                    meetingInfo.MeetingEndTime = r["MeetingEndTime"].ToString();
                    meetingInfo.OrigNotes1PageInLength = r["OriginalNotes1PageInLength"].ToBool();
                    meetingInfo.OrigLengthOfNotes = r["OriginalLengthOfNotes"].ToIntEx();
                    meetingInfo.FinalLengthOfNotes = r["FinalLengthOfNotes"].ToIntEx();
                    meetingInfo.IsLocked = r["IsLocked"].ToBool();

                }
                meetingInfo.MeetingAttributes = GetMeetignAttributes(meetingId);
            }

        }
        return meetingInfo;
    }

    [WebMethod]
    public string GetPositionRecommendationsHtml(long meetingId,string meetingDate)
    {
        var sb = new System.Text.StringBuilder();

        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];
        //sb.Append("<table style='width:100%;border-collapse:separate;'>");
        using (var meetingdb = new DBCommand("GetTagIdsForMeeting"))
        {
            meetingdb.AddParameter("@meetingId", meetingId);
            var meetingds = meetingdb.ExecuteDataSet().Tables[0];

            foreach (System.Data.DataRow r in meetingds.Rows)
            {
                var tagId = r["TagID"].ToLng();
                sb.Append("<table style='width:100%;border-collapse:separate;margin-top:5px;'>");
                //var isMeetingLocked = r["IsLocked"].ToBool();
                using (var db = new DBCommand("GetPositionsForRecommendations"))
                {
                    db.AddParameter("@tagId", tagId);
                    var ds = db.ExecuteDataSet().Tables[0].AsEnumerable();
                    int recoCount = 0;
                    using (var recoCountdb = new DBCommand("GetICPositionRecommendationCount"))
                    {
                        recoCountdb.AddParameter("@tagId", tagId);
                        recoCountdb.AddParameter("@meetingId", meetingId);
                        recoCountdb.AddParameter ("@loginId", login);
                        recoCount= recoCountdb.ExecuteDataSet().Tables[0].Rows[0]["RecoCount"].ToInt();

                    }

                    if (ds.Count() > 0)
                    {
                        //sb.Append("<table style='width:100%;border-collapse:separate;'>");
                        var groups = ds.GroupBy(rec => rec["Symbol"].ToString());
                        foreach (var group in groups)
                        {
                            sb.Append("<tr><td  style='font-weight:bold; background-color:white;'>" + group.ElementAt(0).Field<string>("Symbol") + "</td></tr>");

                            sb.Append("<tr>");
                            sb.Append("<th>Date</th>");
                            int groupCount = group.Count();//this will be used for row fill up when populating history
                            var positionTracker = new Dictionary<int, int>();
                            for (int j = 0; j < group.Count(); j++)//this is for headers
                            {

                                sb.Append("<th>" + group.ElementAt(j).Field<string>("IssueDesc") + " Allocation" + "</th>");
                                positionTracker.Add(j,group.ElementAt(j).Field<int>("BondID"));

                            }
                            sb.Append("<th>Pros</th>");
                            sb.Append("<th>Cons</th>");
                            sb.Append("</tr>");
                            string recommendationIds = string.Empty;//this would be used
                            if (recoCount == 0 && !r["IsLocked"].ToBool())//only render if there is no recommendation  already made for this date
                         
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='vertical-align:top;'>" + meetingDate + "</td>");
                                //string recommendationIds = string.Empty;//this would be used

                                for (int j = 0; j < group.Count(); j++)//this is for first column
                                {
                                    if (j == 0)
                                    {
                                        recommendationIds += group.ElementAt(j).Field<long>("RecommendationId");
                                    }
                                    else
                                    {
                                        recommendationIds += "," + group.ElementAt(j).Field<long>("RecommendationId");
                                    }

                                    sb.Append("<td style='vertical-align:top;'>" + "<input type = 'text' style='width:40px;' id=" + "'alloc" + group.ElementAt(j).Field<long>("RecommendationId") + "'/></td>");
                                    //if (j == 0)
                                    //{
                                    //    sb.Append("<td><textarea id='pros" + group.ElementAt(j).Field<long>("RecommendationId")+"'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                                    //    sb.Append("<td><textarea id='cons" + group.ElementAt(j).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                                    //}

                                }
                                if (group.Count() > 0)
                                {
                                    sb.Append("<td><textarea id='pros" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                                    sb.Append("<td><textarea id='cons" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                                }
                                sb.Append("</tr>");

                                sb.Append("<tr><td style='background-color:white;'><input type='button' onclick=SaveRecommendations('" + recommendationIds + "')" + " value ='Save'/></td></tr>");//added by Venkat on 10/21/2016
                            }

                            using (var dbRecos = new DBCommand("GetPositionRecommendations"))
                            {
                                dbRecos.AddParameter("@tagId", tagId);
                                dbRecos.AddParameter("@loginId", login);
                                var dsRecos = dbRecos.ExecuteDataSet().Tables[0].AsEnumerable();
                                if (dsRecos.Count() > 0)
                                {
                                    var recoGroups = dsRecos.GroupBy(rec => rec["MeetingDate"].ToString());
                                   
                                    foreach (var recoGroup in recoGroups)//this loop is for populating allocations
                                    {
                                        int recoIdCounter = 0;
                                        sb.Append("<tr>");
                                        sb.Append("<td style='vertical-align:top;'><u onClick=GetHistory('"+ recoGroup.ElementAt(0).Field<long>("TagID") + "'"+","+"'"+ recoGroup.ElementAt(0).Field<long>("MeetingId") + "')>" + recoGroup.ElementAt(0).Field<DateTime>("MeetingDate").ToShortDateFormatEx() + "</u></td>");
                                        //for (int k = 0; k < recoGroup.Count(); k++)
                                        //{
                                        //   // if (k == positionTracker[recoGroup.ElementAt(k).Field<long>("SecurityId")])
                                        //    {
                                        //        sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(k).Field<double>("ConvertAllocation").ToString("0.00") + "</td>");
                                        //    }

                                        //}
                                        string recoIds = string.Empty;
                                        //for (int k = 0; k < recoGroup.Count(); k++)
                                        for (int k = 0; k < groupCount; k++)//aded by venkat on 10/21/2016
                                        {
                                           


                                            if (recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault()!=null)
                                            {
                                                if (recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<bool>("IsLocked"))//added by Venkat on 10/20/2016
                                                {
                                                    sb.Append("<td style='vertical-align:top;'>" + recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<double>("ConvertAllocation").ToString("0.00") + "</td>");
                                                }
                                                else
                                                {
                                                    sb.Append("<td style='vertical-align:top;'>" + "<input type = 'text' style='width:40px;' id=" + "'alloc"+ recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<long>("RecommendationId")+"'" + " value='"+ recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<double>("ConvertAllocation").ToString("0.00") + "'"+"/>"+ "</td>");
                                                }
                                                if (recoIdCounter == 0)
                                                {
                                                    recoIds += recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<long>("RecommendationId");
                                                }
                                                else
                                                {
                                                    // recoIds += "," + recoGroup.ElementAt(recoIdCounter).Field<long>("RecommendationId");
                                                    recoIds += "," + recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<long>("RecommendationId");
                                                }
                                                recoIdCounter = recoIdCounter + 1;
                                            }
                                            else
                                            {
                                                sb.Append("<td></td>");
                                            }

                                        }
                                      

                                        if (recoGroup.ElementAt(0).Field<bool>("IsLocked"))//added by Venkat on 10/20/2016
                                        {
                                            sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Pros") + "</td>");
                                            sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Cons") + "</td>");
                                        }
                                        else//added by Venkat on 10/20/2016
                                        {
                                            sb.Append("<td><textarea id='pros" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + recoGroup.ElementAt(0).Field<string>("Pros") + "</textarea></td>");
                                            sb.Append("<td><textarea id='cons" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + recoGroup.ElementAt(0).Field<string>("Cons") + "</textarea></td>");
                                        }
                                        sb.Append("</tr>");

                                        if (!recoGroup.ElementAt(0).Field<bool>("IsLocked"))//added by Venkat on 10/20/2016
                                        {
                                            sb.Append("<tr><td style='background-color:white;'><input type='button' onclick=SaveRecommendations('" + recoIds + "')" + " value ='Save'/></td></tr>");
                                        }
                                    }



                                }
                            }

                            //if (recoCount == 0 && !r["IsLocked"].ToBool())//only render if there is no recommendation  already made for this date
                            //{
                            //    sb.Append("<tr><td style='background-color:white;'><input type='button' onclick=SaveRecommendations('" + recommendationIds + "')" + " value ='Save'/></td></tr>");
                            //}

                        }

                    }
                    // sb.Append("</table>");

                }
                sb.Append("</table>");
            }
        }
       // sb.Append("</table>");
        return sb.ToString();
    }



    [WebMethod]
    public void InsertPositionRecommendations(List<PositionRecommendation> recommendations)
    {
        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];
        if (recommendations != null)
        {
            for (int i = 0; i < recommendations.Count(); i++)
            {
                using (var db = new DBCommand("InsertPositionRecommendations"))
                {
                    db.AddParameter("@tagSecurityId", recommendations[i].TagSecurityId);
                    db.AddParameter("@pros", recommendations[i].Pros);
                    db.AddParameter("@cons", recommendations[i].Cons);
                    db.AddParameter("@convertAllocation", recommendations[i].ConvertAllocation);
                    db.AddParameter("@login", login);

                    db.ExecuteNonQuery();

                }
            }
        }

    }

    [WebMethod]
    public string GetPositionRecommendationsHistoryHtml(long tagId)
    {
        var sb = new System.Text.StringBuilder();
       
        sb.Append("<table style='width:100%;border-collapse:separate;'>");
        using (var db = new DBCommand("GetPositionsForRecommendations"))
        {
            db.AddParameter("@tagId", tagId);
            var ds = db.ExecuteDataSet().Tables[0].AsEnumerable();


            if (ds.Count() > 0)
            {
                //sb.Append("<table style='width:100%;border-collapse:separate;'>");
                var groups = ds.GroupBy(rec => rec["Symbol"].ToString());
                foreach (var group in groups)
                {
                    sb.Append("<tr><td  style='font-weight:bold; background-color:white;'>" + group.ElementAt(0).Field<string>("Symbol") + "</td></tr>");

                    sb.Append("<tr>");
                    //sb.Append("<th>Date</th>");
                    int groupCount = group.Count();//this will be used for row fill up when populating history
                    var positionTracker = new Dictionary<int, int>();
                    for (int j = 0; j < group.Count(); j++)//this is for headers
                    {

                        sb.Append("<th>" + group.ElementAt(j).Field<string>("IssueDesc") + " Allocation" + "</th>");
                        positionTracker.Add(j, group.ElementAt(j).Field<int>("BondID"));

                    }
                    sb.Append("<th>Pros</th>");
                    sb.Append("<th>Cons</th>");
                    sb.Append("<th>LoginId</th>");
                    sb.Append("</tr>");


                    //sb.Append("<tr>");
                    ////sb.Append("<td style='vertical-align:top;'>" + meetingDate + "</td>");
                    //string recommendationIds = string.Empty;//this would be used

                    //for (int j = 0; j < group.Count(); j++)//this is for first column
                    //{
                    //    if (j == 0)
                    //    {
                    //        recommendationIds += group.ElementAt(j).Field<long>("RecommendationId");
                    //    }
                    //    else
                    //    {
                    //        recommendationIds += "," + group.ElementAt(j).Field<long>("RecommendationId");
                    //    }

                    //    sb.Append("<td style='vertical-align:top;'>" + "<input type = 'text' id=" + "'alloc" + group.ElementAt(j).Field<long>("RecommendationId") + "'/></td>");
                    //    //if (j == 0)
                    //    //{
                    //    //    sb.Append("<td><textarea id='pros" + group.ElementAt(j).Field<long>("RecommendationId")+"'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                    //    //    sb.Append("<td><textarea id='cons" + group.ElementAt(j).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                    //    //}

                    //}
                    //if (group.Count() > 0)
                    //{
                    //    sb.Append("<td><textarea id='pros" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                    //    sb.Append("<td><textarea id='cons" + group.ElementAt(0).Field<long>("RecommendationId") + "'" + "style='width:700px;height:100px;'>" + "</textarea></td>");
                    //}
                    //sb.Append("</tr>");

                    using (var dbRecos = new DBCommand("GetPositionsRecommendationHistory"))
                    {
                        dbRecos.AddParameter("@tagId", tagId);
                        //dbRecos.AddParameter("@loginId", login);
                        var dsRecos = dbRecos.ExecuteDataSet().Tables[0].AsEnumerable();
                        if (dsRecos.Count() > 0)
                        {
                            //var recoGroups = dsRecos.GroupBy(rec => rec["MeetingDate"].ToString());
                            var recoGroups = dsRecos.GroupBy(rec => rec["loginId"].ToString());
                            foreach (var recoGroup in recoGroups)
                            {
                                sb.Append("<tr>");
                                //sb.Append("<td style='vertical-align:top;'><u onClick=GetHistory('" + recoGroup.ElementAt(0).Field<long>("TagID") + "'" + "," + "'" + recoGroup.ElementAt(0).Field<long>("MeetingId") + "')>" + recoGroup.ElementAt(0).Field<DateTime>("MeetingDate").ToShortDateFormatEx() + "</u></td>");
                                //for (int k = 0; k < recoGroup.Count(); k++)
                                //{
                                //   // if (k == positionTracker[recoGroup.ElementAt(k).Field<long>("SecurityId")])
                                //    {
                                //        sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(k).Field<double>("ConvertAllocation").ToString("0.00") + "</td>");
                                //    }

                                //}
                                for (int k = 0; k < groupCount; k++)
                                {
                                    if (recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault() != null)
                                    {
                                        sb.Append("<td style='vertical-align:top;'>" + recoGroup.Where(rec => rec.Field<long>("SecurityId") == positionTracker[k]).FirstOrDefault().Field<double>("ConvertAllocation").ToString("0.00") + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                    }

                                }
                                //for (int i= recoGroup.Count();i<groupCount;i++)
                                //{

                                //    sb.Append("<td></td>");

                                //}

                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Pros") + "</td>");
                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Cons") + "</td>");
                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("loginId") + "</td>");
                                sb.Append("</tr>");
                            }



                        }
                    }


                  

                }

            }
            // sb.Append("</table>");

        }
        sb.Append("</table>");

        return sb.ToString();
    }


    [WebMethod]
    public string GetPositionRecommendationsHistoryHtmlEmail(long tagId,string addendumText)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("<div  style='height:500px;overflow:auto;'>");
        sb.Append(addendumText);
        sb.Append("<style>");
        sb.Append("table, th, td {");
        sb.Append("border: 1px solid black;");
        sb.Append("border-collapse: collapse;");
        sb.Append("}");
        sb.Append("</style>");
    
       // sb.Append("<table  style='width:70%;'>");
        using (var db = new DBCommand("GetPositionsForRecommendations"))
        {
            db.AddParameter("@tagId", tagId);
            //var ds = db.ExecuteDataSet().Tables[0].AsEnumerable();
            var ds = db.ExecuteDataSet().Tables[0];


            if (ds.Rows.Count > 0)
            {
                //sb.Append("<table style='width:100%;border-collapse:separate;'>");
               // var groups = ds.GroupBy(rec => rec["Symbol"].ToString());
                //foreach (var group in groups)
                foreach (System.Data.DataRow r in ds.Rows)
                {
                    sb.Append("<div style='margin-top:10px;'>");

                   sb.Append("<table  style='width:80%;padding-top:10px;'>");
                    sb.Append("<tr><td  style='font-weight:bold; background-color:white;'>" + r["IssueDesc"] + "</td></tr>");

                  

                  

                    using (var dbRecos = new DBCommand("ICMeeting_GetAllocRecommendationsForSymbol"))
                    {
                        //dbRecos.AddParameter("@securityId", tagId);
                        dbRecos.AddParameter("@securityId", r["BondID"]);

                        var dsRecos = dbRecos.ExecuteDataSet().Tables[0].AsEnumerable();
                        if (dsRecos.Count() > 0)
                        {
                            //var recoGroups = dsRecos.GroupBy(rec => rec["MeetingDate"].ToString());
                            var recoGroups = dsRecos.GroupBy(rec => rec["loginId"].ToString()).OrderByDescending(rec=>rec.Count());
                            int maxRecoGroupCount = recoGroups.ElementAt(0).Count();//we are getting the count here so that we could use it in filling the empty allocations later and this will always be the max as we are sorting by desc count at the top
                            var positionTracker = new Dictionary<int, string>();
                            sb.Append("<th style='text-align:left;'>PM/Analyst</th>");
                            for (int i=0;i< maxRecoGroupCount ;i++)//this is for the headers
                            {
                                sb.Append("<th style='text-align:left;'>" + recoGroups.ElementAt(0).ElementAt(i).Field<DateTime>("MeetingDate").ToShortDateString()+"Convert Allocation"  + "</th>");
                                positionTracker.Add(i, recoGroups.ElementAt(0).ElementAt(i).Field<DateTime>("MeetingDate").ToString());


                            }
                            sb.Append("<th style='text-align:left;'>Pros</th>");
                            sb.Append("<th style='text-align:left;'>Cons</th>");
                            foreach (var recoGroup in recoGroups)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("loginId") + "</td>");
                               
                                for (int k = 0; k < maxRecoGroupCount; k++)
                                {
                                    if (recoGroup.Where(rec => rec.Field<DateTime>("MeetingDate").ToString() == positionTracker[k]).FirstOrDefault() != null)
                                    {
                                        sb.Append("<td style='vertical-align:top;'>" + recoGroup.Where(rec => rec.Field<DateTime>("MeetingDate").ToString() == positionTracker[k]).FirstOrDefault().Field<double>("ConvertAllocation").ToString("0.00") + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                    }

                                }
                            

                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Pros") + "</td>");
                                sb.Append("<td style='vertical-align:top;'>" + recoGroup.ElementAt(0).Field<string>("Cons") + "</td>");
                               
                                sb.Append("</tr>");
                            }



                        }
                    }



                    sb.Append("</table>");
                    sb.Append("</div>");
                }

            }
            // sb.Append("</table>");

        }
       // sb.Append("</table>");
        sb.Append("</div>");

        return sb.ToString();
    }
    [WebMethod]
    public void SendPositionRecommendationsEmail(long tagId,string addendumText)
    {
        string message = GetPositionRecommendationsHistoryHtmlEmail(tagId, addendumText);
        using (var db = new DBCommand("SendICMeetingNotesEmail"))
        {
            db.AddParameter("@to","vkanakamedala@acmewidget.com");
            db.AddParameter("@subject", "Position Recommendations");
            db.AddParameter("@message", message);
            db.ExecuteNonQuery();

        }




    }

    [WebMethod]
    public void InsertNonAttendeeReason(long tagId, string reason)
    {
        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];
        using (var db = new DBCommand("InsertICMeetingNonAttendeeReason"))
        {
            db.AddParameter("@tagId", tagId);
            db.AddParameter("@attendee", login);
            db.AddParameter("@reasonNotAttended", reason);
            db.ExecuteNonQuery();

        }




    }


    [WebMethod]
    public string GetSymbolListForMeeting(string meetingDate)
    {
        var sb = new System.Text.StringBuilder();
        using (var db = new DBCommand("ICMeeting_GetSymbolListForMeeting"))
        {
            db.AddParameter("@meetingDate", meetingDate);
            // sb.Append("<option value=-1>***Choose Company***</option>");
            foreach (System.Data.DataRow r in db.ExecuteDataSet().Tables[0].Rows)
            {

                sb.Append("<option value=\"" + r["TagId"].ToBlank() + "\">" + r["Symbol"] + "</option>");
            }
        }
        return sb.ToString();
    }


    [WebMethod]
    public void SendICMeetingNotesEmail(long meetingId,string meetingDate,string meetingNotesDoc)
    {
        using (var meetingdb = new DBCommand("ICMeeting_GetICMeetingId"))
        {
            meetingdb.AddParameter("@meetingDate", meetingDate);
            var meetingds = meetingdb.ExecuteDataSet().Tables[0];

            foreach (System.Data.DataRow r in meetingds.Rows)
            {
                meetingId = r["MeetingId"].ToLng();
            }
        }


       var message= new System.Text.StringBuilder();
        message.Append("<table>");
        message.Append("<tr><td>Position Recommendation Link: "+ "http://localhost:53955/addpositionrecommendations.aspx?meetingId="+ meetingId + "&meetingDate="+meetingDate+"</td></tr>");
        message.Append("<tr><td>Meeting Notes Link:" + "<a href='" + meetingNotesDoc + "'>" + meetingNotesDoc + "</a>" + "</td></tr>");
        message.Append("<tr><td>Notes Reviewed Confirmation: " + "http://localhost:53955/ReviewedMeetingNotes.aspx?meetingId=" + meetingId+ "</td></tr>");
        message.Append("</table>");

        using (var db = new DBCommand("SendICMeetingNotesEmail"))//need to change stored proc name here to ICMeeting_SendICMeetingNotesNotificationEmail
        {
            db.AddParameter("@to", "vkanakamedala@acmewidget.com");
            db.AddParameter("@subject", "IC Meeting "+meetingDate);
            db.AddParameter("@message", message.ToString());
            db.ExecuteNonQuery();

        }

        message.Clear();
        message.Append("<table>");
        message.Append("<tr><td>Please enter a reason for not attending meeting: " + "http://localhost:53955/MeetingNonAttendeeReason.aspx?meetingId=" + meetingId + "&meetingDate=" + meetingDate + "</td></tr>");
      
        message.Append("</table>");
        using (var db = new DBCommand("SendICMeetingNotesEmail"))//need to change stored proc name here to ICMeeting_SendICMeetingNotesNonAttendeeEmail
        {
            db.AddParameter("@to", "vkanakamedala@acmewidget.com");
            db.AddParameter("@subject", "IC Meeting "+meetingDate);
            db.AddParameter("@message", message.ToString());
            db.ExecuteNonQuery();

        }

    }


    [WebMethod]
    public void UpdateMeetingNotesChecked(long meetingId)
    {
        string login = HttpContext.Current.Request.ServerVariables["Auth_User"];
        if (login.IndexOf("\\") != -1) login = login.Split('\\')[1];
        using (var db = new DBCommand("ICMeeting_UpdateMeetingNotesChecked"))
        {
            db.AddParameter("@meetingId", meetingId);
            db.AddParameter("@attendee", login);
         
            db.ExecuteNonQuery();

        }




    }
}




