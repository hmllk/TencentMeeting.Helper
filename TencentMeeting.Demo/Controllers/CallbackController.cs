using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using TencentMeeting.CallBack.Handle;
using TencentMeeting.Util;

namespace TencentMeeting.Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallbackController : ControllerBase
    {
        private readonly ILogger<CallbackController> _logger;
        private readonly IEnumerable<ICallbackHandle> _callbackHandles;

        public CallbackController(ILogger<CallbackController> logger, IEnumerable<ICallbackHandle> callbackHandles)
        {
            _logger = logger;
            _callbackHandles = callbackHandles;
        }

        /// <summary>
        /// 腾讯会议回调Check
        /// </summary>
        /// <returns></returns>
        [HttpGet("notice/wemeet")]
        [AllowAnonymous]
        public async Task<IActionResult> WeMeetNoticeCheckAsync(string check_str)
        {
            _logger.LogInformation("腾讯会议回调校验开始");
            _logger.LogInformation($"腾讯会议回调校验,返回check_str：{check_str}");

            var timestamp = HttpContext.Request.Headers["timestamp"].ToString();
            var nonce = HttpContext.Request.Headers["nonce"].ToString();
            var signature = HttpContext.Request.Headers["signature"].ToString();

            _logger.LogInformation($"腾讯会议回调校验,返回timestamp：{timestamp}");
            _logger.LogInformation($"腾讯会议回调校验,返回nonce：{nonce}");
            _logger.LogInformation($"腾讯会议回调校验,返回signature：{signature}");

            var str = System.Net.WebUtility.UrlDecode(check_str);

            var token = await _tencentMeetingOptionsService.GetTokenAsync();

            var sign = TencentMeetingSignHelper.CalSignature(token, timestamp, nonce, str);
            _logger.LogInformation($"腾讯会议回调校验,sign：{sign}");
            if (signature != sign)
            {
                throw new ApplicationException(
                    $"校验签名失败timestamp:{timestamp},nonce:{nonce},signature：{signature}，sign：{sign}");
            }

            var result = EncryptHelper.DecodeBase64(check_str);
            ;
            _logger.LogInformation($"腾讯会议回调校验结束{result}");

            return Content(result);
        }

        /// <summary>
        /// 腾讯会议回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("notice/wemeet")]
        [AllowAnonymous]
        public async Task<IActionResult> WeMeetNoticeAsync()
        {
            _logger.LogInformation("腾讯会议回调开始");
            var streamReader = new StreamReader(Request.HttpContext.Request.Body, Encoding.UTF8);
            var bodyContent = await streamReader.ReadToEndAsync();
            _logger.LogInformation($"腾讯会议回调解析,内容{bodyContent}");

            if (string.IsNullOrEmpty(bodyContent))
            {
                throw new ApplicationException("腾讯会议回调返回数据格式异常");
            }

            var bodyJson = JObject.Parse(bodyContent);
            var data = bodyJson.Value<string>("data");

            var timestamp = HttpContext.Request.Headers["timestamp"].ToString();
            var nonce = HttpContext.Request.Headers["nonce"].ToString();
            var signature = HttpContext.Request.Headers["signature"].ToString();
            _logger.LogInformation($"腾讯会议回调解析,返回timestamp：{timestamp}");
            _logger.LogInformation($"腾讯会议回调解析,返回nonce：{nonce}");
            _logger.LogInformation($"腾讯会议回调解析,返回signature：{signature}");

            var token = await _tencentMeetingOptionsService.GetTokenAsync();

            var sign = TencentMeetingSignHelper.CalSignature(token, timestamp, nonce, data);

            if (signature != sign)
            {
                throw new ApplicationException(
                    $"校验签名失败timestamp:{timestamp},nonce:{nonce},signature：{signature}，sign：{sign}");
            }

            //处理逻辑，base64解码 data
            var message = EncryptHelper.DecodeBase64(data);
            _logger.LogInformation($"腾讯会议回调解析,base64解码signature：{message}");

            //HandleMessage
            var body = JObject.Parse(message);
            var eValue = body.Value<string>("event");
            var handle = _callbackHandles.FirstOrDefault(a => a.Event == eValue);
            if (handle == null)
            {
                throw new ApplicationException($"missing callback handle:{eValue}");
            }

            var payload = body.SelectToken("payload")?.ToString();
            _logger.LogInformation($"腾讯会议回调解析,payload：{payload}");

            await handle.HandleAsync(payload);

            _logger.LogInformation("腾讯会议回调结束");
            var returnStr = "successfully received callback";

            return Content(returnStr);
        }
    }
}
