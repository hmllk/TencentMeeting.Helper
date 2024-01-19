using System;
using System.Collections.Generic;
using System.Text;

namespace TencentMeeting.Api.Model.Meeting
{
    public class UpdateMeetingRequest: CreateMeetingRequest
    {
        // 有效的会议 ID。
        public string meetingId { get; set; }
    }
}
