using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Salary
    {
        [Required(ErrorMessage = "Enter Employee ID")]
        public int empid { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string empname { get; set; }
        [Required(ErrorMessage = "Enter your BasicPay")]
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