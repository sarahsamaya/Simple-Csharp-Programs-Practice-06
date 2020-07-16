using Assig5.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Assig5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
        public partial class MainWindow : Window
        {
            private ObservableCollection<Person> people = null;
            public ObservableCollection<Person> DisplayPeople { get => people; set => people = value; }


            PersonList personList;


            Person aPerson = new Person();

            public string pathString;
            public string fileName;

            public MainWindow()
            {
                InitializeComponent();
                CheckIfFileExists();


                personList = new PersonList();
                ReadFromXML();

                DisplayPeople = new ObservableCollection<Person>();

               DataContext = this;

     
            }

            private void CleanFields()
            {
                txtName.Text = string.Empty;
                txtAge.Text = string.Empty;
                txtHours.Text = string.Empty;
                txtNameFilter.Text = string.Empty;
                txtAgeFilter.Text = string.Empty;


            }
            /// These methods below manage the button actions of the form

            //temporarily adds data in the GRID for observation
            private void btnAdd_Click(object sender, RoutedEventArgs e)
            {
                int myIntAge;
                double myDoubleHours;


            if (txtName.Text == "" || txtAge.Text == "" || txtHours.Text == "")
                {
                    MessageBox.Show("Please fill in all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtName.Focus();
                    return;
                }
                else if (!int.TryParse(txtAge.Text, out myIntAge))
                {
                    MessageBox.Show("Invalid age", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtAge.Focus();
                    return;
                }
                else if (!double.TryParse(txtHours.Text, out myDoubleHours))
                {
                    MessageBox.Show("Invalid worked hours", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtHours.Focus();
                    return;
                }
                else
                {
                    aPerson = new Person();
                    aPerson.Name = txtName.Text;
                    aPerson.Age = myIntAge;
                    aPerson.WorkedHours = myDoubleHours;

                    DisplayPeople.Add(aPerson);
                    

                CleanFields();
                }





            }
            //call method to save data in the XML
            private void btnSave_Click(object sender, RoutedEventArgs e)
            {

                   foreach (Person p in DisplayPeople)
                        {
                            personList.Add(p);
                        }

                        WriteToXML();


                     DisplayPeople.Clear();
                   

              }
                        

            //Filtered search using LINQ
            private void btnSearch_Click(object sender, RoutedEventArgs e)
            {

                PersonList l1 = new PersonList();
                l1 = ReadFromXML();

                int myIntAge = 0;

                if (txtNameFilter.Text.Equals("") && txtAgeFilter.Text.Equals(""))
                {
                    MessageBox.Show("Please fill some field to filter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNameFilter.Focus();
                    return;
                }
                else if ((!txtNameFilter.Text.Equals("")) && (!txtAgeFilter.Text.Equals("")))
                {
                    var query = from person in l1.Persons
                                where (person.Name == txtNameFilter.Text &&
                                person.Age == int.Parse(txtAgeFilter.Text))
                                select person;
                    MyPeopleGrid.ItemsSource = query;
                }
                else if (!txtNameFilter.Text.Equals(""))
                {

                    var query = from person in l1.Persons
                                where person.Name == txtNameFilter.Text
                                select person;
                    MyPeopleGrid.ItemsSource = query;

                }
                else if (!(txtAgeFilter.Text.Equals("")) && (int.TryParse(txtAgeFilter.Text, out myIntAge)))
                {

                    var query = from person in l1.Persons
                                where person.Age == myIntAge
                                select person;
                    MyPeopleGrid.ItemsSource = query;
                }
                else
                {
                    MessageBox.Show("Invalid age", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtHours.Focus();
                    return;
                }




            }



            //Search without prm using LINQ
            private void btnDisplay_Click(object sender, RoutedEventArgs e)
            {
            //PersonList l1 = new PersonList();

         

                personList = ReadFromXML();


                var query = from person in personList.Persons
                            select person;

                MyPeopleGrid.ItemsSource = query;


                CleanFields();

        }


            /// These methods below manage the XML data to read, create and list them
            private void CheckIfFileExists()
            {
                // Specify a name fo ryour folder.
                pathString = @".\myFolder";

                // Create a file name for the file you want to create. 
                fileName = System.IO.Path.GetFileName("Person.xml");

                //Checks if file exists, and creates it
                try
                {

                    if (!File.Exists(pathString))
                    {
                        System.IO.Directory.CreateDirectory(pathString);

                        pathString = System.IO.Path.Combine(pathString, fileName);

                    }

                    if (!File.Exists(pathString))
                    {
                        File.CreateText(pathString);

                    }
                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            private PersonList ReadFromXML()
            {

                //PersonList myPersonList = new PersonList();


                try
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(PersonList));

                    StreamReader reader = new StreamReader(pathString);

                if (personList.Persons.Count > 0)
                    personList = (PersonList)serializer.Deserialize(reader);

                    reader.Close();



                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show(e.ToString());
                }

                return personList;


            }

            private void WriteToXML()
            {
                try
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(PersonList));

                    TextWriter writer = new StreamWriter(pathString);
                    serializer.Serialize(writer, personList);
                    writer.Close();


                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            //Converter



        }
    }

