using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project1.Models
{
    public class Salary
    {
        public int empid { get; set; }
        public string empname { get; set; }
        public double basicpay { get; set; }
        public double da { get; set; }
        public double hra { get; set; }
        public double pf { get; set; }
        public double tax { get; set; }
        public double netsalary { get; set; }
    }
    public class SalaryModel
    {
        public List<Salary> SalaryList { get; set; }
    }
}