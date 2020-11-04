using Microsoft.Ajax.Utilities;
using rita.Business;
using rita.Information;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ViewBag.Title = "abc";
            return View();
        }

        public async Task<ActionResult> List(PaginationInfo pagination, TestMasterInfo.Conditions conditions, string view = "")
        {
            #region 取資料

            //var biz = new rita.Business.TestBiz.Master();
            //DataTable dt = biz.Load(ref pagination, conditions: conditions);

            var request = new ApiReruest
            {
                pagination = pagination,
                conditions = conditions
            };

            var biz = new ApiBiz();
            var response = await biz.Post<ApiResponse>("http://localhost/api/api/", "test", "load", request);
            #region
            //Linq
            //List<TestMasterInfo> model = new List<TestMasterInfo>();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    model =
            //    dt.AsEnumerable()
            //    .Select(i => new TestMasterInfo
            //    {
            //        SID = i.Field<int>("SID"),
            //        ID = i.Field<string>("ID"),
            //        NO = i.Field<string>("NO"),
            //        Name = i.Field<string>("Name"),
            //        Address = i.Field<string>("Address"),
            //        Phone = i.Field<string>("Phone"),
            //        Age = i.Field<decimal?>("Age"),
            //        Birthday = i.Field<DateTime?>("Birthday"),
            //        CreateTime = i.Field<DateTime>("CreateTime"),
            //        UpdateTime = i.Field<DateTime>("UpdateTime")
            //    })
            //    .ToList();
            //}
            #endregion

            #endregion
            #region ViewBag

            ViewBag.Pager = response.pagination;
            ViewBag.Conditions = response.conditions;

            #endregion

            string path = "~/Views/Shared/Test/_List.cshtml";
            if (!string.IsNullOrEmpty(view)) path = $"~/Views/Shared/Test/_List_{view}.cshtml";

            //return View(model);
            return PartialView(path, response.data);
        }

        public ActionResult Read(TestMasterInfo.Conditions conditions)
        {
            var biz = new rita.Business.TestBiz.Master();
            var model = biz.Load(SID: conditions.SID, ID: conditions.ID, NO: conditions.NO);
            return PartialView("~/Views/Shared/Test/_Read.cshtml", model);
        }

        public ActionResult Create(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Create.cshtml");
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(TestMasterInfo entity, bool isPartialView=false,string fun =null)
        {
            if(!string.IsNullOrEmpty(fun) && fun == "Cancel")
            {
                return Content("");
            }
            var biz = new TestBiz.Master();
            if (biz.Load(null, null, entity.NO) != null)
            {
                ModelState.AddModelError("NO", "編號已存在");
            }

            if (ModelState.IsValid)//如果必要有值的話就執行 不然就拋回去
            {

                if (biz.Insert(entity) > 0)
                {
                    //return Read(new TestMasterInfo.Conditions { ID = entity.ID, NO = entity.NO });
                    string js = string.Format("$('#dumyID').val('{0}');$('#dumyList').submit();$('#dumyRead').submit();", entity.ID);
                    return JavaScript(js);
                }
            }
            if(isPartialView)
                return PartialView("~/Views/Shared/Test/_Create.cshtml", entity);
            else
                return View(entity);

        }

        public ActionResult Edit(TestMasterInfo entity, bool isPartialView = true)
        {
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Edit.cshtml", entity);
            else
                return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TestMasterInfo entity, bool isPartialView = true, string fun = null)
        {
            if (!string.IsNullOrWhiteSpace(fun))
            {
                if (fun == "Cancel")
                {
                    return Content("");
                }
                if (fun == "Close")
                {
                    return Read(new TestMasterInfo.Conditions { SID = entity.SID, ID = entity.ID });
                }
            }
            if (ModelState.IsValid)
            {
                var biz = new TestBiz.Master();
                if (biz.Update(entity) > 0)
                {
                    //return Read(new TestMasterInfo.Conditions { ID = entity.ID });
                    string js = string.Format("$('#dumyID').val('{0}');$('#dumyList').submit();$('#dumyRead').submit();", entity.ID);
                    return JavaScript(js);
                }
            }
            if (isPartialView)
                return PartialView("~/Views/Shared/Test/_Edit.cshtml", entity);
            else
                return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var biz = new TestBiz.Master();
            int i = biz.Delete(SID: null, ID: id, null);
            if (i > 0)
            {
                string js = string.Format("$('#dumyList').submit();$('#divajax').html('');");
                return JavaScript(js);
            }
            else
            {
                return JavaScript("alert('刪除失敗');");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Do(string mode,int? sid =null,string id = null)
        {
            switch (mode)
            {
                case "Insert":
                    return Create(true);
                case "Edit":
                    var biz = new TestBiz.Master();
                    return Edit(biz.Load(SID: sid, ID: id, null));
                case "Delete":
                    return Delete(id);
            }
            return JavaScript("");
        }

        public class ApiReruest
        {
            public PaginationInfo pagination { get; set; }
            public TestMasterInfo.Conditions conditions { get; set; }
        }
        public class ApiResponse
        {
            public PaginationInfo pagination { get; set; }
            public TestMasterInfo.Conditions conditions { get; set; }
            public List<TestMasterInfo> data { get; set; }
        }
    }
}