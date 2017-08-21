using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Brooks_Week13Project.Resources;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using System.IO;

namespace Brooks_Week13Project
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string JSONFILENAME = "ContactList.json";
        List<Contact> contactList = new List<Contact>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
       
        private async void saveContButton_Click(object sender, RoutedEventArgs e)
        {
            //“Save Contact” button will perform the following tasks:
            //a.Create an instance of the class Contact, and set its data values using the inputs from the textboxes.
            double number;
            double idNumber;
            if (Double.TryParse(iDtextBox.Text, out number))
                idNumber = number;
            else
                idNumber = 0000;//default
            //create contact with user info
            Contact myContact = new Contact(idNumber, fNameTextBox.Text, lNameTextBox.Text, emailTextBox.Text);

            //b.Add the Contact to a List of Contacts.            
            contactList.Add(myContact);
            //Write to json
            await writeJsonAsync(contactList);

            //d.Clear the input textboxes.
            resetTextBoxes();
        }
     
        private async Task writeJsonAsync(List<Contact> contactList)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(List<Contact>));
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                    JSONFILENAME,
                    CreationCollisionOption.ReplaceExisting))
                {
                    serializer.WriteObject(stream, contactList);
                    stream.Close();
                }
            }
            
            catch
            {
                throw new NotImplementedException();
            }          
         }

        private void listContButton_Click(object sender, RoutedEventArgs e)
        {
            //Show ListPage and pass JSON
            NavigationService.Navigate(new Uri("/ListPage.xaml?jsonFile=" + JSONFILENAME, UriKind.Relative));
        }

        private void resetFormButton_Click(object sender, RoutedEventArgs e)
        {   //Clear the input textboxes.
            resetTextBoxes();
        }

        private void resetTextBoxes()
        {   //Clear text boxes
            iDtextBox.Text = "";
            fNameTextBox.Text = "";
            lNameTextBox.Text = "";
            emailTextBox.Text = "";
        }       
    }
}