﻿using System;
using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class InfoController : ControllerBase
    {
        public ActionResult DateTimeUtc()
        {
            return Content(DateTime.UtcNow.ToString("O"), "text/plain");
        }
	}
}