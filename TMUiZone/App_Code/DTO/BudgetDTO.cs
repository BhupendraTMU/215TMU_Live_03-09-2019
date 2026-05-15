using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AshokaHRMBudget.DTO
{
    public class BudgetDTO
    {
        public string Id { get; set; }
        public string DepartmentCode { get; set; }
        public string CompanyName { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sept { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
    }
}