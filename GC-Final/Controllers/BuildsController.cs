﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GC_Final.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Configuration;
using System.Reflection;

namespace GC_Final.Controllers
{
    public class RequireParameterAttribute : ActionMethodSelectorAttribute
    {
        public string ValueName { set; get; }
        public RequireParameterAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }
    }

    [Authorize]
    public class BuildsController : Controller
    {
        // GET: Builds
        public ActionResult Create()
        {
            ViewBag.GPUs = GetPartData(GetParts("GPU"));
            ViewBag.CPUs = GetPartData(GetParts("CPU"));
            ViewBag.Motherboards = GetPartData(GetParts("Motherboard"));
            ViewBag.PSUs = GetPartData(GetParts("PSU"));
            ViewBag.RAMs = GetPartData(GetParts("RAM"));
            ViewBag.Cases = GetPartData(GetParts("Computer+Case"));
            return View();
        }

        public ActionResult MoreParts(string partType)
        {
            ViewBag.PartSearch = GetParts(partType);

            return View();
        }
        
        [RequireParameter("buildName")]
        public ActionResult Edit(string buildName, string motherboard, string gpu, string cpu, string psu, string casename, string ram)
        {
            Entities ORM = new Entities();
            Build UserBuild = new Build();
            //UserBuild.OwnerID = User.Identity.GetUserId().ToString();
            Motherboard tempMB = new Motherboard(motherboard);
            ORM.Motherboards.Add(tempMB);
            UserBuild.Motherboard = tempMB;
            GPU tempGPU = new GPU(gpu);
            ORM.GPUs.Add(tempGPU);
            UserBuild.GPU = tempGPU;
            UserBuild.GPUCount = 1;
            CPU tempCPU = new CPU(cpu);
            ORM.CPUs.Add(tempCPU);
            UserBuild.CPU = tempCPU;
            PSU tempPSU = new PSU(psu);
            ORM.PSUs.Add(tempPSU);
            UserBuild.PSU = tempPSU;
            PCCase tempCase = new PCCase(casename);
            ORM.PCCases.Add(tempCase);
            UserBuild.PCCase = tempCase;
            RAM tempRAM = new RAM(ram);
            ORM.RAMs.Add(tempRAM);
            ORM.SaveChanges();
            UserBuild.MBID = tempMB.MotherboardID;
            UserBuild.GPUID = tempGPU.GPUID;
            UserBuild.CPUID = tempCPU.CPUID;
            UserBuild.PSUID = tempPSU.PSUID;
            UserBuild.CaseID = tempCase.CaseID;
            UserBuild.BuildID = Guid.NewGuid().ToString("D");
            UserBuild.OwnerID = User.Identity.GetUserId().ToString();
            ORM.Builds.Add(UserBuild);
            ORM.SaveChanges();

            return _Edit(UserBuild.BuildID, User.Identity.GetUserId());
        }

        ////edit when given a buildID
        private ActionResult _Edit(string BuildID, string UserID)
        {
            Entities ORM = new Entities();
            Build temp = ORM.Builds.Find(BuildID);

            if (temp.OwnerID == UserID)
            {
                //ViewBag Stuff here
                return View("Edit");
            }
            else
            {
                return View("Display", "Builds", BuildID);
            }
        }

        public ActionResult Edit(string id)
        {
            return _Edit(id, User.Identity.GetUserId());
        }

        [AllowAnonymous]
        public ActionResult Display(string id)
        {
            Entities ORM = new Entities();
            Build temp = ORM.Builds.Find(id);

            if (temp == null)
            {
                ViewBag.Message = "The build you were searching for could not be found!";
                return View("Error");
            }

            return View();
        }

        public JObject GetParts(string partType)
        {
            JObject partlist;
            string partinfo;
            //for (int i = 1; i < 9; i++) //NEEDS- add multiple page queries
            //{
            HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://api.zinc.io/v1/search?query={partType}&page=2&retailer=amazon");
            apiRequest.Headers.Add("Authorization", ConfigurationManager.AppSettings["ZINCkey"]);
            apiRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";


            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            //if (apiResponse.StatusCode != HttpStatusCode.OK) //http error 200
            //{
            //    return (error);
            //}

            StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());

            partinfo = responseData.ReadToEnd();

            partlist = JObject.Parse(partinfo);

            return partlist;
        }

        public List<JObject> GetPartData(JObject partlist)
        {
            List<JObject> Parts = new List<JObject>();
            for (int i = 0; i <= 14; i++)
            {
                string x = partlist["results"][i]["product_id"].ToString();

                HttpWebRequest apiRequest1 = WebRequest.CreateHttp($"https://api.zinc.io/v1/products/{x}?retailer=amazon");
                apiRequest1.Headers.Add("Authorization", ConfigurationManager.AppSettings["ZINCKey"]); //used to add keys
                //apiRequest1.Headers.Add("-u", ConfigurationManager.AppSettings["apizinc"]);
                apiRequest1.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";

                HttpWebResponse apiResponse1 = (HttpWebResponse)apiRequest1.GetResponse();

                //NEEDS - Add if apiresponse error

                StreamReader responseData1 = new StreamReader(apiResponse1.GetResponseStream());

                string partdetails = responseData1.ReadToEnd();

                JObject Temp = JObject.Parse(partdetails);

                Parts.Add(Temp);
            }

            return Parts;
        }

    }
}