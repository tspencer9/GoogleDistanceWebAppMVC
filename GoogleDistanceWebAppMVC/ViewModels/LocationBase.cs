using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleDistanceWebAppMVC.ViewModels
{
    public class RootLocationBase
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string place_id { get; set; }
    }
}