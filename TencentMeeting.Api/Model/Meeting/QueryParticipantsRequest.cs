using System;
using System.Collections.Generic;
using System.Text;

namespace TencentMeeting.Api.Model.Meeting
{
    public class QueryParticipantsRequest : BaseRequest
    {
        // 有效的会议 ID。
        public string meetingId { get; set; }

        // 周期性会议子会议 ID。说明：可通过查询用户的会议列表、查询会议接口获取返回的子会议 ID，即 current_sub_meeting_id；如果是周期性会议，此参数必传。
        public string sub_meeting_id { get; set; }

        // 操作者 ID。operator_id 必须与 operator_id_type 配合使用。根据 operator_id_type 的值，operator_id 代表不同类型。
        // 说明：userid 字段和 operator_id 字段二者必填一项。若两者都填，以 operator_id 字段为准。
        public string operator_id { get; set; }

        // 操作者 ID 的类型：
        // 3：rooms_id
        //     说明：当前仅支持 rooms_id。如操作者为企业内 userid 或 openId，请使用 userid 字段。
        public int operator_id_type { get; set; }

        //会议创建者的用户 ID（企业内部请使用企业唯一用户标识；OAuth 2.0鉴权用户请使用 openId）。
        public string userid { get; set; }

        public int? pos { get; set; }

        public int size { get; set; } = 100;

        public int? start_time { get; set; }

        public int? end_time { get; set; }
    }
}
