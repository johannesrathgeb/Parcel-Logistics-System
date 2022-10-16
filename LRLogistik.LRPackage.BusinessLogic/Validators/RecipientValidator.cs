using FluentValidation;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Validators
{
    public class RecipientValidator : AbstractValidator<Recipient>
    {
        public RecipientValidator()
        {
            RuleFor(recipient => recipient).NotNull();

            RuleFor(recipient => recipient.Country).Matches(@"^(Austria||Österreich)$").DependentRules(() => {

                RuleFor(recipient => recipient.PostalCode).NotNull().DependentRules(() => {
                    RuleFor(recipient => recipient.PostalCode).Must(BeAValidPostcode);
                });

                RuleFor(recipient => recipient.Street).NotNull().DependentRules(() => {
                    RuleFor(recipient => recipient.Street).Must(BeAValidStreet);
                });

                RuleFor(recipient => recipient.City).NotNull().DependentRules(() => {
                    RuleFor(recipient => recipient.City).Must(BeAValidCity);
                });
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
