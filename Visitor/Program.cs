using System;
using System.Collections.Generic;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager cahit = new Manager { Name = "Cahit", Salary = 1000 };
            Manager salih = new Manager { Name = "Salih", Salary = 900 };

            Worker ayse = new Worker { Name = "Ayşe", Salary = 800 };
            Worker ali = new Worker { Name = "Ali", Salary = 800 };

            cahit.Subordinates.Add(salih);
            salih.Subordinates.Add(ayse);
            salih.Subordinates.Add(ali);


            OrganisationalStructure organisationalStructure = new OrganisationalStructure(cahit);

            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();

            organisationalStructure.Accept(payrollVisitor);
            organisationalStructure.Accept(payriseVisitor);


            Console.ReadKey();
        }
    }

    class OrganisationalStructure
    {
        public EmployeeBase Employee;

        public OrganisationalStructure(EmployeeBase firstEmployee)
        {
            Employee = firstEmployee;
        }

        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }
    }

    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);
        public string Name { get; set; }
        public Decimal Salary { get; set; }
    }

    class Manager : EmployeeBase
    {
        public List<EmployeeBase> Subordinates { get; set; }

        public Manager()
        {
            Subordinates = new List<EmployeeBase>();
        }

        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);

            foreach (var employee in Subordinates)
            {
                employee.Accept(visitor);
            }
        }
    }

    class Worker : EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }

    abstract class VisitorBase
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }

    class PayrollVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} pain {1}", worker.Name, worker.Salary);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} pain {1}", manager.Name, manager.Salary);
        }
    }

    class PayriseVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to {1}", worker.Name, worker.Salary * (decimal)1.1);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased to {1}", manager.Name, manager.Salary * (decimal)1.2);
        }
    }
}
