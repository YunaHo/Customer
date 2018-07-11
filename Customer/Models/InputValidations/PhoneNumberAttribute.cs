using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Customer.Models.InputValidations
{
    sealed public class PhoneNumberAttribute : ValidationAttribute
    {
        readonly string _PhoneType;
        public string PhoneType
        {
            get { return _PhoneType; }
        }

        public PhoneNumberAttribute(string PhoneType)
        {
            _PhoneType = PhoneType;
        }

        public override bool IsValid(object value)
        {
            var phoneNumber = (String)value;
            bool result = true;
            if (this.PhoneType!=null && !string.IsNullOrEmpty(phoneNumber))
            {
                result = PhoneMask(this.PhoneType,phoneNumber);
            }
            return result;
        }

        internal bool PhoneMask(string PhoneType,string phoneNumber)
        {
            bool result = false;
            switch(PhoneType)
            {
                case "phone":
                    result = Regex.IsMatch(phoneNumber.ToString(), @"^09(\d{2}-)?\d{6}$");
                    break;
                case "tel":
                    result = Regex.IsMatch(phoneNumber.ToString(), @"^(\d{2}-)?\d{6,8}$");
                    break;
            }
            return result;
        }


    }
}