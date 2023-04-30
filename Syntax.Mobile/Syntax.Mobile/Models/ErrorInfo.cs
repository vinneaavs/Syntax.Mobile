﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax.Mobile.Models
{
    public class ErrorInfo
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
