using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TencentMeeting.CallBack.Handle
{
    public interface ICallbackHandle
    {
        public string Event { get; }

        public Task<bool> HandleAsync(string message);
    }
}
