using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.DistanceCalculation
{
	/// <summary>
	/// Defines interface for types that calculate the minimum distance between two strings.
	/// </summary>
	public interface IStringDistanceCalculator
	{
		int CalculateDistance([NotNull]string first, [NotNull]string second);
	}
}