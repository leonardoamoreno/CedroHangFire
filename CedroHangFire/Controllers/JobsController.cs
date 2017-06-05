using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using CedroHangFire.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Hangfire.Common;
using System.Reflection;
using Hangfire;
using CedroHangFire.Util;

namespace CedroHangFire.Controllers
{
    public class JobsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public JobsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Jobs
        public ActionResult Index()
        {

            return View();
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Upload da DLL que contém o metódo principal OnExecute()
                    var file = model.DllFile;
                    var parsedContentDisposition =
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", Path.GetFileNameWithoutExtension(file.FileName));

                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }

                    var filename = Path.Combine(path, parsedContentDisposition.FileName.Trim('"'));

                    using (var stream = System.IO.File.OpenWrite(filename))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //Upload das dependencias da DLL
                    var files = model.DependenciesFile;

                    if (files != null && files.Count > 0)
                    {
                        foreach (var fileDep in files)
                        {
                            if (fileDep.Length > 0)
                            {
                                using (var fileStream = new FileStream(Path.Combine(path, fileDep.FileName), FileMode.Create))
                                {
                                    await fileDep.CopyToAsync(fileStream);
                                }
                            }
                        }
                    }

                    var jobId = Path.GetFileNameWithoutExtension(file.FileName);

                    if (model.TypeJob == EnumTypeJob.RunScheduleRecurring)
                    {
                        RecurringJob.AddOrUpdate<ExecuteJobs>(jobId, (a) => a.Execute(Path.GetFileNameWithoutExtension(file.FileName), filename), model.CronExpression);
                    }
                    else if (model.TypeJob == EnumTypeJob.RunImmediately)
                    {
                        BackgroundJob.Enqueue<ExecuteJobs>((a) => a.Execute(Path.GetFileNameWithoutExtension(file.FileName), filename));

                    }
                    else if (model.TypeJob == EnumTypeJob.RunSchedule)
                    {
                        BackgroundJob.Schedule<ExecuteJobs>((a) => a.Execute(Path.GetFileNameWithoutExtension(file.FileName),filename), (DateTime)model.DateTime);
                    }

                }
                else
                {
                    return View(model);
                }

                return RedirectToAction("","hangfire");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }


}