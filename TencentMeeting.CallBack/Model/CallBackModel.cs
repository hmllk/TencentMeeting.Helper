using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TencentMeeting.CallBack.Model
{
    public class CallBackModel
    {
        [JsonProperty("operate_time")] public long OperatorTime { get; set; }

        [JsonProperty("operator")] public Operator Operator { get; set; }
    }

    public class Operator
    {
        [JsonProperty("userid")] public string UserId { get; set; }
        [JsonProperty("open_id")] public string OpenId { get; set; }
        [JsonProperty("uuid")] public string UuId { get; set; }
        [JsonProperty("ms_open_id")] public string MsOpenId { get; set; }
        [JsonProperty("instance_id")] public int InstanceId { get; set; }
        [JsonProperty("user_name")] public string UserName { get; set; }
        [JsonProperty("pstn_number")] public string PstnNumber { get; set; }
    }
}
