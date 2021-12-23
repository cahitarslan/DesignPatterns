using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee cahit = new Employee { Name = "Cahit Arslan" };
            Employee mucahit = new Employee { Name = "Mücahit Arslan" };
            cahit.AddSubordinate(mucahit);

            Employee mikail = new Employee { Name = "Mikail Arslan" };
            cahit.AddSubordinate(mikail);

            Employee ahmet = new Employee { Name = "Ahmet Yılmaz" };
            mucahit.AddSubordinate(ahmet);

            Contractor ali = new Contractor { Name = "Ali Veli" };
            mikail.AddSubordinate(ali);


            Console.WriteLine(cahit.Name);
            foreach (Employee manager in cahit)
            {
                Console.WriteLine("  {0}", manager.Name);
                foreach (IPerson employee in manager)
                {
                    Console.WriteLine("    {0}", employee.Name);
                }

            }

            Console.ReadKey();
        }
    }

    interface IPerson
    {
        string Name { get; set; }
    }

    class Contractor : IPerson
    {
        public string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson>
    {
        List<IPerson> _subordinates = new List<IPerson>();

        public void AddSubordinate(IPerson person)
        {
            _subordinates.Add(person);
        }

        public void RemoveSubordinate(IPerson person)
        {
            _subordinates.Remove(person);
        }

        public IPerson GetSubordinate(int index)
        {
            return _subordinates[index];
        }


        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in _subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
