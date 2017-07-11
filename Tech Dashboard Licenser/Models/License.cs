
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Dashboard_Licenser.Models
{
    public class License
    {
        public License()
        {

        }

        public License(int licenseId, string companyName, string typeOfLicense, int totalLicenseCount, DateTime licenseIssueDate, DateTime licenseExpirationDate
            , string licenseEncodedString)
        {
            LicenseID = licenseId;
            CompanyName = companyName;
            TypeOfLicense = typeOfLicense;
            TotalLicenseCount = totalLicenseCount;
            LicenseIssueDate = licenseIssueDate;
            LicenseExpirationDate = licenseExpirationDate;
            LicenseEncodedString = licenseEncodedString;
        }

        public int LicenseID { get; set; }

        public string CompanyName { get; set; }

        public string TypeOfLicense { get; set; }

        public int TotalLicenseCount { get; set; }

        public DateTime LicenseIssueDate { get; set; }

        public DateTime LicenseExpirationDate { get; set; }

        public string LicenseEncodedString { get; set; }

        public override string ToString()
        {
            return LicenseEncodedString;
        }
    }
}
