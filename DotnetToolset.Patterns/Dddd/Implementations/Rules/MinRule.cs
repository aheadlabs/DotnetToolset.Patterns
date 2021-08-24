﻿using DotnetToolset.ExtensionMethods;
using DotnetToolset.Patterns.Dddd.Interfaces;
using System.Collections.Generic;
using Res = DotnetToolset.Patterns.Resources.Literals;

namespace DotnetToolset.Patterns.Dddd.Implementations.Rules
{
	/// <inheritdoc />
	public class MinRule : IRule
	{
		/// <summary>
		/// Constructor to set the rule value
		/// </summary>
		/// <param name="min"></param>
		public MinRule(int min)
		{
			Rule = new KeyValuePair<RuleType, object>(RuleType.Min, min);
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
			if (value != null && (int)value >= (int)Rule.Value)
			{
				return (true, null);
			}

			return (false, Res.p_RuleMinNotPassed.ParseParameters(new object[] { value.ToString(), (int)value }));
		}
	}
}