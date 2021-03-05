using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace BustadirForm.Models
{

    [DataContract]
    public class FileUploadInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public string UID { get; set; }
    }

    [DataContract]
    public class Orkulan
    {
        //[DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public HttpPostedFileBase File1 { get; set; }

        //[DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public HttpPostedFileBase File2 { get; set; }

        //[DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public IEnumerable<HttpPostedFileBase> File3 { get; set; }

        //[DataMember]
        //public IEnumerable<HttpPostedFileBase> File4 { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public FileUploadInfo File1 { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public FileUploadInfo File2 { get; set; }

        [DataMember]
        public FileUploadInfo File3 { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public List<FileUploadInfo> Files { get; set; }


        [DataMember]
        public ApplicationBasicInfo ApplicationBasicInfo { get; set; }

        [DataMember]
        public ApplicationExtraInfo ApplicationExtraInfo { get; set; }

        [DataMember]
        [Display(Name = "Váttan og loyvir")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public bool HasAgreed { get; set; }

        public List<InsuranceCompany> _InsuranceCompanies { get; set; }

        public IEnumerable<SelectListItem> InsuranceCompanies
        {
            get { return new SelectList(_InsuranceCompanies, "Name", "Name"); }
        }

        public Orkulan()
        {
            ApplicationBasicInfo = new ApplicationBasicInfo();
            ApplicationExtraInfo = new ApplicationExtraInfo();

            _InsuranceCompanies = new List<InsuranceCompany>();
            _InsuranceCompanies.Add(new InsuranceCompany() { Id = 0, Name = "Tryggingarfelagnum Føroyar" });
            _InsuranceCompanies.Add(new InsuranceCompany() { Id = 1, Name = "Trygd" });
            //var regularExpenditures = new List<Expenditure>();
            //regularExpenditures.Add(new Expenditure() { Id = 0, Description = "SEV", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 1, Description = "Olja", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 2, Description = "Kringvarpsgjald kr.", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 3, Description = "Barnaansing", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 4, Description = "Telefon", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 5, Description = "Internet", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 6, Description = "Televarp/parabol", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 7, Description = "Vegskattur", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 8, Description = "Biltrygging", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 9, Description = "Sethúsatrygging", Amount = 0m });
            //regularExpenditures.Add(new Expenditure() { Id = 10, Description = "Aðrar útreiðslur", Amount = 0m });
            //ApplicationExtraInfo.RegularExpenditures = regularExpenditures;

        }

        public SessionOrder SessionOrder { get; set; }
    }

    [DataContract]
    public class ApplicationBasicInfo
    {
        [DataMember]
        public Person2 Person { get; set; }

        [DataMember]
        public PropertyInfo PropertyInfo { get; set; }

        //[DataMember]
        //[Display(Name = "Vinaliga legg tilboð uppá ætlaða arbeiðið við umsóknini, ella útgreina eina kostnaðarmeting av arbeiðinum á síðu 3. Er tilboð viðlagt?")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public bool HasOfferAttached { get; set; }

        [DataMember]
        [Display(Name = "* Eg vil við hesum søkja um lán á")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal LoanAmount { get; set; }

        //[DataMember]
        //[Display(Name = "dagfesting")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public DateTime AgreementDate { get; set; }

        //[DataMember]
        //[Display(Name = "staður")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string AgreementPlace { get; set; }

        //[DataMember]
        //[Display(Name = "umsøkjari")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string AgreementApplicantFullName { get; set; }

        //[DataMember]
        //[Display(Name = "hjúnafelagi /samánari")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string AgreementPartnerFullName { get; set; }
    }

    [DataContract]
    public class Person2
    {
        [DataMember]
        [Display(Name = "* Navn og eftirnavn")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string FullName { get; set; }

        [DataMember]
        [Display(Name = "* P-tal")]
        [MinLength(9)]
        [MaxLength(10)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string SSN { get; set; }

        [DataMember]
        [Display(Name = "Starv")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string JobTitle { get; set; }

        //[DataMember]
        //[Display(Name = "Telefon heima")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string HomePhone { get; set; }

        //[DataMember]
        //[Display(Name = "* Telefon")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string PhoneNumber { get; set; }

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
        [Display(Name = "* Bústaður")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Address { get; set; }

        [DataMember]
        [Display(Name = "* Býur / bygd")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string City { get; set; }

        [DataMember]
        [Display(Name = "* Land")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Country { get; set; }

        [DataMember]
        [Display(Name = "* Teldupostur")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "* Postnummar")]
        [MaxLength(10)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string PostalCode { get; set; }
    }

    [DataContract]
    public class PropertyInfo
    {
        [DataMember]
        [Display(Name = "* Navn á tinglýstum eigara av ognini")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string RegisteredOwner { get; set; }

        [DataMember]
        [Display(Name = "* Stað (gøta, húsanummar og býur/bygd)")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Address { get; set; }

        [DataMember]
        [Display(Name = "* Matr. nr.")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string LandRegisterNumber { get; set; }
    }

    [DataContract]
    public class ApplicationExtraInfo
    {
        //[DataMember]
        //[Display(Name = "Inntøku-, ognarviðurskifti og fastar útreiðslur")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public PersonIncome IncomeLoanTaker { get; set; }

        //[DataMember]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public PersonIncome IncomeSpouse { get; set; }

        //[DataMember]
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public PersonIncome IncomePartner { get; set; }


        [DataMember]
        [Display(Name = "* 1. veðrættarlán frá")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan MortageLoanNO1 { get; set; }

        [DataMember]
        [Display(Name = "* 2. veðrættarlán frá")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan MortageLoanNO2 { get; set; }

        [DataMember]
        [Display(Name = "* 3. veðrættarlán frá")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan MortageLoanNO3 { get; set; }

        [DataMember]
        [Display(Name = "Ognin er tryggjað hjá")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string InsuranceCompany { get; set; } // Tryggingarfelagnum Føroyar, Trygd

        //[DataMember]
        //[Display(Name = "Onnur lán við veð í fastari ogn, matr. nr.")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public MortgageLoan OtherPropertyLoans { get; set; }

        [DataMember]
        [Display(Name = "Billán")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan CarLoans { get; set; }

        [DataMember]
        [Display(Name = "Bátalán")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan BoatLoans { get; set; }

        [DataMember]
        [Display(Name = "Onnur nýtslulán")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan OtherExpenditureLoans { get; set; }

        [DataMember]
        [Display(Name = "Annað")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public MortgageLoan OtherLoans { get; set; }

        //[DataMember]
        //[Display(Name = "FASTAR ÚTREIÐSLUR")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public List<Expenditure> RegularExpenditures { get; set; }

        [DataMember]
        [Display(Name = "* SEV")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureElectricity { get; set; }

        [DataMember]
        [Display(Name = "* Olja")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureOil { get; set; }

        [DataMember]
        [Display(Name = "* Kringvarpsgjald kr.")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditurePublicService { get; set; }

        [DataMember]
        [Display(Name = "* Barnaansing")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureChildcare { get; set; }

        [DataMember]
        [Display(Name = "* Telefon")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditurePhone { get; set; }

        [DataMember]
        [Display(Name = "* Internet")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureInternet { get; set; }

        [DataMember]
        [Display(Name = "* Televarp/parabol")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureCableTV { get; set; }

        [DataMember]
        [Display(Name = "* Vegskattur")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureRoadTax { get; set; }

        [DataMember]
        [Display(Name = "* Biltrygging")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureCarInsurane { get; set; }

        [DataMember]
        [Display(Name = "* Sethúsatrygging")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureHouseInsurance { get; set; }

        [DataMember]
        [Display(Name = "* Aðrar útreiðslur")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public Expenditure RegularExpenditureOther { get; set; }



        [DataMember]
        [Display(Name = "* Børn undir 18 ár, ið búgva á adressuni")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public int? ChildrenLivingAtHome { get; set; }

        //[DataMember]
        //[Display(Name = "Viðmerkingar")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public String Comments { get; set; }

        [DataMember]
        [Display(Name = "* Dagfesting")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public DateTime AgreementDate { get; set; }

        //[DataMember]
        //[Display(Name = "* Staður")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string AgreementPlace { get; set; }

        //[DataMember]
        //[Display(Name = "undirskrift")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public string AgreementApplicantFullName { get; set; }

        //[DataMember]
        //[Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //public List<EnergySaving> EnergySavingMeasures { get; set; }


        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public EnergySaving EnergySavingMeasureNO1 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO2 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO3 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO4 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO5 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO6 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO7 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO8 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO9 { get; set; }

        [DataMember]
        [Display(Name = "SUNDURGREINAÐ ORKUSPARANDI TILTAK:")]
        public EnergySaving EnergySavingMeasureNO10 { get; set; }

        [DataMember]
        [Display(Name = "Tilsamans")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal EnergySavingMeasuresTotal
        {
            get
            {
                //return
                //    EnergySavingMeasureNO1.Amount +
                //    EnergySavingMeasureNO2.Amount +
                //    EnergySavingMeasureNO3.Amount +
                //    EnergySavingMeasureNO4.Amount +
                //    EnergySavingMeasureNO5.Amount +
                //    EnergySavingMeasureNO6.Amount +
                //    EnergySavingMeasureNO7.Amount +
                //    EnergySavingMeasureNO8.Amount +
                //    EnergySavingMeasureNO9.Amount +
                //    EnergySavingMeasureNO10.Amount;
                return
                    (EnergySavingMeasureNO1.Amount.HasValue ? EnergySavingMeasureNO1.Amount.Value : 0m) +
                    (EnergySavingMeasureNO2.Amount.HasValue ? EnergySavingMeasureNO2.Amount.Value : 0m) +
                    (EnergySavingMeasureNO3.Amount.HasValue ? EnergySavingMeasureNO3.Amount.Value : 0m) +
                    (EnergySavingMeasureNO4.Amount.HasValue ? EnergySavingMeasureNO4.Amount.Value : 0m) +
                    (EnergySavingMeasureNO5.Amount.HasValue ? EnergySavingMeasureNO5.Amount.Value : 0m) +
                    (EnergySavingMeasureNO6.Amount.HasValue ? EnergySavingMeasureNO6.Amount.Value : 0m) +
                    (EnergySavingMeasureNO7.Amount.HasValue ? EnergySavingMeasureNO7.Amount.Value : 0m) +
                    (EnergySavingMeasureNO8.Amount.HasValue ? EnergySavingMeasureNO8.Amount.Value : 0m) +
                    (EnergySavingMeasureNO9.Amount.HasValue ? EnergySavingMeasureNO9.Amount.Value : 0m) +
                    (EnergySavingMeasureNO10.Amount.HasValue ? EnergySavingMeasureNO10.Amount.Value : 0m);
                //return EnergySavingMeasures.Sum(x => x.Amount);
            }
        }
    }

    [DataContract]
    public class PersonIncome
    {
        [DataMember]
        [Display(Name = "Navn")]
        //[MinLength(2)]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string FullName { get; set; }

        [DataMember]
        [Display(Name = "Ársinntøka")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal? YearlyIncome { get; set; }
    }

    [DataContract]
    public class EnergySaving
    {
        [DataMember]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        //[MinLength(2)]
        [Display(Name = "Sundurgreinað orkusparandi tiltak. Greið, frá hvørjar postar tú hevur, og set upphædd á")]
        public string Description { get; set; }

        [DataMember]
        [Display(Name = "Upphædd")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal? Amount { get; set; }
    }

    [DataContract]
    public class Expenditure
    {
        [DataMember]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        [MinLength(2)]
        public string Description { get; set; }

        [DataMember]
        [Display(Name = "kr.")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal? Amount { get; set; }
    }

    [DataContract]
    public class MortgageLoan
    {
        [DataMember]
        [Display(Name = "Banki")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public string Bank { get; set; }

        [DataMember]
        [Display(Name = "kr.")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal? Amount { get; set; }

        [DataMember]
        [Display(Name = "kr.")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(@BootstrapResources.Resources))]
        public decimal? MontlyPayment { get; set; }
    }

    [DataContract]
    public class InsuranceCompany
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}