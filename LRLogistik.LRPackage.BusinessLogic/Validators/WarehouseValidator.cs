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
    public class WarehouseValidator : AbstractValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(warehouse => warehouse.Description).NotNull().DependentRules(() => {
                RuleFor(warehouse => warehouse.Description).Must(BeAValidDescription);
            });
        }

        public bool BeAValidDescription(string description)
        {
            Regex regex = new Regex(@"^[A-Zäüöß\d][a-zäüöß /\d]*$");

            return regex.IsMatch(description);
        }

    }
}
