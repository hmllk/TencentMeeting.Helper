using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TencentMeeting.Util
{
    public static class TencentMeetingSignHelper
    {
        /**
    * 生成签名
    *
    * @param secretId 邮件下发的secret_id
    * @param secretKey 邮件下发的secret_key
    * @param httpMethod http请求方法 GET/POST/PUT等
    * @param headerNonce X-TC-Nonce请求头，随机数
    * @param headerTimestamp X-TC-Timestamp请求头，当前时间的秒级时间戳
    * @param requestUri 请求uri，eg：/v1/meetings
    * @param requestBody 请求体，没有的设为空串
    * @return 签名，需要设置在请求头X-TC-Signature中
      */
        public static string Sign(string secretId, string secretKey, string httpMethod, string headerNonce,
            string headerTimestamp, string requestUri, string requestBody)
        {
            string tobeSig =
                httpMethod + "\nX-TC-Key=" + secretId + "&X-TC-Nonce=" + headerNonce + "&X-TC-Timestamp=" +
                headerTimestamp + "\n" + requestUri + "\n" + requestBody;
            var encoding = System.Text.Encoding.UTF8;
            byte[] keyByte = encoding.GetBytes(secretKey);
            byte[] messageBytes = encoding.GetBytes(tobeSig);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                var b16 = BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
                return Convert.ToBase64String(encoding.GetBytes(b16));
            }
        }

        /// <summary>
        /// 回调签名认证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CalSignature(string token, string timestamp, string nonce, string data)
        {
            // string[] arr = {token, timestamp, nonce, data};
            // Array.Sort(arr);
            var arr = new List<string>() { token, timestamp, nonce, data };
            arr.Sort(string.CompareOrdinal);

            var toSign = new StringBuilder();
            for (int i = 0; i < arr.Count; i++)
            {
                toSign.Append(arr[i]);
            }

            var buffer = Encoding.UTF8.GetBytes(toSign.ToString());
            var signData = SHA1.Create().ComputeHash(buffer);
            var sb = new StringBuilder();
            foreach (var t in signData)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }


        public static string GetTimestamp()
        {
            // 获取当前 UTC 时间
            DateTime currentTime = DateTime.UtcNow;
            // 计算与 UNIX 纪元的时间差
            TimeSpan timeSpan = currentTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // 获取总秒数作为 UNIX 时间戳
            long unixTimestampInSeconds = (long)timeSpan.TotalSeconds;
            var timestamp = unixTimestampInSeconds.ToString();
            return timestamp;
        }
    }
}
