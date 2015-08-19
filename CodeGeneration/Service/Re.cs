using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.Service
{
    public class BaseRequest
    { }

    public class BaseResponse
    {
        public bool IsSuccesss { get; set; }
        public string Message { get; set; }
    }
}
