using BustadirForm.BLL;
using BustadirForm.Models;
using Newtonsoft.Json;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BustadirForm.Controllers
{
    //[RequireHttps]
    [AllowAnonymous]
    public class TilmeldingController : BaseController
    {
        private static readonly string K = "5e313f69352370424f736e686e6747657979337b344d32714739703164487c796372425d2d716172353b5d4f657972614354292978646e38574a28417b726544";

        BasicFamilyInfo GetDemoData()
        {
            var model = new BasicFamilyInfo()
            {
                //EducationInfo = new List<EducationInfo>(),
                //TotalIncomeBeforeTax = new List<IncomeBeforeTax>(),
                //ApartmentExtra = new List<ApartmentExtra>(),
                //WishListAreas = new List<WishListArea>()
            };

            AddViewData(model);
            //model.EducationInfo.Add(new EducationInfo() { Description = "Fólkaskúli", Id = 0, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Miðnámsútbúgving", Id = 1, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Handverkaraútbúgving / skrivstovuútbúgving", Id = 2, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Styttri útbúgving frá hægri lærustovni (2¼ ár)", Id = 3, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Miðal útbúgving frá hægri lærustovni (upp til 4 ár)", Id = 4, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Longri útbúgving frá hægri lærustovni (yvir 4 ár)", Id = 5, HasDiploma = false });
            //model.EducationInfo.Add(new EducationInfo() { Description = "Annað", Id = 6, HasDiploma = false });

            model.PaymentData = new PaymentData()
            {
                Acquirer = "PBS",
                ActionCode = "000",
                Amount = "100",
                CardNumberMasked = "123456XXXXXX7890",
                CardTypeName = "MasterCard",
                Currency = "208",
                ExpMonth = "MM",
                ExpYear = "YY",
                Merchant = "901XXXXX",
                OrderId = "test1",
                TransactionStatus = "ACCEPTED",
                TransactionDate = DateTime.Now,
                TransactionId = "1018917960",
                MAC = "45836a36876539c169155ee2ce9217ed9059feacdc90294289b7c3d170c92a03"
            };


            var TotalIncomeBeforeTax = new List<string>();
            TotalIncomeBeforeTax.Add("Undir 100.000");
            TotalIncomeBeforeTax.Add("100.000 - 200.000 kr");
            TotalIncomeBeforeTax.Add("200.000 - 300.000 kr");
            TotalIncomeBeforeTax.Add("300.000 - 500.000 kr");
            TotalIncomeBeforeTax.Add("500.000 - 700.000 kr");
            TotalIncomeBeforeTax.Add("700.000 kr ella yvir");
            TotalIncomeBeforeTax.Add("Eg ynski ikki at svara");

            ViewBag.TotalIncomeBeforeTax = TotalIncomeBeforeTax;

            /*var typeOfApartment = new List<string>();
            typeOfApartment.Add("Vanliga bústað");
            typeOfApartment.Add("Lestrarbústað");
            typeOfApartment.Add("Eldrabústað (60+ ár)");
            ViewBag.typeOfApartment = typeOfApartment;*/
            model.TypeOfApartment1 = true;
            model.TypeOfApartment2 = false;
            model.TypeOfApartment3 = false;

            model.ApartmentExtraOutdoorHouse = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraWorkHouse = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraBalcony = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraOwnWashingMachine = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraNoNeighboursAboveOrBelow = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraGoodView = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraSchoolKindergardenNearby = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraIsNearCentrum = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraShoppingMallNearby = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraGoodBusConnections = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraFriendlyNeighbourhood = EApartmentExtraWishImportance.NEEDED;
            model.ApartmentExtraFriendlyNeighbourhoodForChildren = EApartmentExtraWishImportance.NOT_NEEDED;

            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 0, Description = "Atgongd til hjall", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 1, Description = "Atgongd til arbeiðsskúr", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 2, Description = "Atgongd til altan/terassu", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 3, Description = "Egna vaskimaskinu", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 4, Description = "At tú ikki hevur yvir- og undirbúgvar", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 5, Description = "Útsýni", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 6, Description = "At skúli / barnagarður er tætt við", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 7, Description = "At bústaðurin liggur mitt í býnum", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 8, Description = "At matvøruhandil er tætt við", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 9, Description = "Busssambandið", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 10, Description = "At økið er friðaligt", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });
            //model.ApartmentExtra.Add(new ApartmentExtra() { Id = 11, Description = "At økið er barnavinaligt", Importance = ApartmentExtra.EApartmentExtraWishImportance.NOT_NEEDED });

            //model.WishListAreas.Add(new WishListArea() { Id = 0, Ranking = 7, CityAndBuildingName = "Klaksvík, bygningur 1", Description = "Norðoyggjar" });
            //model.WishListAreas.Add(new WishListArea() { Id = 1, Ranking = 4, CityAndBuildingName = "Runavík, bygningur 2", Description = "Eysturoy" });
            //model.WishListAreas.Add(new WishListArea() { Id = 2, Ranking = 2, CityAndBuildingName = "Vestmanna, bygningur 3", Description = "Norðurstreymoy" });
            //model.WishListAreas.Add(new WishListArea() { Id = 3, Ranking = 1, CityAndBuildingName = "Tórshavn, bygningur 4", Description = "Suðurstreymoy" });
            //model.WishListAreas.Add(new WishListArea() { Id = 4, Ranking = 3, CityAndBuildingName = "Sandavágur, bygningur 5", Description = "Vágoy" });
            //model.WishListAreas.Add(new WishListArea() { Id = 5, Ranking = 6, CityAndBuildingName = "Skopun, bygningur 6", Description = "Sandoy" });
            //model.WishListAreas.Add(new WishListArea() { Id = 6, Ranking = 5, CityAndBuildingName = "Hvalba, bygningur 7", Description = "Suðuroy" });

            //model.WishListArea0 = new WishListArea() { Ranking = 7, CityAndBuildingName = "Klaksvík, bygningur 1" };
            //model.WishListArea1 = new WishListArea() { Ranking = 4, CityAndBuildingName = "Runavík, bygningur 2" };
            //model.WishListArea2 = new WishListArea() { Ranking = 2, CityAndBuildingName = "Vestmanna, bygningur 3" };
            //model.WishListArea3 = new WishListArea() { Ranking = 1, CityAndBuildingName = "Tórshavn, bygningur 4" };
            //model.WishListArea4 = new WishListArea() { Ranking = 3, CityAndBuildingName = "Sandavágur, bygningur 5" };
            //model.WishListArea5 = new WishListArea() { Ranking = 6, CityAndBuildingName = "Skopun, bygningur 6" };
            //model.WishListArea6 = new WishListArea() { Ranking = 5, CityAndBuildingName = "Hvalba, bygningur 7" };



            //model.ApartmentCostWithoutLightAndHeat = "3.000 - 4.000 kr";
            model.ApartmentCostWithoutLightAndHeatOption0 = false;
            model.ApartmentCostWithoutLightAndHeatOption1 = false;
            model.ApartmentCostWithoutLightAndHeatOption2 = false;
            model.ApartmentCostWithoutLightAndHeatOption3 = false;
            model.ApartmentCostWithoutLightAndHeatOption4 = true;
            model.ApartmentCostWithoutLightAndHeatOption5 = false;
            model.ApartmentCostWithoutLightAndHeatOption6 = false;

            model.FullNameApplicant = "Benjamin Hammer";// "Hans Blaasvær";
            model.SSN = "290884-123";
            model.FullNameOfPartner = "";
            model.CurrentAddress = "Mækjugøta 8";
            model.CurrentlyLivingAt = ECurrentLivingStatus.FAMILY_OR_FRIENDS;
            model.RentalCost = 2000;
            model.RentalCostOil = 300;
            model.RentalCostElectricity = 250;
            model.ApartmentHeatingType = "við oljufýring";
            model.ApartmentFloorSize = 60;
            model.ApartmentRecidencePeriod = new RecidencePeriod() { ApartmentRecidenceYears = 2, ApartmentRecidenceMonths = 5 };
            model.OtherReasonForNewApartment = "no reason";
            model.EmailAddress = "benjamin@kthusid.com";
            model.PostalCode = "100";
            model.CityName = "Tórshavn";
            model.CountryCode = "FO";
            model.Country = "Føroyar";
            model.MobilePhone = "+298555552";
            model.HomePhone = "+298555552";
            model.WorkPhone = "+298555552";
            //model.HomePhone = "654321";
            //model.ChildrenLivingHomeUnderEighteen = new ChildInfo() { Amount = 0, SSN_List = new List<string>() };
            //model.ChildrenLivingHomeUnderEighteen_SSN_List = new List<string>();
            //model.ChildrenLivingHomeUnderEighteenNotLivingAtHomeFullTime = new ChildInfo() { Amount = 0, SSN_List = new List<string>() };
            //model.ChildrenLivingHomeUnderEighteenNotLivingAtHomeFullTime_SSN_List = new List<string>();
            model.IsRentingFromFaroeseDwellingCommunity = true;
            model.FaroeseDwellingCommunityLocation = "onkustaðni...";
            model.TypeOfApartment1 = true;
            model.IsActiveMember = true;
            model.WantsOneRoom = true;
            model.WantsTwoRooms = true;
            model.ApartmentHeightPlacement = EApartmentLocation.SecondStore;
            //model.HasPet = true;
            model.HasSpecialNeeds = true;
            model.SpecialNeedsComment = "ein viðmerking";
            model.Comments = "ein viðmerking";
            model.TotalIncomeBeforeTax = TotalIncomeBeforeTax[2];

            model.ApartmentExtraWishes = "...";

            model.EducationLevel0 = new EducationInfo() { HasDiploma = true, PartnerHasDiploma = true };
            model.EducationLevel1 = new EducationInfo() { HasDiploma = true, PartnerHasDiploma = true };
            model.EducationLevel2 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel3 = new EducationInfo() { HasDiploma = true, PartnerHasDiploma = false };
            model.EducationLevel4 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel5 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = true };
            model.EducationLevel6 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };

            return model;
        }

        public ActionResult SessionTest()
        {
            return View(SessionHandler.Instance.Read(Session, Request));
        }

        //[HttpGet]
        //public ActionResult SessionReset()
        //{
        //    SessionHandler.Instance.Destroy(SessionHandler.Instance.Read(Session, Request).Id, Session, Request);
        //    return RedirectToAction("SessionTest");
        //}

        private void AddViewData(BasicFamilyInfo model)
        {
            //model.SessionOrder = SessionHandler.Instance.Read(Session, Request);

            if (!model.IsActiveMember.HasValue)
            {
                model.IsActiveMember = true; // default
            }
            //model.ApartmentHeightPlacement = model.ApartmentHeightPlacement == EApartmentLocation.DoesntMatter ? EApartmentLocation.DoesntMatter : model.ApartmentHeightPlacement;
            var totalIncomeBeforeTax = new List<string>();
            totalIncomeBeforeTax.Add("Undir 100.000");
            totalIncomeBeforeTax.Add("100.000 - 200.000 kr");
            totalIncomeBeforeTax.Add("200.000 - 300.000 kr");
            totalIncomeBeforeTax.Add("300.000 - 500.000 kr");
            totalIncomeBeforeTax.Add("500.000 - 700.000 kr");
            totalIncomeBeforeTax.Add("700.000 kr ella yvir");
            totalIncomeBeforeTax.Add("Eg ynski ikki at svara");
            ViewBag.TotalIncomeBeforeTax = totalIncomeBeforeTax;

            var typeOfApartment = new List<string>();
            typeOfApartment.Add("Vanliga bústað");
            typeOfApartment.Add("Lestrarbústað");
            typeOfApartment.Add("Eldrabústað (60+ ár)");
            ViewBag.typeOfApartment = typeOfApartment;

            var wishListAreaChoices = new List<string>();
            wishListAreaChoices.Add("Norðoyggjar");
            wishListAreaChoices.Add("Eysturoy");
            wishListAreaChoices.Add("Norðurstreymoy");
            wishListAreaChoices.Add("Suðurstreymoy");
            wishListAreaChoices.Add("Vágoy");
            wishListAreaChoices.Add("Sandoy");
            wishListAreaChoices.Add("Suðuroy");
            ViewBag.WishListAreaChoices = wishListAreaChoices;

            if (model.WishListAreas == null)
            {
                model.WishListAreas = new List<WishListArea>();
                model.WishListAreas.Add(new WishListArea() { Ranking = 1, Name = "" });
                //Hans 05-01-2021
                //model.WishListAreas.Add(new WishListArea() { Ranking = 2, Name = "" });
                //model.WishListAreas.Add(new WishListArea() { Ranking = 3, Name = "" });

            }

            if (model.ReasonForNewApartment == null)
            {
                model.ReasonForNewApartment = new List<string>();
            }

            var reasonForNewApartment = new List<string>();
            reasonForNewApartment.Add("Ynski minni bústað");
            reasonForNewApartment.Add("Havi tørv á størri bústaði");
            reasonForNewApartment.Add("Ynski minni viðlíkahald");
            reasonForNewApartment.Add("Ynski bíligari bústað");
            reasonForNewApartment.Add("Verandi bústaðarstøða er ov ótrygg");
            reasonForNewApartment.Add("Ynski orkuvinarligari bústað");
            reasonForNewApartment.Add("Ynski mær egnan bústað");
            reasonForNewApartment.Add("At flyta til annað øki í landinum");
            reasonForNewApartment.Add("At flyta til Føroyar");
            reasonForNewApartment.Add("Broyttar fíggjarligar umstøður");
            reasonForNewApartment.Add("Onnur orsøk");
            ViewBag.ReasonForNewApartment = reasonForNewApartment;
        }

        [HttpPost]
        public ActionResult SaveFormData(BasicFamilyInfo model)
        {
            Session.Remove("tempTilmeldingModel");
            Session.Add("tempTilmeldingModel", model);
            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()//bool? isDemo)
        {
            //if (isDemo.HasValue && isDemo.Value == true)
            //{
            //    return View(GetDemoData());
            //}
            BasicFamilyInfo model = Session.Contents["tempTilmeldingModel"] as BasicFamilyInfo;
            if (model != null)
            {
                AddViewData(model);
                return View(model);
            }
            model = new BasicFamilyInfo();
            AddViewData(model);

            model.ApartmentHeightPlacement = EApartmentLocation.DoesntMatter;
            model.ApartmentRecidencePeriod = new RecidencePeriod() { ApartmentRecidenceYears = 0, ApartmentRecidenceMonths = 0 };

            //model.WishListArea0 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea1 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea2 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea3 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea4 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea5 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };
            //model.WishListArea6 = new WishListArea() { Ranking = null, CityAndBuildingName = "" };

            model.EducationLevel0 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel1 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel2 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel3 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel4 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel5 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };
            model.EducationLevel6 = new EducationInfo() { HasDiploma = false, PartnerHasDiploma = false };


            return View(model);
        }
        //public ActionResult KvittanTest()
        //{
        //    var model = GetDemoData();
        //    return View("Kvittan", model);
        //}

        [HttpPost]
        public ActionResult Kvittan()
        {
            string errfilename = HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Tilmelding/", "error" + DateTime.Now.Millisecond + ".txt"));
            string filename, filenameResult;
            try
            {
                var responseHeaders = Request.Form;
                var responseHeaderList = new Dictionary<string, string>();
                for (int i = 0; i < responseHeaders.Count; i++)
                {
                    string key = responseHeaders.GetKey(i);
                    string value = responseHeaders.Get(i);
                    responseHeaderList.Add(key, value);

                }
                //ViewBag.PaymentResponseList = responseHeaderList;


                string OrderId = responseHeaderList["orderId"];
                string TransactionStatus = responseHeaderList["status"];
                string TransactionId = responseHeaderList["transaction"];
                string CardNumberMasked = responseHeaderList["cardNumberMasked"];



                filename = HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Tilmelding/", OrderId + ".htm"));
                filenameResult = HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Tilmelding/", TransactionId + ".html"));

                if (!System.IO.File.Exists(filenameResult)) // Hans
                {
                    string body = System.IO.File.ReadAllText(filename);
                    body = body.Replace("<p>Transaction Id</p>", "<p>Transaction Id</p>" + TransactionId);
                    body = body.Replace("<p>Transaction Status</p>", "<p>Transaction Status</p>" + TransactionStatus);
                    body = body.Replace("<p>Card Mask</p>", "<p>Card Mask</p>" + CardNumberMasked);
                    int posbyrjan = body.IndexOf("<span id='emailOfApplicant'>", 0);
                    int posendi = body.IndexOf("</span>", posbyrjan);
                    string EmailAddress = body.Substring(posbyrjan + 30, posendi - posbyrjan - 30).Trim();

                    try
                    {
                        System.IO.File.WriteAllText(filenameResult, body);
                    }
                    catch (Exception ex)
                    {

                    }


                    var template = "";
                    var rcptList = new[] { EmailAddress, "mals@bustadir.fo" };
                    var subject = "Umsókn - Tilmelding";

                    try
                    {
                        SendMail(new[] { "hans@kthusid.com" }, subject, body, null);
                        SendMail(rcptList, subject, body, null);//  rcptlist           
                        //return Json(HttpStatusCode.OK);
                    }
                    catch (UriTemplateMatchException ex)
                    {
                        //return View(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        //return View(ex.Message);
                    }

                    finally
                    {
                        // Destroy the session data
                        //SessionHandler.Instance.Destroy(model.SessionOrder.Id, Session, Request);
                    }
                }
                var model = new BasicFamilyInfo();
                model.PaymentData = new PaymentData();
                model.PaymentData.OrderId = OrderId;
                model.PaymentData.TransactionId = TransactionId;
                model.PaymentData.TransactionDate = DateTime.Now;
                model.PaymentData.TransactionStatus = TransactionStatus;
                model.PaymentData.CardNumberMasked = CardNumberMasked;
                return View(model);

                //System.IO.File.WriteAllText(errfilename, EmailAddress);

                //model.PaymentData = new PaymentData()
                //{
                //    Acquirer = responseHeaderList["acquirer"],
                //    ActionCode = responseHeaderList["actionCode"],
                //    Amount = responseHeaderList["amount"],
                //    CardNumberMasked = responseHeaderList["cardNumberMasked"],
                //    CardTypeName = responseHeaderList["cardTypeName"],
                //    Currency = responseHeaderList["currency"],
                //    ExpMonth = responseHeaderList["expMonth"],
                //    ExpYear = responseHeaderList["expYear"],
                //    Merchant = responseHeaderList["merchant"],
                //    OrderId = responseHeaderList["orderId"],
                //    TransactionStatus = responseHeaderList["status"],
                //    TransactionDate = DateTime.Now,
                //    TransactionId = responseHeaderList["transaction"],
                //    MAC = responseHeaderList["MAC"]
                //};
                //model.PaymentData.OrderId = "" + model.SessionOrder.OrderId;
                /*acceptReturnUrl = http://localhost:44302/Tilmelding/Kvittan
                acquirer = PBS
                actionCode = 000
                amount = 100
                billingAddress = MÃ¦kjugÃ¸ta 8
                billingEmail = benjamin@kthusid.com
                billingFirstName = Benjamin
                billingLastName = Hammer
                billingMobile = +298555552
                billingPostalCode = 100
                billingPostalPlace = TÃ³rshavn
                cardNumberMasked = 517014XXXXXX9766
                cardTypeName = MasterCard
                currency = 208
                expMonth = 01
                expYear = 17
                merchant = 90151340
                orderId = test1
                status = ACCEPTED
                transaction = 1018917960
                MAC = 45836a36876539c169155ee2ce9217ed9059feacdc90294289b7c3d170c92a03*/

                //try
                //{
                //    //template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/EmailTemplate.html"), System.Text.Encoding.UTF8);
                //    template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/Views/Tilmelding/EmailTemplate.cshtml"), System.Text.Encoding.UTF8);
                //    body = Razor.Parse(template, model);
                //}
                //catch (Exception ex)
                //{

                //}

            }

            catch (Exception ex)
            {
                string msg = ex.ToString();
                msg += "\n";
                msg += ex.StackTrace;
                msg += "\n";
                msg += ex.Message;
                if (ex.InnerException != null)
                {
                    msg += ex.InnerException.ToString();
                    msg += "\n";
                    msg += ex.InnerException.StackTrace;
                    msg += "\n";
                    msg += ex.Message;
                }
                System.IO.File.WriteAllText(errfilename, msg);
                throw;
            }
        }

        [HttpGet]
        public ActionResult CalculateMac(string msg)
        {
            return Json(TransactionHandler.CalculateMac(msg, K), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult GetMerchantUrl(PaymentModel data) // BasicFamilyInfo model, string msg, string acceptReturnUrl, bool useMac = true)
        //{
        //    return Json(data);
        //    TempData.Add("model", model);
        //    return Content(null);
        //    // No parameters received. You need to specify at minimum merchant, currency, amount, orderId and acceptReturnUrl.
        //    // https://sat1.dibspayment.com/dibspaymentwindow/entrypoint?merchant=90151340&currency=208&amount=200&orderId=test1&acceptReturnUrl=http://localhost:44302/tilmelding#/4

        //    var url = "https://sat1.dibspayment.com/dibspaymentwindow/entrypoint";
        //    var merchantId = 90151340;
        //    var currencyCode = 208;     // DKK
        //    var amount = 100;// 20000;         // 200 DKK
        //    var orderId = "test1";
        //    msg = url + "?merchant=" + merchantId + "&currency=" + currencyCode + "&amount=" + amount + "&orderId=" + orderId + "&" + msg + (!string.IsNullOrEmpty(acceptReturnUrl) ? ("&acceptReturnUrl=" + acceptReturnUrl) : "");
        //    if (useMac)
        //    {
        //        msg = msg + "&MAC=" + TransactionHandler.CalculateMac(msg, K);
        //    }
        //    return Content(msg);
        //}

        [HttpPost]
        public ActionResult MakePayment(BasicFamilyInfo model)
        {
            if (ModelState.IsValid)
            {
                var template = "";
                string body = "";
                string filename;

                model.SessionOrder = SessionHandler.Instance.Read(Session, Request);
                
                model.PaymentData = new PaymentData()
                {
                    OrderId = "" + model.SessionOrder.OrderId,
                    TransactionDate = DateTime.Now,
                };
                 try
                 {
                    filename = HttpContext.Server.MapPath("~/Views/Tilmelding/EmailTemplate.cshtml");
                    template = System.IO.File.ReadAllText(filename, System.Text.Encoding.UTF8);
                    body = Razor.Parse(template, model);
                }
                catch (Exception ex) { }
                
                try
                {
                    filename = HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Tilmelding/", model.SessionOrder.OrderId + ".htm"));
                    System.IO.File.WriteAllText(filename, body);
                }
                catch (Exception ex) { }

                TempData["model"] = model;

                //if (TempData.ContainsKey("model"))
                //{
                //    TempData["model"] = model;
                //}
                //else
                //{
                //    TempData.Add("model", model);
                //}


                // No parameters received. You need to specify at minimum merchant, currency, amount, orderId and acceptReturnUrl.
                // https://sat1.dibspayment.com/dibspaymentwindow/entrypoint?merchant=90151340&currency=208&amount=200&orderId=test1&acceptReturnUrl=http://localhost:44302/tilmelding#/4

                var url = "https://sat1.dibspayment.com/dibspaymentwindow/entrypoint";
                var merchantId = 90151340;
                var currencyCode = 208;     // DKK
                var amount = 20000;         // 200 DKK Hans
                var orderId = model.SessionOrder.OrderId;
                var emailAddress = model.EmailAddress;
                var requestAppPath = Request.ApplicationPath.EndsWith("/") ? Request.ApplicationPath : Request.ApplicationPath + "/";
                var acceptReturnUrl = Request.Url.GetLeftPart(UriPartial.Authority) + requestAppPath + "Tilmelding/Kvittan"; // model.AcceptReturnUrl
                var acceptReturnUrlString = (!string.IsNullOrEmpty(acceptReturnUrl) ? ("&acceptReturnUrl=" + acceptReturnUrl) : "");
                var msg2 = RemoveDiacritics(Server.UrlDecode(model.Msg));

                var msg = url + "?merchant=" + merchantId + "&currency=" + currencyCode + "&amount=" + amount + "&orderId=" + orderId + "&emailAddress=" + emailAddress + "&" + msg2 + acceptReturnUrlString;
                //if (model.UseMac)
                //{
                //    msg = msg + "&MAC=" + TransactionHandler.CalculateMac(msg, K);
                //}
                //return Content(msg);
                return Json(new DibsPreOrderInfo()
                {
                    AcceptUrl = acceptReturnUrl,
                    AcceptReturnUrl = acceptReturnUrl,
                    Amount = amount,
                    Currency = currencyCode,
                    Merchant = merchantId,
                    OrderId = orderId,
                    EmailAddress = emailAddress,
                    MSG = msg
                });
            }
            return Json(HttpStatusCode.BadRequest);
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace("æ", "ae").Replace("ø", "oe");
        }

        //[HttpPost]
        ////public ActionResult SubmitForm(string obj)
        //public ActionResult SubmitForm(BasicFamilyInfo model)
        //{
        //    //var model = JsonConvert.DeserializeObject<List<BasicFamilyInfo>>(obj)[0];
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/EmailTemplate.html"), System.Text.Encoding.UTF8);
        //            var rcptList = new[] { "benjamin@kthusid.com" };
        //            var subject = "Tilmelding";
        //            string body = Razor.Parse(template, model);
        //            SendMail(rcptList, subject, body, null);// Newtonsoft.Json.JsonConvert.SerializeObject(data));
        //            return Json(HttpStatusCode.OK);
        //        }
        //        catch (UriTemplateMatchException ex)
        //        {
        //            return Json(ex.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(ex.Message);
        //        }
        //    }
        //    return Json(HttpStatusCode.BadRequest);
        //}

        public ActionResult EmailTemplate()
        {
            var model = GetDemoData();
            return View(model);
        }

        public ActionResult SendEmailTemplate()
        {
            var model = GetDemoData();
            var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/Views/Tilmelding/EmailTemplate.cshtml"), System.Text.Encoding.UTF8);
            var rcptList = new[] { "hans@kthusid.com" };
            var subject = "Umsókn - Tilmelding";
            string body = Razor.Parse(template, model);
            //System.IO.File.WriteAllText(HttpContext.Server.MapPath(Path.Combine("~/App_Data/Mail/Orkulan/", Session.SessionID + ".html")), body);
            SendMail(rcptList, subject, body, null);
            return View(model);
        }

        //public ActionResult TestEmailTemplate()
        //{
        //    var model = GetDemoData();
        //    try
        //    {
        //        var template = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/EmailTemplate.html"), System.Text.Encoding.UTF8);
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

        //public ActionResult EmailTemplate()
        //{
        //    var model = GetDemoData();
        //    return View(model);
        //}

        public ActionResult TestMail()
        {
            SendMail(new[] { "hans@kthusid.com" }, "Test mail from IIS", "Bert ein roynd", null);
            return View();
        }

        public ActionResult Apitester()
        {
            return View();
        }

        public ActionResult BrowserInfo()
        {
            return View();
        }

        public ActionResult Mobile()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = Kthusid.Helpers.CultureHelper.Instance.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies.Get("_culture");
            if (cookie != null)
            {
                //cookie.Expires = DateTime.UtcNow.AddYears(-1);
                cookie.Value = culture;   // update cookie value
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Shareable = true;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            var returnTo = Request.UrlReferrer;
            if (returnTo == null)
                return Redirect("/");
            return Redirect(returnTo.ToString());
        }

        ///**
        //* AuthorizeCard
        //* Authorizes a given card using the AuthorizeCard JSON service.
        //* @param amount The amount of the purchase in smallest unit
        //* @param cardNumber The customers cardnumber
        //* @param clientIp The customers IP address
        //* @param currency The currency either in numeric or string format (e.g. 208/DKK)
        //* @param merchantId DIBS Merchant ID / customer number
        //* @param orderId The shops order ID for the purchase
        //* @param cvc Card Verification Value, 3 digit
        //* @param expMonth Expiry month of the card, 1 or 2 digits (e.g. 6/06)
        //* @param expYear Expiry year of the card, 2 digits (e.g. 18 for 2018)
        //* @param K The secret HMAC key from DIBS Admin
        //**/
        //static void AuthorizeCard(string amount, string cardNumber, string clientIp, string currency, string cvc, string expMonth, string expYear, string merchantId, string orderId, string K)
        //{
        //    //Create Dictionary<string, string> object with used values. Can be modified to contain additional parameters.
        //    Dictionary<string, string> message = new Dictionary<string, string> {
        //        {"amount", amount},
        //        {"cardNumber", cardNumber},
        //        {"clientIp", clientIp},
        //        {"currency",currency},
        //        {"cvc", cvc},
        //        {"expMonth", expMonth},
        //        {"expYear", expYear},
        //        {"merchantId", merchantId},
        //        {"orderId", orderId}
        //    };

        //    //Uncomment following line for test mode to be active
        //    message.Add("test", "true");

        //    //Calculate mac and add it
        //    string mac = calculateMac(message, K);
        //    message.Add("MAC", mac);

        //    //Use postToDIBS() function to send the parameters and receive the response
        //    Dictionary<string, string> res = postToDIBS("AuthorizeCard", message);

        //    System.Console.WriteLine("Authorization done.");
        //    System.Console.WriteLine("Response:");
        //    foreach (KeyValuePair<string, string> r in res)
        //    {
        //        System.Console.WriteLine("{0} = {1}", r.Key, r.Value);
        //    }
        //}

        ///**
        //* postToDIBS
        //* Sends a set of parameters to a DIBS API function
        //* @param paymentFunction The name of the target payment function, e.g. AuthorizeCard
        //* @param data A set of parameters to be posted in Dictionary<string, string> format
        //* @return Dictionary<string, string>
        //*/
        //static Dictionary<string, string> postToDIBS(string paymentFunction, Dictionary<string, string> data)
        //{
        //    //Set correct POST URL corresponding to the payment function requested
        //    string postUrl = "https://api.dibspayment.com/merchant/v1/JSON/Transaction/";
        //    switch (paymentFunction)
        //    {
        //        case "AuthorizeCard":
        //            postUrl += "AuthorizeCard";
        //            break;
        //        case "AuthorizeTicket":
        //            postUrl += "AuthorizeTicket";
        //            break;
        //        case "CancelTransaction":
        //            postUrl += "CancelTransaction";
        //            break;
        //        case "CaptureTransaction":
        //            postUrl += "CaptureTransaction";
        //            break;
        //        case "CreateTicket":
        //            postUrl += "CreateTicket";
        //            break;
        //        case "RefundTransaction":
        //            postUrl += "RefundTransaction";
        //            break;
        //        case "Ping":
        //            postUrl += "Ping";
        //            break;
        //        default:
        //            System.Console.WriteLine("Wrong input paymentFunctions to postToDIBS");
        //            postUrl = null;
        //            break;
        //    }

        //    //Create JSON string from Dictionary<string, string>
        //    string json_data = JsonConvert.SerializeObject(data);
        //    json_data = "request=" + json_data;

        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] json_data_encoded = encoding.GetBytes(json_data);

        //    //Using HttpWebRequest for posting and receiving response
        //    HttpWebRequest con = (HttpWebRequest)WebRequest.Create(postUrl);

        //    con.Method = "POST";
        //    con.ContentType = "application/x-www-form-urlencoded";
        //    con.ContentLength = json_data_encoded.Length;
        //    con.Timeout = 15000; //15 seconds timeout

        //    //Send the POST request
        //    using (Stream stream = con.GetRequestStream())
        //    {
        //        stream.Write(json_data_encoded, 0, json_data_encoded.Length);
        //    }

        //    //Receive response
        //    Dictionary<string, string> res_dict = new Dictionary<string, string> { };
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)con.GetResponse();
        //        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //        //Create Dictionary<string,string> hashmap from response JSON data
        //        res_dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
        //    }
        //    catch (System.Net.WebException)
        //    {
        //        Console.WriteLine("Timeout occured...");
        //    }
        //    return res_dict;
        //}

        //private static string calculateMac(Dictionary<string, string> message, string K)
        //{
        //    throw new NotImplementedException();
        //}
    }
}