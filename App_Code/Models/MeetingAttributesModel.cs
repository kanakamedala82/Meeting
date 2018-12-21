using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Meeting
/// </summary>
public class MeetingAttributesModel
{
    public string Symbol { get; set; }

    public bool RequiredRecommendation { get; set; }
    public string Attendee { get; set; }

    public List<string> Attendees { get; set; }

    public List<string> NonAttendees { get; set; }

    public List<long> Securities { get; set; }






}