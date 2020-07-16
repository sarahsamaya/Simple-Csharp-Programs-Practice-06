using System;
using System.Collections.Generic;
using System.Text;

namespace Assig5.Model
{
    public class Person: IPerson
    {
        private string name;
        private int age;
        private double workedHours;

        public Person() { }

        public Person(string name, int age, double hours)
        {
            this.name = name;
            this.age = age;
            this.workedHours = hours;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Age
        {
            get => age;
            set => age = value;
        }
        public double WorkedHours
        {
            get => workedHours;
            set => workedHours = value;

        }
        double IPerson.workedHours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string ToString()
        {
            return string.Format("Person is {0} of Age {1} and worked {2} hours", name, age, workedHours);
        }
        public int CompareTo(IPerson other)
        {
            return age.CompareTo(other.Age);
        }
        public string PrepareString()
        {
            string fullType = GetType().ToString();
            string[] arrType = fullType.Split('.');
            return string.Format("{0} is {1} of Age {2} and wors {3} hours", arrType[arrType.Length - 1], name, age, workedHours);
        }


    }


}
