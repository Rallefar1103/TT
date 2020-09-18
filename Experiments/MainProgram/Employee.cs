using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MainProgram
{
    class Employee
    {
        protected string Name { get; set; }
        protected string JobTitle { get; set; }
        protected decimal Salary { get; set; }
        protected int Seneniority { get; set; }
        public Employee(string name, string jobTitle, decimal salary)
        {
            Name = name;
            JobTitle = jobTitle;
            Salary = salary;
            Seneniority = 0;
        }
        public Employee(string name, string jobTitle)
        {
            Name = name;
            JobTitle = jobTitle;
            this.AvailableEmployeeJobsAndSetSalary();
            Seneniority = 0;
        }
        private void AvailableEmployeeJobsAndSetSalary()
        {
            if (JobTitle == "Worker")
            {
                Salary = 10;
            }
            else if (JobTitle == "Office")
            {
                Salary = 20;
            }
            else if (JobTitle == "Sales")
            {
                Salary = 30;
            }
            else if (JobTitle == "Boss")
            {
                Salary = 50;
            }
            else if (JobTitle == "Director")
            {
                Salary = 100;
            }
            else
            {
                throw new Exception("JobTitle doesn't exist!");
            }
        }
        public virtual decimal CalculateYearlySalary()
        {
            int MONTHS_IN_YEAR = 12;
            return MONTHS_IN_YEAR * Salary;
        }
        protected decimal OneMoreYear()
        {
            Seneniority += 1;
            Salary = Salary * (0.1m * Seneniority);
            return Salary;
        }
    }
    class Manager : Employee
    {
        protected decimal Bonus { get; set; }
        public Manager(string name, string jobTitle, decimal salary, decimal bonus) :base(name,jobTitle,salary)
        {
            Bonus = bonus;
        }
        public Manager(string name, string jobTitle) :base(name,jobTitle)
        {
            this.AvailableManagerJobsAndSetBonus(jobTitle);
        }
        private void AvailableManagerJobsAndSetBonus(string jobTitle)
        {
            if (jobTitle == "Boss")
            {
                Bonus = 100;
            }
            else if (jobTitle == "Director")
            {
                Bonus = 500;
            }
            else
            {
                throw new Exception("JobTitle doesn't exist!");
            }
        }
        public override decimal CalculateYearlySalary()
        {
            decimal yearly_salary = base.CalculateYearlySalary();
            return yearly_salary + Bonus;
        }
    }
}
