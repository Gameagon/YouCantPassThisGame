using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UtilityEditor
{
	public class ShowIfAttribute : PropertyAttribute
	{
		public string conditionPropertyName { get; private set; }
		public bool readonlyInstead { get; private set; }

		public ShowIfAttribute(string conditionPropertyName, bool readonlyInstead = false)
		{
			this.conditionPropertyName = conditionPropertyName;
			this.readonlyInstead = readonlyInstead;
		}
	}

	public class HideIfAttribute : PropertyAttribute
	{
		public string conditionPropertyName { get; private set; }
		public bool readonlyInstead { get; private set; }

		/// <summary>
		/// Only draws the field only if a condition is met.
		/// </summary>
		/// <param name="comparedPropertyName">The name of the property that is being compared (case sensitive).</param>
		/// <param name="comparedValue">The value the property is being compared to.</param>
		/// <param name="comparisonType">The type of comparison the values will be compared by.</param>
		/// <param name="disablingType">The type of disabling that should happen if the condition is NOT met. Defaulted to DisablingType.DontDraw.</param>
		public HideIfAttribute(string conditionPropertyName, bool readonlyInstead = false)
		{
			this.conditionPropertyName = conditionPropertyName;
			this.readonlyInstead = readonlyInstead;
		}
	}
}
