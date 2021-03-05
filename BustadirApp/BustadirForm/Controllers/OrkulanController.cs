using BustadirForm.BLL;
using BustadirForm.Models;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BustadirForm.Controllers
{
    //[RequireHttps]
    [AllowAnonymous]
    public class OrkulanController : BaseController
    {
        Orkulan GetDemoData()
        {
            var model = new Orkulan();

            AddViewData(model);
            //ViewBag.InsuranceCompanies = new SelectList(insuranceCompanies, "Name", "Name");
            // Basic info
            model.ApplicationBasicInfo.Person = new Person2()
            {
                FullName = "Hans Hansen",
                SSN = "290884-XYZ",
                JobTitle = "Software Developer",
                MobilePhone = "+298212121",
                HomePhone = "+298313131",
                WorkPhone = "+298555555",
                //PhoneNumber = "+298219052",
                Address = "Gøta  8",
                PostalCode = "100",
                City = "Tórshavn",
                Country = "Føroyar",
                Email = "info@kthusid.com"
            };
            model.ApplicationBasicInfo.PropertyInfo = new PropertyInfo()
            {
                RegisteredOwner = "Hansina Hansen",
                Address = "Gøta 8",
                LandRegisterNumber = "123456"
            };
            //model.ApplicationBasicInfo.HasOfferAttached = true;
            //model.ApplicationBasicInfo.LoanAmount = 150000m;
            //model.ApplicationBasicInfo.AgreementDate = DateTime.Now;
            //model.ApplicationBasicInfo.AgreementPlace = "Bústaðir";
            //model.ApplicationBasicInfo.AgreementApplicantFullName = "H Hammer";
            //model.ApplicationBasicInfo.AgreementPartnerFullName = "Person 2";
            // Extra info
            //model.ApplicationExtraInfo.IncomeLoanTaker = new PersonIncome() { FullName = "H Hammer", YearlyIncome = 123456m };
            //model.ApplicationExtraInfo.IncomeSpouse = new PersonIncome() { FullName = "", YearlyIncome = 0m };
            //model.ApplicationExtraInfo.IncomePartner = new PersonIncome() { FullName = "", YearlyIncome = 0m };
            model.ApplicationExtraInfo.MortageLoanNO1 = new MortgageLoan()
            {
                Bank = banks[0],
                Amount = 1m,
                MontlyPayment = 2m
            };
            model.ApplicationExtraInfo.MortageLoanNO2 = new MortgageLoan()
            {
                Bank = banks[1],
                Amount = 2m,
                MontlyPayment = 3m
            };
            model.ApplicationExtraInfo.MortageLoanNO3 = new MortgageLoan()
            {
                Bank = banks[2],
                Amount = 4m,
                MontlyPayment = 5m
            };
            model.ApplicationExtraInfo.InsuranceCompany = model._InsuranceCompanies[1].Name;
            //model.ApplicationExtraInfo.OtherPropertyLoans = new MortgageLoan()
            //{
            //    Amount = 6m,
            //    MontlyPayment = 7m
            //};
            model.ApplicationExtraInfo.CarLoans = new MortgageLoan()
            {
                Bank = banks[0],
                Amount = 8m,
                MontlyPayment = 9m
            };
            model.ApplicationExtraInfo.BoatLoans = new MortgageLoan()
            {
                Bank = banks[1],
                Amount = 10m,
                MontlyPayment = 11m
            };
            model.ApplicationExtraInfo.OtherExpenditureLoans = new MortgageLoan()
            {
                Bank = banks[2],
                Amount = 12m,
                MontlyPayment = 13m
            };
            model.ApplicationExtraInfo.OtherLoans = new MortgageLoan()
            {
                Bank = banks[3],
                Amount = 14m,
                MontlyPayment = 15m
            };
            model.ApplicationExtraInfo.RegularExpenditureElectricity = new Expenditure() { Amount = 1, Description = "SEV" };
            model.ApplicationExtraInfo.RegularExpenditureOil = new Expenditure() { Amount = 2, Description = "Olja" };
            model.ApplicationExtraInfo.RegularExpenditurePublicService = new Expenditure() { Amount = 3, Description = "Kringvarpsgjald" };
            model.ApplicationExtraInfo.RegularExpenditureChildcare = new Expenditure() { Amount = 4, Description = "Barnaansing" };
            model.ApplicationExtraInfo.RegularExpenditurePhone = new Expenditure() { Amount = 5, Description = "Telefon" };
            model.ApplicationExtraInfo.RegularExpenditureInternet = new Expenditure() { Amount = 6, Description = "Internet" };
            model.ApplicationExtraInfo.RegularExpenditureCableTV = new Expenditure() { Amount = 7, Description = "Televarp/parabol" };
            model.ApplicationExtraInfo.RegularExpenditureRoadTax = new Expenditure() { Amount = 8, Description = "Vegskattur" };
            model.ApplicationExtraInfo.RegularExpenditureCarInsurane = new Expenditure() { Amount = 9, Description = "Biltrygging" };
            model.ApplicationExtraInfo.RegularExpenditureHouseInsurance = new Expenditure() { Amount = 10, Description = "Sethúsatrygging" };
            model.ApplicationExtraInfo.RegularExpenditureOther = new Expenditure() { Amount = 11, Description = "Aðrar útreiðslur" };
            model.ApplicationExtraInfo.ChildrenLivingAtHome = 0;
            //model.ApplicationExtraInfo.Comments = "ein viðmerking";
            model.ApplicationExtraInfo.AgreementDate = DateTime.Now;
            //model.ApplicationExtraInfo.AgreementPlace = "Bústaðir";
            //model.ApplicationExtraInfo.AgreementApplicantFullName = "Hans Hansen";

            model.ApplicationExtraInfo.EnergySavingMeasureNO1 = new EnergySaving() { Description = "tiltak 1", Amount = 1000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO2 = new EnergySaving() { Description = "tiltak 2", Amount = 2000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO3 = new EnergySaving() { Description = "tiltak 3", Amount = 3000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO4 = new EnergySaving() { Description = "tiltak 4", Amount = 4000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO5 = new EnergySaving() { Description = "tiltak 5", Amount = 5000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO6 = new EnergySaving() { Description = "tiltak 6", Amount = 6000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO7 = new EnergySaving() { Description = "tiltak 7", Amount = 7000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO8 = new EnergySaving() { Description = "tiltak 8", Amount = 8000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO9 = new EnergySaving() { Description = "tiltak 9", Amount = 9000m };
            model.ApplicationExtraInfo.EnergySavingMeasureNO10 = new EnergySaving() { Description = "tiltak 10", Amount = 10000m };
            //model.EnergySavingMeasures = new List<EnergySaving>();
            //model.ApplicationExtraInfo.EnergySavingMeasures = new List<EnergySaving>();
            //for (int i = 0; i < 20; i++)
            //{
            //    model.ApplicationExtraInfo.EnergySavingMeasures.Add(new EnergySaving()
            //    {
            //        Amount = 0m,
            //        Description = ""
            //    });
            //}
            //model.ApplicationExtraInfo.EnergySavingMeasures.Add(new EnergySaving()
            //{
            //    Amount = 5000m,
            //    Description = "orkusparandi tiltak #2"
            //});
            return model;
        }


        List<string> banks = new List<string>();

        private void AddViewData(Orkulan model)
        {
            //model.SessionOrder = SessionHandler.Instance.Read(Session, Request);

            banks.Add("BankNordik");
            banks.Add("Eik");
            banks.Add("Norðoya Sparikassi");
            banks.Add("Suðuroya Sparikassi");
            banks.Add("Annað");
            ViewBag.BankId = new SelectList(banks.Select(m => new SelectListItem
            {
                Text = m,
                Value = m
            }), "Value", "Text");
        }

        [HttpPost]
        public ActionResult SaveFormData(Orkulan model)
        {
            Session.Remove("tempOrkulanModel");
            Session.Add("tempOrkulanModel", model);
            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(bool? isDemo)
        {
            Orkulan model = null;

            //if (isDemo.HasValue && isDemo.Value == true)
            //{
            //    model = GetDemoData();
            //    return View(model);
            //}

            model = Session.Contents["tempOrkulanModel"] as Orkulan;
            if (model != null)
            {
                AddViewData(model);
                return View(model);
            }


            model = new Orkulan();
            AddViewData(model);
            // Basic info
            model.ApplicationBasicInfo.Person = new Person2();
            model.ApplicationBasicInfo.PropertyInfo = new PropertyInfo();
            // Extra info
            //model.ApplicationExtraInfo.IncomeLoanTaker = new PersonIncome() { FullName = "", YearlyIncome = null };
            model.ApplicationExtraInfo.MortageLoanNO1 = new MortgageLoan();
            model.ApplicationExtraInfo.MortageLoanNO2 = new MortgageLoan();
            model.ApplicationExtraInfo.MortageLoanNO3 = new MortgageLoan();
            //model.ApplicationExtraInfo.InsuranceCompany = null;//model._InsuranceCompanies[1].Name;
            //model.ApplicationExtraInfo.OtherPropertyLoans = new MortgageLoan();
            model.ApplicationExtraInfo.CarLoans = new MortgageLoan();
            model.ApplicationExtraInfo.BoatLoans = new MortgageLoan();
            model.ApplicationExtraInfo.OtherExpenditureLoans = new MortgageLoan();
            model.ApplicationExtraInfo.OtherLoans = new MortgageLoan();
            model.ApplicationExtraInfo.RegularExpenditureElectricity = new Expenditure() { Amount = null, Description = "sev" };
            model.ApplicationExtraInfo.RegularExpenditureOil = new Expenditure() { Amount = null, Description = "olja" };
            model.ApplicationExtraInfo.RegularExpenditurePublicService = new Expenditure() { Amount = null, Description = "kvf" };
            model.ApplicationExtraInfo.RegularExpenditureChildcare = new Expenditure() { Amount = null, Description = "barna ansning" };
            model.ApplicationExtraInfo.RegularExpenditurePhone = new Expenditure() { Amount = null, Description = "tlf." };
            model.ApplicationExtraInfo.RegularExpenditureInternet = new Expenditure() { Amount = null, Description = "internet" };
            model.ApplicationExtraInfo.RegularExpenditureCableTV = new Expenditure() { Amount = null, Description = "tv" };
            model.ApplicationExtraInfo.RegularExpenditureRoadTax = new Expenditure() { Amount = null, Description = "veg skattur" };
            model.ApplicationExtraInfo.RegularExpenditureCarInsurane = new Expenditure() { Amount = null, Description = "bil trygging" };
            model.ApplicationExtraInfo.RegularExpenditureHouseInsurance = new Expenditure() { Amount = null, Description = "hús trygging" };
            model.ApplicationExtraInfo.RegularExpenditureOther = new Expenditure() { Amount = null, Description = "ymist annað" };
            //model.ApplicationExtraInfo.ChildrenLivingAtHome = null;
            model.ApplicationExtraInfo.AgreementDate = DateTime.Now;
            //model.ApplicationExtraInfo.AgreementPlace = null;
            model.ApplicationExtraInfo.EnergySavingMeasureNO1 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO2 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO3 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO4 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO5 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO6 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO7 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO8 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO9 = new EnergySaving() { Description = null, Amount = null };
            model.ApplicationExtraInfo.EnergySavingMeasureNO10 = new EnergySaving() { Description = null, Amount = null };

            return View(model);
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }

        public ActionResult SaveFileList1(IEnumerable<HttpPostedFileBase> FileList1)
        {
            return SaveFile(FileList1);
        }

        public ActionResult SaveFileList2(IEnumerable<HttpPostedFileBase> FileList2)
        {
            return SaveFile(FileList2);
        }

        public ActionResult SaveFileList3(IEnumerable<HttpPostedFileBase> FileList3)
        {
            return SaveFile(FileList3);
        }

        public ActionResult SaveFileList4(IEnumerable<HttpPostedFileBase> FileList4)
        {
            return SaveFile(FileList4);
        }

        private ActionResult SaveFile(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var sessionFolder = GetSessionFolder();
                    var physicalPath = Path.Combine(sessionFolder, fileName);

                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult RemoveFile(string[] FileList1)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (FileList1 != null)
            {
                foreach (var fullName in FileList1)
                {
                    var fileName = Path.GetFileName(fullName);
                    var sessionFolder = GetSessionFolder();
                    var physicalPath = Path.Combine(sessionFolder, fileName);
                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        string GetFilePath(string name)
        {
            var fileName = Path.GetFileName(name);
            var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Files"), fileName);
            return physicalPath;
        }

        private string GetSessionFolder()
        {
            //var sessionOrder = SessionHandler.Instance.Read(Session, Request);
            //if (sessionOrder != null)
            //{
            try
            {
                var sessionFolder = Path.Combine(Server.MapPath("~/App_Data/Files"), Session.SessionID);
                Directory.CreateDirectory(sessionFolder);
                return sessionFolder;
            }
            catch (Exception)
            {
                // todo: handle
            }
            //}
            return Server.MapPath("~/App_Data/Files");
        }

        private string GetFilePhysicalPath(string fileName)
        {
            var sessionFolder = GetSessionFolder();
            return Path.Combine(sessionFolder, fileName);
        }


        [HttpPost]
        public ActionResult SubmitForm(Orkulan model)
        {
            if (true)//(ModelState.IsValid)
            {
                try
                {
                    var files = new List<FileInfo>();
                    if (model.File1 != null && model.File1.Name != null)
                    {
                        files.Add(new FileInfo(GetFilePhysicalPath(model.File1.Name)));
                    }
                    if (model.File2 != null && model.File2.Name != null)
                    {
                        files.Add(new FileInfo(GetFilePhysicalPath(model.File2.Name)));
                    }
                    if (model.File3 != null && model.File3.Name != null)
                    {
                        files.Add(new FileInfo(GetFilePhysicalPath(model.File3.Name)));
                    }
                    if (model.Files != null)
                    {
                        foreach (var file in model.Files)
                        {
                            if (file != null && file.Name != null)
                            {
                                files.Add(new FileInfo(GetFilePhysicalPath(file.Name)));
                            }
                        }
                    }
                    //var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/OrkulanEmailTemplate.html"), System.Text.Encoding.UTF8);
                    var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/Views/Orkulan/EmailTemplate.cshtml"), System.Text.Encoding.UTF8);
                    //var rcptList = new[] { model.ApplicationBasicInfo.Person.Email, "lan@bustadir.fo" };
                    var subject = "Umsókn - Orkulán";
                    string body = Razor.Parse(template, model);
                    System.IO.File.WriteAllText(HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Orkulan/", Session.SessionID + ".htm")), body.ToString());
                    SendMail(new[] {"lan@bustadir.fo"}, subject, body, files);// Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    SendMail(new[] { "hans@kthusid.com" }, subject, body, files);
                    //try
                    //{
                    //    System.Threading.Thread.Sleep(50);
                    //    if (files.Count > 0)
                    //    {
                    //        var path = Path.GetDirectoryName(files[0].FullName);
                    //        System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(path);
                    //        foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                    //        {
                    //            file.Delete();
                    //        }
                    //        foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                    //        {
                    //            dir.Delete(true);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    // todo: handle
                    //    return Json(ex.Message);
                    //}
                    //finally
                    //{
                    //    SessionHandler.Instance.Destroy(Session, Request);
                    //}
                    return Json(HttpStatusCode.OK);
                }
                catch (UriTemplateMatchException ex)
                {
                    return Json(ex.Message);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
                finally
                {
                    SessionHandler.Instance.Destroy(Session, Request);
                }
            }
            //return Json(this.ModelState.Values.SelectMany(m => m.Errors));
            //return Json(HttpStatusCode.BadRequest);

        }

        public ActionResult EmailTemplate()
        {
            var model = GetDemoData();
            return View(model);
        }

        public ActionResult SendEmailTemplate()
        {
            var model = GetDemoData();
            var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/Views/Orkulan/EmailTemplate.cshtml"), System.Text.Encoding.UTF8);
            var rcptList = new[] { "hans@kthusid.com" };
            var subject = "Umsókn - Orkulán";
            string body = Razor.Parse(template, model);
            System.IO.File.WriteAllText(HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Orkulan/", Session.SessionID + ".html")), body);
            SendMail(rcptList, subject, body, null);
            return View(model);
        }

        //public ActionResult TestEmailTemplate()
        //{
        //    var model = GetDemoData();
        //    try
        //    {
        //        var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/OrkulanEmailTemplate.html"), System.Text.Encoding.UTF8);
        //        string body = Razor.Parse(template, model);
        //        return Content(body);
        //    }
        //    catch (UriTemplateMatchException ex)
        //    {
        //        //return View(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        //return View(ex.Message);
        //    }
        //    return View(model);
        //}
    }
}