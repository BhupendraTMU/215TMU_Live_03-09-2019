using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL
{
    public class EnquiryPL
    {
        public EnquiryPL()
        { }
        public string No_ { get; set; }
        public string Details { get; set; }

        public string EnquiryDate { get; set; }
        public string EnquiryType { get; set; }
        public string EnquirySource { get; set; }
        public string NameoftheMedia { get; set; }
        public string EnquirerName { get; set; }
        public string ApplicantRelationship { get; set; }
        public string ApplicantName { get; set; }
        public string DateofBirth { get; set; }
        public string Father_sName { get; set; }
        public string Mother_sName { get; set; }
        public string ApplicantStatus { get; set; }
        public string AcademicYear { get; set; }
        public string CourseCode { get; set; }
        public string UniversityInterested { get; set; }
        public int HostelAcommodation { get; set; }//boolean
        public string Prequalification { get; set; }
        public string NameofthePreviousInstitute { get; set; }
        public string CertificationAuthoriry { get; set; }
        public string MediumofInstruction { get; set; }
        public string Addressto { get; set; }
        public string Addressee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string EMailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public string State { get; set; }
        public string No_Series { get; set; }
        public string Address3 { get; set; }
        public int ConvertedtoApplication { get; set; }
        public int Age { get; set; }
        public int Months { get; set; }
        public string UserID { get; set; }
        public string PortalID { get; set; }
        public string CollegeInterested { get; set; }
        public string Category { get; set; }
        public string Religion { get; set; }
        public int SubReligion { get; set; }
        public string Remarks_Feedback { get; set; }
        public int FeeType { get; set; }
        //=============Follow Up==========
        public int LineNo { get; set; }
        public int FollowUpStatus { get; set; }
        public string NextFollowUpDate { get; set; }
        public string Remarks { get; set; }
        public String FollowUpStatusName { get; set; }

        //=============Follow Up==========

        public EnquiryPL(
            string No_,
            string Details
            )
        {
            this.No_ = No_;
            this.Details = Details;
        }

        public EnquiryPL(
          string No_,
          string EnquiryDate,
          string EnquiryType,
          string EnquirySource,
          string NameoftheMedia,
          string EnquirerName,
          string ApplicantRelationship,
          string ApplicantName,
          string DateofBirth,
          string Father_sName,
          string Mother_sName,
          string ApplicantStatus,
          string AcademicYear,
          string CourseCode,
          string UniversityInterested,
          int HostelAcommodation,
          string Prequalification,
          string NameofthePreviousInstitute,
          string CertificationAuthoriry,
          string MediumofInstruction,
          string Addressto,
          string Addressee,
          string Address1,
          string Address2,
          string City,
          string PostCode,
          string CountryCode,
          string EMailAddress,
          string MobileNumber,
          string PhoneNumber,
          int Gender,
          string State,
          string No_Series,
          string Address3,
          int ConvertedtoApplication,
          int Age,
          int Months,
          string UserID,
          string PortalID,
          string CollegeInterested,
          string Category,
          string Religion,
          int SubReligion,
          string Remarks_Feedback,
          int FeeType
          )
        {
            this.No_ = No_;
            this.EnquiryDate = EnquiryDate;
            this.EnquiryType = EnquiryType;
            this.EnquirySource = EnquirySource;
            this.NameoftheMedia = NameoftheMedia;
            this.EnquirerName = EnquirerName;
            this.ApplicantRelationship = ApplicantRelationship;
            this.ApplicantName = ApplicantName;
            this.DateofBirth = DateofBirth;
            this.Father_sName = Father_sName;
            this.Mother_sName = Mother_sName;
            this.ApplicantStatus = ApplicantStatus;
            this.AcademicYear = AcademicYear;
            this.CourseCode = CourseCode;
            this.UniversityInterested = UniversityInterested;
            this.HostelAcommodation = HostelAcommodation;
            this.Prequalification = Prequalification;
            this.NameofthePreviousInstitute = NameofthePreviousInstitute;
            this.CertificationAuthoriry = CertificationAuthoriry;
            this.MediumofInstruction = MediumofInstruction;
            this.Addressto = Addressto;
            this.Addressee = Addressee;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.City = City;
            this.PostCode = PostCode;
            this.CountryCode = CountryCode;
            this.EMailAddress = EMailAddress;
            this.MobileNumber = MobileNumber;
            this.PhoneNumber = PhoneNumber;
            this.Gender = Gender;
            this.State = State;
            this.No_Series = No_Series;
            this.Address3 = Address3;
            this.ConvertedtoApplication = ConvertedtoApplication;
            this.Age = Age;
            this.Months = Months;
            this.UserID = UserID;
            this.PortalID = PortalID;
            this.CollegeInterested = CollegeInterested;
            this.Category = Category;
            this.Religion = Religion;
            this.SubReligion = SubReligion;
            this.Remarks_Feedback = Remarks_Feedback;
            this.FeeType = FeeType;
        }

        public EnquiryPL(string No_, int LineNo, int FollowUpStatus, string NextFollowUpDate, string Remarks, string FollowUpStatusName)
        {
            this.No_ = No_;
            this.LineNo = LineNo;
            this.FollowUpStatus = FollowUpStatus;
            this.NextFollowUpDate = NextFollowUpDate;
            this.Remarks = Remarks;
            this.FollowUpStatusName = FollowUpStatusName;

        }

    }
}



