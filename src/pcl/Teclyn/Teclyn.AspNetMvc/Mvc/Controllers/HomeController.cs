﻿using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Teclyn.AspNetMvc.Mvc.Models;
using Teclyn.Core.Ioc;
using Teclyn.Core.Storage;

namespace Teclyn.AspNetMvc.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public RepositoryService RepositoryService { get; set; }

        public ActionResult Index()
        {
            return Content("NYI");
        }

        public ActionResult Info()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            var model = new HomeInfoModel
            {
                Aggregates = this.RepositoryService.Aggregates.Select(agg => new AggregateInfoModel
                {
                    AggregateType = agg.AggregateType.ToString(),
                    ImplementationType = agg.ImplementationType.ToString(),
                }).ToArray(),
                TeclynVersion = version,
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}