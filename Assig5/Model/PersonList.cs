using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace Assig5.Model
{
    [XmlRoot("PersonList")]

    public class PersonList: IDisposable
    {
        [XmlArray("Persons")]
        [XmlArrayItem("Person", typeof(Person))]
        public List<Person> Persons { get; set; }

        public PersonList()
        {
            Persons = new List<Person>();
        }

        public void Add(Person tree)
        {
            Persons.Add(tree);
        }
        public void Remove(Person tree)
        {
            Persons.Remove(tree);
        }

        public int Count()
        {
            return Persons.Count();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Person this[int i]
        {
            get { return Persons[i]; }
        }

        public void Sort()
        {
            Persons.Sort();
        }

        public void Clear()
        {
            Persons.Clear();
        }
    }
}
