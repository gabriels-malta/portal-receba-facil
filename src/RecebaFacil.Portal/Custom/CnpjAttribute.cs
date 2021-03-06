﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecebaFacil.Portal.Custom
{
    public class CnpjAttribute : ValidationAttribute
    {
        public CnpjAttribute()
        {
            ErrorMessage = "CNPJ inválido";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueValidLength = 14;
                var maskChars = new[] { ".", "-", "/" };
                var multipliersForFirstDigit = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multipliersForSecondDigit = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                var mod11 = new Mod11();
                var isValid = mod11.IsValid(value.ToString(), valueValidLength, maskChars, multipliersForFirstDigit, multipliersForSecondDigit);

                if (!isValid)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return null;
        }
    }
    public class Mod11
    {
        public bool IsValid(
            string value,
            int valueValidLength,
            string[] maskChars,
            int[] multipliersForFirstDigit,
            int[] multipliersForSecondDigit)
        {
            var valueWithoutMask = GetValueWithoutMask(value, maskChars);

            var isInvalid =
                IsInvalidLength(valueWithoutMask, valueValidLength) ||
                IsNotNumbersOnly(valueWithoutMask) ||
                IsNotInvalidSequence(value) ||
                IsInvalidMod11(multipliersForFirstDigit, multipliersForSecondDigit, valueWithoutMask);

            return !isInvalid;
        }

        private static string GetValueWithoutMask(
            string value,
            string[] maskChars)
        {
            foreach (var maskChar in maskChars)
            {
                value = value.Replace(maskChar, string.Empty);
            }
            return value;
        }

        private static bool IsInvalidLength(
            string value,
            int valueValidLength)
        {
            return value.Length != valueValidLength;
        }

        private static bool IsNotNumbersOnly(
            string value)
        {
            return !Regex.IsMatch(value, @"\d+");
        }

        private bool IsNotInvalidSequence(
            string value)
        {
            var allCharsAreEqual = value.Distinct().Count() == 1;
            return allCharsAreEqual;
        }

        private static bool IsInvalidMod11(
            int[] multipliersForFirstDigit,
            int[] multipliersForSecondDigit,
            string value)
        {
            var firstDigit = GetFirstDigit(multipliersForFirstDigit, value);
            var secondDigit = GetSecondDigit(multipliersForSecondDigit, value, firstDigit);
            var expectedSufix = string.Concat(firstDigit, secondDigit);
            var isInvalid = !value.EndsWith(expectedSufix);
            return isInvalid;
        }

        private static int GetFirstDigit(
            int[] multipliers,
            string value)
        {
            var valueToWork = value.Substring(0, multipliers.Length);
            var sum = multipliers
                .Select((d, i) => new
                {
                    Value = int.Parse(valueToWork[i].ToString()),
                    Multiplier = multipliers[i]
                })
                .Sum(d => d.Value * d.Multiplier);
            var rest = sum % 11;
            var firstDigit = rest < 2 ? 0 : 11 - rest;
            return firstDigit;
        }

        private static int GetSecondDigit(
            int[] multipliers,
            string value,
            int firstDigit)
        {
            var valueToWork = string.Concat(value.Substring(0, multipliers.Length - 1), firstDigit);
            var sum = multipliers
                .Select((d, i) => new
                {
                    Value = int.Parse(valueToWork[i].ToString()),
                    Multipler = d
                })
                .Sum(d => d.Value * d.Multipler);
            var rest = sum % 11;
            var secondDigit = rest < 2 ? 0 : 11 - rest;
            return secondDigit;
        }
    }
}
