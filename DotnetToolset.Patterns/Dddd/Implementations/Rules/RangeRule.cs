﻿using System;
using DotnetToolset.ExtensionMethods;
using DotnetToolset.Patterns.Dddd.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Design;
using DotnetToolset.Patterns.Dddd.Enums;
using Res = DotnetToolset.Patterns.Resources.Literals;

namespace DotnetToolset.Patterns.Dddd.Implementations.Rules
{
	/// <inheritdoc />
	public class RangeRule : IRule
	{
		/// <summary>
		/// Constructor to set the rule value
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public RangeRule(int min, int max)
		{
			Rule = new KeyValuePair<RuleType, object>(RuleType.Range, (min, max));
		}

		/// <summary>
		/// Rule
		/// </summary>
		public KeyValuePair<RuleType, object> Rule { get; set; }

		/// <summary>
		/// Validates data passed using the rule logic
		/// </summary>
		/// <param name="value">Data to be validated</param>
		/// <returns>True if rule passed and an optional error message</returns>
		public (bool isValid, string errorMessage) Validate(object value)
		{
			(int min, int max) ruleValue = ((int min, int max))Rule.Value;

            var castedValue = 0.0;

            if (value == null)
            {
                return (true, Res.b_NoValueToValidate);
            }

			// Value can be float depending of subyacent type, so we must treat it apart.
			if (value.GetType().Name.Contains("Single"))
            {
                castedValue = (float)value;
            }
            else
            {
                castedValue = (int)value;
            }

            if (castedValue >= ruleValue.min && castedValue <= ruleValue.max)
			{
				return (true, null);
			}

			return (false, Res.p_RuleRangeNotPassed.ParseParameters(new object[] { value.ToString(), ruleValue.min.ToString(), ruleValue.max.ToString() }));
		}
	}
}
