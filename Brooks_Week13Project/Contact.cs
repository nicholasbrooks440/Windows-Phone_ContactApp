using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Brooks_Week13Project
{
    [DataContract] //serialize
    class Contact
    {        
        [DataMember]
        private double iDNum;
        [DataMember]
        private string firstName;
        [DataMember]
        private string lastName;

        [DataMember]
        public string Email{get; set;} //Short notation
            
        [DataMember]
        public double IDNumber // Long notation
        {
            get { return iDNum; }
            set { iDNum = value;}
        }
        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
  
        //Overloaded Constructor
        public Contact(double id, string fName, string lName, string emailAddress)
        {
            IDNumber = id;
            FirstName = fName;
            LastName = lName;
            Email = emailAddress;
        }
    }
}
