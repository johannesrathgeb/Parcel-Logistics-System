using FluentValidation;
using System.Text.RegularExpressions; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class Recipient
    {

        public string Name { get; set; }


        public string Street { get; set; }


        public string PostalCode { get; set; }


        public string City { get; set; }


        public string Country { get; set; }
    }

    public class RecipientValidator : AbstractValidator<Recipient>
    {
        public RecipientValidator()
        {
            RuleFor(parcel => parcel).NotNull();

            RuleFor(parcel => parcel.Country).Matches(@"^(Austria||Österreich)$").DependentRules(() => {
                RuleFor(parcel => parcel.PostalCode).Must(BeAValidPostcode);
                RuleFor(parcel => parcel.Street).Must(BeAValidStreet);
                RuleFor(parcel => parcel.City).Must(BeAValidCity);
            });

        }

        public bool BeAValidPostcode(string postcode)
        {
            Regex regex = new Regex(@"A-\d{4}"); 

            return regex.IsMatch(postcode);
        }

        public bool BeAValidStreet(string street)
        {
            Regex regex = new Regex(@"[a-zA-Zäüöß]+\s([A-Za-z0-9äüöß]+(/[A-Za-z0-9äüöß]+){0,1})");

            return regex.IsMatch(street);
        }

        public bool BeAValidCity(string city)
        {
            Regex regex = new Regex(@"^[A-Zäüöß][a-zäüöß /]*$");

            return regex.IsMatch(city);
        }

    }



}
