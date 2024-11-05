using MTecl.GraphQlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BwApiClient.Model.Data
{
    [GqlTypeFragment("Company")]
    public interface ICompany
    {
        /// <summary>
        /// Internal company ID - if registered.
        /// </summary>
        string companyid { get; set; }

        /// <summary>
        /// Legal registration ID.
        /// </summary>
        string company_reg_id { get; set; }
        

        /// <summary>
        /// Company's name including legal form.
        /// </summary>
        string company_name { get; set; }

        /// <summary>
        /// Set of predefined addresses.
        /// </summary>
        List<Address> registered_address { get; set; }

        /// <summary>
        /// E-mail address.
        /// </summary>
        string email { get; set; }

        /// <summary>
        /// Contact person name.
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Contact person surname.
        /// </summary>
        string surname { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        string phone { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN).
        /// </summary>
        string vat_id { get; set; }

        /// <summary>
        /// Secondary VAT ID or tax identification number (TIN).
        /// </summary>
        string vat_id2 { get; set; }
    }

    [GqlTypeFragment("Person")]
    public interface IPerson
    {
        /// <summary>
        /// Internal person ID.
        /// </summary>
        string personid { get; set; }

        /// <summary>
        /// Person's name.
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        string surname { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        string phone { get; set; }

        /// <summary>
        /// E-mail address.
        /// </summary>
        string email { get; set; }

        /// <summary>
        /// Set of predefined addresses (for repeated usage).
        /// </summary>
        List<Address> registered_address { get; set; }
    }

    [GqlTypeFragment("UnauthenticatedEmail")]
    public interface IUnauthenticatedEmail
    {
        /// <summary>
        /// E-mail address.
        /// </summary>
        string email { get; set; }

        /// <summary>
        /// Person's name.
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        string surname { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        string phone { get; set; }
    }

    public class OrderCreator : IPerson, IUnauthenticatedEmail
    {
        public string __typename { get; set; }

        /// <summary>
        /// E-mail address.
        /// </summary>        
        public string email { get; set; }

        /// <summary>
        /// Person's name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Person's surname.
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Contact phone.
        /// </summary>
        public string phone { get; set; }

        [Gql(IsAliasFor = "id")]
        public string personid { get; set; }

        /// <summary>
        /// Set of predefined addresses (for repeated usage).
        /// </summary>        
        public List<Address> registered_address { get; set; }
    }

    public class Customer : OrderCreator, IPerson, ICompany, IUnauthenticatedEmail
    {        

        [Gql(IsAliasFor = "id")]
        public string companyid { get; set; }
                

        /// <summary>
        /// Legal registration ID.
        /// </summary>
        [Gql(IsAliasFor = "company_id")]
        public string company_reg_id { get; set; }

        /// <summary>
        /// Company's name including legal form.
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN).
        /// </summary>
        public string vat_id { get; set; }

        /// <summary>
        /// VAT ID or tax identification number (TIN) #2.
        /// </summary>
        public string vat_id2 { get; set; }             
    }
}
