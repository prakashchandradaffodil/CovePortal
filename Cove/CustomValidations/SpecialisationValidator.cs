using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cove.Web.CustomValidations
{
    public class SpecialisationValidator:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var arr = (string[])value;
                if (arr.Count() == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //public override IsValid(object value, ValidationContext validationContext)
        //{

        //}
    }
}
