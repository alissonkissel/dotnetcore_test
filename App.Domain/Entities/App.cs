using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace App.Domain.Entities
{
    public class App
    {
        public int Application { get; set; }
        public string Url { get; set; }
        public string PathLocal { get; set; }
        public bool? DebuggingMode { get; set; }
    }
}
