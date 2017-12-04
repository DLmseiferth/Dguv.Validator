﻿// <copyright file="Check6.cs" company="DATALINE GmbH &amp; Co. KG">
// Copyright (c) DATALINE GmbH &amp; Co. KG. All rights reserved.
// </copyright>
using System;
using System.Linq;

namespace Dguv.Validator.Format.Checks
{
    /// <summary>
    /// Prüfziffernberechnung 6
    /// </summary>
    public class Check6 : ICheckNumberValidator
    {
        /// <summary>
        /// Berechnung der Prüfziffer anhand der Mitgliedsnummer
        /// </summary>
        /// <param name="membershipNumber">Die Mitgliedsnummer</param>
        /// <returns>Die errechnete Prüfziffer</returns>
        public object Calculate(string membershipNumber)
        {
            int calculatedCheckNumber = 0;
            string trimmed = membershipNumber.Trim();
            if (membershipNumber.Length == 11)
            {
                trimmed = trimmed.Substring(1, 6);
                var mgnr_parts = Array.ConvertAll(trimmed.ToCharArray(), c => (int)char.GetNumericValue(c));
                if (trimmed.Length == 6)
                {
                    var f1 = mgnr_parts[1] + mgnr_parts[5];
                    var f2 = mgnr_parts[0] + mgnr_parts[4];
                    var f3 = mgnr_parts[3];
                    var pz1 = (f1 % 10) + (f2 % 10) + (f3 % 10);
                    var fx = Array.ConvertAll((2 * mgnr_parts[3]).ToString().ToCharArray(), c => (int)char.GetNumericValue(c));
                    f3 = fx.Sum();
                    var pz2 = (f1 % 10) + (f3 % 10) + (mgnr_parts[2] % 10);

                    calculatedCheckNumber = Convert.ToUInt16($"{(pz1 % 10).ToString()}{(pz2 % 10).ToString()}");
                }
            }
            return calculatedCheckNumber;
        }

        /// <summary>
        /// Validierung der Prüfziffer in einer Mitgliedsnummer
        /// </summary>
        /// <param name="membershipNumber">Die Mitgliedsnummmer</param>
        /// <returns><code>TRUE</code>, wenn die errechnete und die in der Mitgliedsnummer enthaltene Prüfziffer gleich ist. Sonst <code>FALSE</code></returns>
        public bool Validate(string membershipNumber)
        {
            var originChecknumber = ExtractCheckNumber(membershipNumber);
            var calculatedChecknumber = (int)Calculate(membershipNumber);
            return originChecknumber == calculatedChecknumber;
        }

        private int ExtractCheckNumber(string membershipNumber)
        {
            var mgnr = membershipNumber.Trim();
            return Convert.ToUInt16(mgnr.Substring(mgnr.Length - 2, 2));
        }
    }
}
