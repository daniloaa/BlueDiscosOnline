using BlueDiscosOnline.Common.Enumerators;
using System;
using System.Collections.Generic;

namespace BlueDiscosOnline.Common.Containers
{
    public class RequestResult
    {
        public RequestResult()
        {
            Status = StatusResult.Success;
            Messages = new List<Message>();
        }
        
        public RequestResult(StatusResult status, object data)
            : this()
        {
            Status = status;
            Data = data;
        }

        public RequestResult(StatusResult status, params Message[] messages)
            : this()
        {
            Status = status;

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
        }

        public RequestResult(StatusResult status, object data, params Message[] messages)
           : this()
        {
            Status = status;
            Data = data;

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
        }

        public StatusResult Status { get; set; }

        public Object Data { get; set; }

        public List<Message> Messages { get; set; }

        public Exception Exception { get; set; }
    }
}
