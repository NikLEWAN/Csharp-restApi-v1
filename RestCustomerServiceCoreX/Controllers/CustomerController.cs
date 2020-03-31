/*
 * RestCustomerServiceCoreX.Controllers
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2018.09.10
 * Copyright 2018 by Michael Claudius
 * Revised ..
 * All rights reserved
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestCustomerServiceCoreY.Model;

namespace RestCustomerServiceCoreY.Controllers

//https://localhost:44387/api/customer using IISExpress
//https://localhost:5001/api/customer RestCustomerService local

{
    [Route("api/[controller]")]
    //[EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public static int nextId = 0;

        private static List<Customer> cList = new List<Customer>()
        {    new Customer("Michael", "Kejser", 1961),
             new Customer("Ebbe", "Flod", 1968),
             new Customer("Peter", "Ulv", 1958),
             new Customer("Anders", "And", 1931),
             new Customer("Søren", "Kierkegaard", 1958),
             new Customer("King", "Khan", 1975)
        };



        // GET: api/Customer
        /// <summary>
        /// Base Uri Rest GET - List all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Customer> Get()
        {
            return cList;// 
        }

        // GET: api/Customer/5
        //[HttpGet("{id}", Name = "Get")] changed uri to below
        /// <summary>
        /// Uri ID Parameter - List specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        // Url = api/customer/{id}
        [Route("{id}")] 
        public Customer GetCustomer(int id)
        {   // DOES  work

          Response.StatusCode = (int)HttpStatusCode.OK; //200  The message for the HttpResponse action

            Customer c = cList.FirstOrDefault(customer => customer.ID == id);
            //Set statuscode of response
            if (c == null) Response.StatusCode = (int)HttpStatusCode.NotFound;
            // Alternatively 
            // if (c == null) Response.StatusCode = 404;
            //Any number can be used for type casting, even customized numbers like 420

            return c;
        }

        /* MY PART */
        // GET: api/Customer/name/"some name"
        /// <summary>
        /// Uri Contains "name"
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet("name/{x}")]
        public IEnumerable<Customer> GetFromSubstring(String x)
        {
            return cList.FindAll(i => i.FirstName.Contains(x));
        }

        // GET: api/Customer/year/after/1995
        /// <summary>
        /// Find customers born after x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet("year/after/{x}")]
        public IEnumerable<Customer> GetYearAfter(int x)
        {
            return cList.FindAll(i => i.Year >= x);
        }

        // GET: api/Customer/year/before/1995
        /// <summary>
        /// Find Customers born before x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [HttpGet("year/before/{x}")]
        public IEnumerable<Customer> GetYearBefore(int x)
        {
            return cList.FindAll(i => i.Year <= x);
        }
        // ex https://localhost:44313/api/customer/year/both/1975/1960
        // returns anything between both values
        /// <summary>
        /// Return customers between before born year and after born year
        /// </summary>
        /// <param name="x">Before Year</param>
        /// <param name="y">After Year</param>
        /// <returns></returns>
        [HttpGet("year/both/{x}/{y}")]
        public IEnumerable<Customer> GetYearBeforeAfter(int x, int y)
        {
            return cList.FindAll(i => i.Year <= x && i.Year >= y);
        }


        /* MY PART */

        // GET: api/Customer/5
        /// <summary>
        /// Special ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("special/{id}", Name = "Get1")]
        public ActionResult GetCustomer1(int id)
        {   
            Customer c = cList.FirstOrDefault(customer => customer.ID == id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        // POST: api/Customer
        /// <summary>
        /// Post to Customers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        //[EnableCors("AllowSpecifOrigin")]
        public Customer InsertCustomer([FromBody] Customer customer)
        {
            customer.ID = CustomerController.nextId++;
            cList.Add(customer);
            return customer;
        }


        // PUT: api/Customer/5
        /// <summary>
        /// Put into Customers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Customer UpdateCustomer(int id, [FromBody] Customer customer)
        {
            //Find customer in the list replace old data with new; i.e. original ID is not changed
            //The original position in the list is kept
            Customer c = GetCustomer(id); 
            if (c == null) return null;
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;
            c.Year = customer.Year;
            return GetCustomer(id);

            //Or insert at index specified
            //Customer c = GetCustomer(id); 
            //if (c == null) return null;
            //cList.Insert(cList.IndexOf(c), customer);
            //
            //Or delete and insert new customer with the old ID
            //The position in the list is changed to the be the last one
            //Customer oldCustomer = DeleteCustomer(id);
            //if (oldCustomer != null)
            //{
            //    customer.ID = oldCustomer.ID;
            //    cList.Add(customer);
            //}
            //return GetCustomer(id);

        }

        // DELETE: api/Customer/5
        //[HttpDelete("{id}")]
        /// <summary>
        /// Delete Customer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        //[DisableCors]
        public Customer DeleteCustomer(int id)
        {
            Customer c = GetCustomer(id);
            if (c == null || c.ID < 3) return null;
            cList.Remove(c);
            return c;
        }

        // NEW URIS
    }
}
