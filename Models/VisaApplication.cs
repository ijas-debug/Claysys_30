using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FinalProject.Models
{
    public class VisaApplication
    {

        [Key]
        public int ApplicationID { get; set; }


        [Required(ErrorMessage = "Enter First Name!")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Enter Last Name!")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Select Date of Birth!")]
        [Display(Name = "Date of Birth:")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        


        [Required(ErrorMessage = "Please Enter The Email Address !")]
        [Display(Name = "Email Id:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string EmailID { get; set; }


        

        [Required(ErrorMessage = "Enter Phone Number!")]
        [Display(Name = "Phone Number:")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Enter Address!")]
        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Select Expected Date of Arrival!")]
        [Display(Name = "Expected Date of Arrival:")]
        [DataType(DataType.Date)]
        public DateTime ExpectedDateOfArrival { get; set; }

        [Required(ErrorMessage = "Select Expected Date of Departure!")]
        [Display(Name = "Expected Date of Departure:")]
        [DataType(DataType.Date)]
        public DateTime ExpectedDateOfDeparture { get; set; }


        [Required(ErrorMessage = "Select Visa Service!")]
        [Display(Name = "Visa Service:")]
        public string VisaService { get; set; }


        [Required(ErrorMessage = "Select Gender!")]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Enter Town/City of Birth!")]
        [Display(Name = "Town/City of Birth:")]
        public string TownCityOfBirth { get; set; }


        [Required(ErrorMessage = "Enter Country of Birth!")]
        [Display(Name = "Country of Birth:")]
        public string CountryOfBirth { get; set; }


        [Required(ErrorMessage = "Enter Citizenship/National ID No.!")]
        [Display(Name = "Citizenship/National ID No.:")]
        public string CitizenshipNationalIdNo { get; set; }


        [Required(ErrorMessage = "Enter Religion!")]
        [Display(Name = "Religion:")]
        public string Religion { get; set; }


        [Required(ErrorMessage = "Enter Educational Qualification!")]
        [Display(Name = "Educational Qualification:")]
        public string EducationalQualification { get; set; }


        [Required(ErrorMessage = "Enter Passport Type!")]
        [Display(Name = "Passport Type:")]
        public string PassportType { get; set; }


        [Required(ErrorMessage = "Enter Nationality!")]
        [Display(Name = "Nationality:")]
        public string Nationality { get; set; }


        [Required(ErrorMessage = "Enter Passport Number!")]
        [Display(Name = "Passport Number:")]
        public string PassportNumber { get; set; }


        [Required(ErrorMessage = "Enter Place of Issue!")]
        [Display(Name = "Place of Issue:")]
        public string PlaceOfIssue { get; set; }


        [Required(ErrorMessage = "Select Date of Issue!")]
        [Display(Name = "Date of Issue:")]
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }

        [Required(ErrorMessage = "Select Date of Expiry!")]
        [Display(Name = "Date of Expiry:")]
        [DataType(DataType.Date)]
        public DateTime DateOfExpiry { get; set; }

        

        [Display(Name = "Passport or Identity Certificate Number:")]
        public string PassportOrICNo { get; set; }

        [Display(Name = "Port of Arrival:")]
        public string PortOfArrival { get; set; }

        [Display(Name = "Reference Name in India:")]
        public string ReferenceNameInIndia { get; set; }

        [Display(Name = "Reference Address in India:")]
        public string ReferenceAddressInIndia { get; set; }

        [Display(Name = "Reference Phone:")]
        public string ReferencePhone { get; set; }

        [Required(ErrorMessage = "Upload Photo")]
        [Display(Name = "Photo :")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,gif", ErrorMessage = "Only JPEG and GIF images are allowed.")]
        public string Photo { get; set; }


        public string PhotoUrl { get; set; }

        public string Status { get; set; }

        [Display(Name = "ETA Number:")]
        public string ETANumber { get; set; }

        [Display(Name = "No. of Entries:")]
        public string NumberOfEntries { get; set; }

        [Display(Name = "Date of issue of ETA:")]
        [DataType(DataType.Date)]
        public DateTime DateOfIssueETA { get; set; }

        [Display(Name = "Date of expiry of ETA:")]
        [DataType(DataType.Date)]
        public DateTime DateOfExpiryETA { get; set; }

    }
}
