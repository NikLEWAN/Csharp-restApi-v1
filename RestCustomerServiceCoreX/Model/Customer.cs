


/*
 * RestCustomerServiceCoreX.Customer
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2018.09.10
 * Copyright 2018 by Michael Claudius
 * Revised ..
 * All rights reserved
 */

using RestCustomerServiceCoreY.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RestCustomerServiceCoreY.Model
{


    //Datacontract not necessary as we are using JSON
    //Why use a Datacontract ?
    //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/data-member-order
    //How to order data member?
    //https://stackoverflow.com/questions/4836683/when-to-use-datacontract-and-datamember-attributes

    [DataContract]
    public class Customer
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public int Year { get; set; }


        public Customer()
        { //Start data generation
        }

        public Customer(String firstName)
        {
            this.ID = CustomerController.nextId++;
            this.FirstName = firstName;
        }

        public Customer(String firstName, String lastName, int year)
        {
            this.ID = CustomerController.nextId++; //I also have this in AddCustomer, maybe dont have it two times
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
        }

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Year)}: {Year}";

        }

    }
}


       