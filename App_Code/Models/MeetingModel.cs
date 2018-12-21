using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MeetingModel
/// </summary>
public class MeetingModel
{
    public long MeetingId { get; set; }
    public string MeetingDate { get; set; }
    public string MeetingDocLocation { get; set; }
    public bool MeetingStartAt3PM { get; set; }
    public string MeetingStartTime { get; set; }
    public string MeetingEndTime{get; set;}
    public bool OrigNotes1PageInLength { get; set; }
    public int? OrigLengthOfNotes { get; set; }
    public int? FinalLengthOfNotes { get; set; }

    public bool? IsLocked { get; set; }

    public List<MeetingAttributesModel> MeetingAttributes {get; set;}


}