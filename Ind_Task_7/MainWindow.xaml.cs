using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace Ind_Task_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Person> _people = new List<Person>();
        private static string _jsonFileName = "db.json";

        public MainWindow()
        {
            InitializeComponent();
            DeserializeDB();
        }

        private void DeserializeDB()
        {
            try
            {
                if (File.Exists(_jsonFileName))
                {
                    _people = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(_jsonFileName));
                }

                if (_people.Count > 0)
                {
                    for (int i = 0; i < _people.Count; i++)
                    {
                        if (Person.count < _people[i].ID) { Person.count = _people[i].ID; }
                    }

                }
                ListBoxInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListBoxInitialize()
        {
            try
            {
                lvPeople.Items.Clear();

                foreach (Person p in _people)
                {
                    lvPeople.Items.Add(p);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        #region UI Event Methods

        private void ButAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text == String.Empty || tbLastName.Text == String.Empty)
            {
                Console.Beep();
            }
            else
            {
                Person next = new Person(tbFirstName.Text, tbLastName.Text);

                _people.Add(next);
                lvPeople.Items.Add(next);

                tbFirstName.Text = "First Name";
                tbLastName.Text = "Last Name";
            }
        }

        private void ButRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lvPeople.SelectedItems.Count; i++)
                {
                    _people.Remove(((Person)lvPeople.SelectedItems[i]));
                }

                ListBoxInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result",
                MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.OK, MessageBoxOptions.None);
            }
        }

        private async void ButSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_people.Count > 0)
                {
                    await Task.Run(() =>
                    {
                        _people.Sort();
                    });
                }

                ListBoxInitialize();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result",
                MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.OK, MessageBoxOptions.None);
            }
}

        private void ButSave_Click(object sender, RoutedEventArgs e)
{
    try
    {
        string serialized = JsonConvert.SerializeObject(_people);
        File.WriteAllText(_jsonFileName, serialized);
        MessageBox.Show("Saved successfully", "Result",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.OK, MessageBoxOptions.None);
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Result",
            MessageBoxButton.OK, MessageBoxImage.Error,
            MessageBoxResult.OK, MessageBoxOptions.None);
    }
}

        #endregion
    }
}
