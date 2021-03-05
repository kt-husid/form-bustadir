using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BustadirForm.Models
{
    [DataContract]
    public class PaymentModel
    {
        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public string AcceptReturnUrl { get; set; }

        [DataMember]
        public bool UseMac { get; set; }
    }

    [DataContract]
    public class RecidencePeriod
    {
        [DataMember]
        [Display(Name = "ár")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public int ApartmentRecidenceYears { get; set; }

        [DataMember]
        [Display(Name = "mánaðir")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public int ApartmentRecidenceMonths { get; set; }
    }

    [DataContract]
    public class PaymentData
    {
        [DataMember]
        public string Acquirer { get; set; }

        [DataMember]
        public string ActionCode { get; set; }

        [DataMember]
        public string Amount { get; set; }

        [DataMember]
        public string CardNumberMasked { get; set; }

        [DataMember]
        public string CardTypeName { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string ExpMonth { get; set; }

        [DataMember]
        public string ExpYear { get; set; }

        [DataMember]
        public string Merchant { get; set; }

        [DataMember]
        public string OrderId { get; set; }

        [DataMember]
        public string TransactionStatus { get; set; }

        [DataMember]
        public string TransactionId { get; set; }

        [DataMember]
        public DateTime TransactionDate { get; set; }

        [DataMember]
        public string MAC { get; set; }
    }

    [DataContract]
    public class BasicFamilyInfo
    {
        [DataMember]
        public PaymentData PaymentData { get; set; }

        [DataMember]
        public SessionOrder SessionOrder { get; set; }

        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public string AcceptReturnUrl { get; set; }

        //[DataMember]
        //public bool UseMac { get; set; }

        [DataMember]
        [Display(Name = "* Navn og eftirnavn á umsøkjara")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string FullNameApplicant { get; set; }

        [DataMember]
        [Display(Name = "* P-tal")]
        [MinLength(9)]
        [MaxLength(11)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string SSN { get; set; }

        [DataMember]
        [Display(Name = "Navn og eftirnavn á hjúnarfelaga / sambúgva")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string FullNameOfPartner { get; set; }

        [DataMember]
        [Display(Name = "P-tal")]
        public string PartnerSSN { get; set; }

        [DataMember]
        [Display(Name = "* Núverandi bústaður")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string CurrentAddress { get; set; }

        [DataMember]
        [Display(Name = "Hvar býrt tú í løtuni?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public ECurrentLivingStatus CurrentlyLivingAt { get; set; }

        [DataMember]
        [Display(Name = "* Annað, greið frá")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string CurrentlyLivingAtOtherPlace { get; set; }

        [DataMember]
        [Display(Name = "Leiga")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal RentalCost { get; set; }

        [DataMember]
        [Display(Name = "Olja")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal RentalCostOil { get; set; }

        [DataMember]
        [Display(Name = "El")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal RentalCostElectricity { get; set; }

        [DataMember]
        [Display(Name = "Hvussu verður bústaðurin upphitaður?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string ApartmentHeatingType { get; set; }

        [DataMember]
        [Display(Name = "Hvussu stór er gólvvíddin á tínum núverandi bústaði?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal ApartmentFloorSize { get; set; }

        [DataMember]
        [Display(Name = "Hvussu leingi hevur tú búð í tinum núverandi bústaði?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public RecidencePeriod ApartmentRecidencePeriod { get; set; }

        //[DataMember]
        //[Display(Name = "* Hví ynskir tú / tit annan bústað?")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public EReasonForNewApartment ReasonForNewApartment { get; set; }
        /* reasonForNewApartment.Add("Ynski minni bústað");
            reasonForNewApartment.Add("Havi tørv á størri bústaði");
            reasonForNewApartment.Add("Ynski minni viðlíkahald");
            reasonForNewApartment.Add("Ynski bíligari bústað");
            reasonForNewApartment.Add("Verandi bústaðarstøða er ov ótrygg");
            reasonForNewApartment.Add("Ynski orkuvinarligari bústað");
            reasonForNewApartment.Add("Ynski mær egnan bústað");
            reasonForNewApartment.Add("At flyta til annað øki í landinum");
            reasonForNewApartment.Add("At flyta til Føroyar");
            reasonForNewApartment.Add("Broyttar fíggjarligar umstøður");
            reasonForNewApartment.Add("Onnur orsøk");*/


        [DataMember]
        [Display(Name = "* Hví ynskir tú / tit annan bústað?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public List<string> ReasonForNewApartment { get; set; }

        [DataMember]
        [Display(Name = "* Onnur orsøk, greið frá")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string OtherReasonForNewApartment { get; set; }

        [DataMember]
        [Display(Name = "* Teldupostur")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataMember]
        [Display(Name = "* Postnummar")]
        [MaxLength(10)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string PostalCode { get; set; }

        [DataMember]
        [Display(Name = "* Býur / bygd")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string CityName { get; set; }

        [DataMember]
        [Display(Name = "* Landakota")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string CountryCode { get; set; }

        [DataMember]
        [Display(Name = "* Land")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Country { get; set; }

        [DataMember]
        [Display(Name = "* Fartelefon")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string MobilePhone { get; set; }

        [DataMember]
        [Display(Name = "Heimatelefon")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string HomePhone { get; set; }

        [DataMember]
        [Display(Name = "Arbeiðstelefon")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string WorkPhone { get; set; }


        [DataMember]
        [Display(Name = "* Tal á heimabúgvandi børnum undir 18 ár")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public List<string> ChildrenLivingHomeUnderEighteen_SSN_List { get; set; }
        //public ChildInfo ChildrenLivingHomeUnderEighteen { get; set; }

        //[DataMember]
        //[Display(Name = "* Tal av børnum undir 18 ár, ið ikki búgva heima fulla tíð")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public List<string> ChildrenLivingHomeUnderEighteenNotLivingAtHomeFullTime_SSN_List { get; set; }
        ////public ChildInfo ChildrenLivingHomeUnderEighteenNotLivingAtHomeFullTime { get; set; }

        [DataMember]
        [Display(Name = "* Ja")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public bool IsRentingFromFaroeseDwellingCommunity { get; set; }

        [DataMember]
        [Display(Name = "Hvar?")]
        public string FaroeseDwellingCommunityLocation { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public bool? IsActiveMember { get; set; }

        [DataMember]
        public IList<WishListArea> WishListAreas { get; set; }

        //[DataMember]
        ////[Display(Name = "Norðoyggjar")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea0 { get; set; }

        //[DataMember]
        ////[Display(Name = "Eysturoy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea1 { get; set; }

        //[DataMember]
        ////[Display(Name = "Norðurstreymoy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea2 { get; set; }

        //[DataMember]
        ////[Display(Name = "Suðurstreymoy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea3 { get; set; }

        //[DataMember]
        ////[Display(Name = "Vágoy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea4 { get; set; }

        //[DataMember]
        ////[Display(Name = "Sandoy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea5 { get; set; }

        //[DataMember]
        ////[Display(Name = "Suðuroy")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public WishListArea WishListArea6 { get; set; }

        [DataMember]
        [Display(Name = "1 rúm")]
        public bool WantsOneRoom { get; set; }

        [DataMember]
        [Display(Name = "2 rúm")]
        public bool WantsTwoRooms { get; set; }

        [DataMember]
        [Display(Name = "3 rúm")]
        public bool WantsThreeRooms { get; set; }

        [DataMember]
        [Display(Name = "4 rúm")]
        public bool WantsFourRooms { get; set; }

        [DataMember]
        [Display(Name = "5 rúm")]
        public bool WantsFiveRooms { get; set; }

        [DataMember]
        [Display(Name = "6 rúm")]
        public bool WantsSixRooms { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentLocation ApartmentHeightPlacement { get; set; }

        //[DataMember]
        //public string ApartmentCostWithoutLightAndHeat { get; set; }

        [DataMember]
        [Display(Name = "2.000 - 3.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption0 { get; set; }

        [DataMember]
        [Display(Name = "3.000 - 4.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption1 { get; set; }

        [DataMember]
        [Display(Name = "4.000 - 5.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption2 { get; set; }

        [DataMember]
        [Display(Name = "5.000 - 6.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption3 { get; set; }

        [DataMember]
        [Display(Name = "6.000 - 7.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption4 { get; set; }

        [DataMember]
        [Display(Name = "7.000 - 8.000 kr")]
        public bool ApartmentCostWithoutLightAndHeatOption5 { get; set; }

        [DataMember]
        [Display(Name = "8.000 kr - ella yvir")]
        public bool ApartmentCostWithoutLightAndHeatOption6 { get; set; }

        [DataMember]
        [Display(Name = "VÁTTAN")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public bool HasAgreed { get; set; }

        [DataMember]
//        public bool HasPet { get; set; }
        public EHasPet HasPet { get; set; }


        [DataMember]
        public bool HasSpecialNeeds { get; set; }

        [DataMember]
        public string SpecialNeedsComment { get; set; }

        [DataMember]
        [Display(Name = "Aðrar viðmerkingar")]
        public string Comments { get; set; }

        //[DataMember]
        //public IList<EducationInfo> EducationInfo { get; set; }

        [DataMember]
        [Display(Name = "Fólkaskúli")]
        public EducationInfo EducationLevel0 { get; set; }

        [DataMember]
        [Display(Name = "Miðnámsútbúgving")]
        public EducationInfo EducationLevel1 { get; set; }

        [DataMember]
        [Display(Name = "Handverkaraútbúgving / skrivstovuútbúgving")]
        public EducationInfo EducationLevel2 { get; set; }

        [DataMember]
        [Display(Name = "Styttri útbúgving frá hægri lærustovni (2¼ ár)")]
        public EducationInfo EducationLevel3 { get; set; }

        [DataMember]
        [Display(Name = "Miðal útbúgving frá hægri lærustovni (upp til 4 ár)")]
        public EducationInfo EducationLevel4 { get; set; }

        [DataMember]
        [Display(Name = "Longri útbúgving frá hægri lærustovni (yvir 4 ár)")]
        public EducationInfo EducationLevel5 { get; set; }

        [DataMember]
        [Display(Name = "Annað")]
        public EducationInfo EducationLevel6 { get; set; }


        [DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string TotalIncomeBeforeTax { get; set; }

        [DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        [Display(Name = "Vanligan bústað")]
        public bool TypeOfApartment1 { get; set; }

        [DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        [Display(Name = "Lestrarbústað")]
        public bool TypeOfApartment2 { get; set; }

        [DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        [Display(Name = "Eldrabústað (60+ ár)")]
        public bool TypeOfApartment3 { get; set; }

        [DataMember]
        [Display(Name = "Hvørji útbúgving ert tú undir?")]
        public string TypeOfEducation { get; set; }

        //[DataMember]
        //public IList<ApartmentExtra> ApartmentExtra { get; set; }


        [DataMember]
        [Display(Name = "Atgongd til hjall")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraOutdoorHouse { get; set; }

        [DataMember]
        [Display(Name = "Atgongd til arbeiðsskúr")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraWorkHouse { get; set; }

        [DataMember]
        [Display(Name = "Atgongd til altan / terassu")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraBalcony { get; set; }

        [DataMember]
        [Display(Name = "Egna vaskimaskinu")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraOwnWashingMachine { get; set; }

        [DataMember]
        [Display(Name = "At tú ikki hevur yvir- og undirbúgvar")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraNoNeighboursAboveOrBelow { get; set; }

        [DataMember]
        [Display(Name = "Útsýni")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraGoodView { get; set; }

        [DataMember]
        [Display(Name = "At skúli / barnagarður er tætt við")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraSchoolKindergardenNearby { get; set; }

        [DataMember]
        [Display(Name = "At bústaðurin liggur mitt í býnum")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraIsNearCentrum { get; set; }

        [DataMember]
        [Display(Name = "At matvøruhandil er tætt við")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraShoppingMallNearby { get; set; }

        [DataMember]
        [Display(Name = "Busssambandið")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraGoodBusConnections { get; set; }

        [DataMember]
        [Display(Name = "At økið er friðaligt")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraFriendlyNeighbourhood { get; set; }

        [DataMember]
        [Display(Name = "At økið er barnavinaligt")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EApartmentExtraWishImportance ApartmentExtraFriendlyNeighbourhoodForChildren { get; set; }

        [DataMember]
        [Display(Name = "Onnur ynski, greið frá")]
        public string ApartmentExtraWishes { get; set; }

        //public bool WishListArea0_Active { get; set; }

        //public bool WishListArea1_Active { get; set; }

        //public bool WishListArea2_Active { get; set; }

        //public bool WishListArea3_Active { get; set; }

        //public bool WishListArea4_Active { get; set; }

        //public bool WishListArea5_Active { get; set; }

        //public bool WishListArea6_Active { get; set; }
    }

    public enum EHasPet
    {
        [Description("Líkamikið")]
        DoesntMatter,
        [Description("Ja")]
        Ja,
        [Description("Nei")]
        Nei
    }

    public enum EApartmentLocation
    {
        [Description("Veghædd")]
        StreetLevel,
        [Description("1. hædd")]
        FirstStore,
        [Description("2. hædd")]
        SecondStore,
        [Description("Líkamikið")]
        DoesntMatter
    }

    //public enum EReasonForNewApartment
    //{
    //    [Description("Ynski minni bústað")]
    //    WANT_SMALLER_PLACE,
    //    [Description("Havi tørv á størri bústaði")]
    //    WANT_BIGGER_PLACE,
    //    [Description("Ynski minni viðlíkahald")]
    //    WANT_LESS_MAINTENANCE,
    //    [Description("Ynski bíligari bústað")]
    //    WANT_CHEAPER_PLACE,
    //    [Description("Verandi bústaðarstøða er ov ótrygg")]
    //    CURRENT_PLACE_IS_INSECURE,
    //    [Description("Ynski orkuvinarligari bústað")]
    //    WANT_ENERGYSAVING_PLACE,
    //    [Description("Ynski mær egnan bústað")]
    //    WANT_OWN_PLACE,
    //    [Description("At flyta til annað øki í landinum")]
    //    WANT_TO_MOVE_TO_ANOTHER_LOCATION_WITHIN_THE_COUNTRY,
    //    [Description("At flyta til Føroyar")]
    //    WANT_TO_MOVE_TO_HOME_COUNTRY,
    //    [Description("Broyttar fíggjarligar umstøður")]
    //    CHANGED_FINANCIAL_CIRCUMSTANCES,
    //    [Description("Onnur orsøk")]
    //    OTHER
    //}

    public enum ECurrentLivingStatus
    {
        [Description("Í egnum bústaði")]
        OWN_PLACE,
        [Description("Í húsi / íbúð, sum eg leigi")]
        APARTMENT_FOR_RENT,
        [Description("Hjá familju / vinum")]
        FAMILY_OR_FRIENDS,
        [Description("Eg búgvi uttanlands")]
        ABROAD,
        [Description("Hjá fosturfamilju / barnaheimi")]
        FOSTER_FAMILY,
        [Description("Á stovni / sjúkrahúsi")]
        INSTITUTION_OR_HOSPITAL,
        [Description("Annað")]
        OTHER
    }

    //[DataContract]
    //public class ApartmentExtra
    //{
    //    //[DataMember]
    //    //public int Id { get; set; }

    //    //[DataMember]
    //    //public EApartmentExtraWishImportance Importance { get; set; } // null = not important, false

    //    [DataMember]
    //    public string Importance { get; set; }

    //    [DataMember]
    //    public string Description { get; set; }
    //}

    public enum EApartmentExtraWishImportance
    {
        [Description("Ikki neyðugt")]
        NOT_NEEDED,
        [Description("Ynski")]
        WISH,
        [Description("Neyðugt")]
        NEEDED
    }

    [DataContract]
    public class EducationInfo
    {
        //[DataMember]
        //public string Description { get; set; }

        //[DataMember]
        //public int Id { get; set; }

        [DataMember]
        [Display(Name = "Tú")]
        public bool HasDiploma { get; set; }

        [DataMember]
        [Display(Name = "Makin")]
        public bool PartnerHasDiploma { get; set; }
    }

    [DataContract]
    public class WishListArea
    {
        [DataMember]
        [Display(Name = "Raðfesting")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public int Ranking { get; set; }

        [DataMember]
        [Display(Name = "Øki")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Name { get; set; }

        //[DataMember]
        //[Display(Name = "Bygd, býlingur")]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string CityAndBuildingName { get; set; }

        //[DataMember]
        //public string Description { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ChildInfo
    {
        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public int Amount { get; set; }

        [DataMember]
        [Display(Name = "P-tal")]
        public List<string> SSN_List { get; set; }
    }

}