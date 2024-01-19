using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TencentMeeting.Api.Model.Meeting
{
    public class CreateMeetingRequest : BaseRequest
    {
        /**
     * 调用方用于标示用户的唯一 ID（企业内部请使用企业唯一用户标识；OAuth2.0 鉴权用户请使用 openId）。
     * 企业唯一用户标识说明：
     * 1. 企业对接 SSO 时使用的员工唯一标识 ID；
     * 2. 企业调用创建用户接口时传递的 userid 参数
     * 必传
     */
        public string userid { get; set; }

        /**
     * 用户的终端设备类型：
     * 1：PC
     * 2：Mac
     * 3：Android
     * 4：iOS
     * 5：Web
     * 6：iPad
     * 7：Android Pad
     * 8：小程序
     * 创建会议时 userid 对应的设备类型，不影响入会时使用的设备类型，缺省可填1
     * 必传
     */
        public int instanceid { get; set; } = 10;

        /**
     * 会议主题 必传
     */
        public string subject { get; set; }

        /**
   * 会议类型：
   * 0：预约会议
   * 1：快速会议
   * 必传
   */
        public int type { get; set; } = 0;

        /**
     * 会议指定主持人的用户 ID，如果无指定，主持人将被设定为参数 userid 的用户，即 API 调用者。
     * 注意：仅腾讯会议商业版和企业版可指定主持人
     */
        public List<User> hosts { get; set; }

        /**
     * 会议嘉宾列表，会议嘉宾不受会议密码和等候室的限制
     */
        public List<Guest> guests { get; set; }

        /**
   * 会议邀请的参会者，可为空。
   * 注意：仅腾讯会议商业版和企业版可邀请参会者
   */
        public List<User> invitees { get; set; }

        /**
     * 会议开始时间戳（单位秒）必传
     */
        public string start_time { get; set; }

        /**
     * 会议结束时间戳（单位秒） 必传
     */
        public string end_time { get; set; }

        /**
     * 会议密码（4~6位数字），可不填
     */
        public string password { get; set; }

        /**
     * 会议媒体参数配置
     */
        public MeetingSetting settings { get; set; }

        /**
    * 默认值为0。
    * 0：普通会议
    * 1：周期性会议
    * 说明：周期性会议时 type 不能为快速会议。
    */
        public int meeting_type { get; set; } = 0;

        //     /**
        // * 周期性会议配置。
        // */
        //     public RecurringRule recurring_rule { get; set; }

        /**
     * 是否开启直播。
     */
        public bool enable_live { get; set; }

        //    /**
        // * 直播配置。
        // */
        //    public LiveConfig live_config { get; set; }

        /**
    * 是否允许成员上传文档，默认为允许
    */
        public bool enable_doc_upload_permission { get; set; }

        // 该参数仅提供给支持混合云的企业可见，默认值为0。
        // 0：公网会议
        // 1：专网会议
        public int media_set_type { get; set; }

        // 同声传译开关，默认值为false。
        // false：不开启
        // true：开启同声传译
        public bool enable_interpreter { get; set; }

        // 是否激活报名。
        public bool enable_enroll { get; set; }

        // 是否开启主持人密钥，默认为false。
        // true：开启
        // false：关闭
        public bool enable_host_key { get; set; }

        // 主持人密钥，仅支持6位数字。
        // 如开启主持人密钥后没有填写此项，将自动分配一个6位数字的密钥。
        public string host_key { get; set; }

        // 会议是否同步至企业微信，该字段仅支持创建会议时设置，创建后无法修改。该配置仅支持与企业微信关联的企业。
        // true：同步，默认同步（目前仅支持同步到创建者企业微信，不支持会议邀请者）
        // false：不同步
        public bool sync_to_wework { get; set; }

        // 时区
        public string time_zone { get; set; }

        // 会议地点。最长支持18个汉字或36个英文字母。
        public string location { get; set; }

    }

    public class User
    {
        /**
     * 用户 ID（企业内部请使用企业唯一用户标识；OAuth2.0 鉴权用户请使用 openId）
     */
        [JsonProperty("userid")]public string UserId { get; set; }

        /**
     * 用户是否匿名入会，缺省为 false，不匿名。
     * true：匿名
     * false：不匿名
     */
        [JsonProperty("is_anonymous")]public bool IsAnonymous { get; set; }

        /**
    * 用户匿名字符串。如果字段“is_anonymous”设置为“true”，但是无指定匿名字符串, 会议将分配缺省名称，
    * 例如 “会议用户xxxx”，其中“xxxx”为随机数字。
    */
        [JsonProperty("nick_name")]public string NickName { get; set; }
    }

    public class MeetingSetting
    {
        [JsonProperty("mute_enable_type_join")]
        public int MuteEnableTypeJoin { get; set; } = 2;

        [JsonProperty("mute_enable_join")]
        public bool MuteEnableJoin { get; set; } = true;

        [JsonProperty("allow_in_before_host")]
        public bool AllowInBeforeHost { get; set; }

        [JsonProperty("auto_in_waiting_room")]
        public bool AutoInWaitingRoom { get; set; }

        [JsonProperty("allow_screen_shared_watermark")]
        public bool AllowScreenSharedWatermark { get; set; } = false;

        [JsonProperty("water_mark_type")]
        public int WaterMarkType { get; set; } = 0;

        [JsonProperty("only_enterprise_user_allowed")]
        public bool OnlyEnterpriseUserAllowed { get; set; } = false;

        [JsonProperty("auto_record_type")]
        public string AutoRecordType { get; set; } = "none";
    }

    public class Guest
    {
        [JsonProperty("area")] public string Area { get; set; }
        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }
        [JsonProperty("guest_name")] public string GuestName { get; set; }
    }
}
