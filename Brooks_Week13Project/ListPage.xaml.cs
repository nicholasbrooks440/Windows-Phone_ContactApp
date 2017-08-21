using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Brooks_Week13Project
{
    public partial class ListPage : PhoneApplicationPage
    {
        string passedJsonFile = "";

        public ListPage()
        {
            InitializeComponent();
        }
        //Return button
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {   //Go back
            NavigationService.GoBack();
        }
        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Desrialize if jsonfile passed
            if (NavigationContext.QueryString.TryGetValue("jsonFile", out passedJsonFile))
            {
                // await readJsonAsync();
               await deserializeJsonAsync();
            }
        }
        //Deserialize
        private async Task deserializeJsonAsync()
        {   //list of Contacts     
            List<Contact> myContacts;
            //json serializer and stream
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Contact>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(passedJsonFile);
            //Put deserialized json into list of contacts
            myContacts = (List<Contact>)jsonSerializer.ReadObject(myStream);

            foreach (var con in myContacts)
            {   //Format string and add to listbox
                string formatContact = String.Format("ID: {0}, {1} {2}, {3}", con.IDNumber, con.FirstName, con.LastName, con.Email);
                contListBox.Items.Add(formatContact);
            }
            //Close stream
            myStream.Close();
        }
    }
}