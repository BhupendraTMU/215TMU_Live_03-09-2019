using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PL
{
    public class PersonalInformationPL
    {
        public PersonalInformationPL()
        { }
        public string No_ { get; set; }
        public string StudentName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Citizenship { get; set; }
        public string AcademicYear { get; set; }
        public string CourseCode { get; set; }
        public string UniversityInterested { get; set; }
        public string EnrollmentNo_ { get; set; }
        public string StudAddress1 { get; set; }
        public string StudAddress2 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Dist { get; set; }
        public string BankName { get; set; }
        public string BankAcNo { get; set; }
        public string Doctor { get; set; }
        public string PrincipalHOD { get; set; }
        public string principalContNo { get; set; }
        public string LocalGuardian { get; set; }
        public string Relation { get; set; }
        public string ImpContactNo { get; set; }
        public string ImpEmailId { get; set; }
        public string ImpAddress { get; set; }
        public string DOB { get; set; }
        public string IncaseOf { get; set; }
        public string StudentImage { get; set; }

        public PersonalInformationPL(
            string No_,
            string StudentName,
            string FathersName,
            string MothersName,
            string Citizenship,
            string AcademicYear,
            string CourseCode,
            string UniversityInterested,
            string EnrollmentNo_,
            string StudAddress1,
            string StudAddress2,
            string State,
            string Country,
            string Dist,
            string BankName,
            string BankAcNo,
            string Doctor,
            string PrincipalHOD,
            string principalContNo,
            string LocalGuardian,
            string Relation,
            string ImpContactNo,
            string ImpEmailId,
            string ImpAddress,
            string DOB,
            string IncaseOf,
            string StudentImage
            )
        {
            this.No_ = No_;
            this.StudentName = StudentName;
            this.FathersName = FathersName;
            this.MothersName = MothersName;
            this.Citizenship = Citizenship;
            this.AcademicYear = AcademicYear;
            this.CourseCode = CourseCode;
            this.UniversityInterested = UniversityInterested;
            this.EnrollmentNo_ = EnrollmentNo_;
            this.StudAddress1 = StudAddress1;
            this.StudAddress2 = StudAddress2;
            this.State = State;
            this.Country = Country;
            this.Dist = Dist;
            this.BankName = BankName;
            this.BankAcNo = BankAcNo;
            this.Doctor = Doctor;
            this.PrincipalHOD = PrincipalHOD;
            this.principalContNo = principalContNo;
            this.LocalGuardian = LocalGuardian;
            this.Relation = Relation;
            this.ImpContactNo = ImpContactNo;
            this.ImpEmailId = ImpEmailId;
            this.ImpAddress = ImpAddress;
            this.DOB = DOB;
            this.IncaseOf = IncaseOf;
            this.StudentImage = StudentImage;


        }

    }
}



