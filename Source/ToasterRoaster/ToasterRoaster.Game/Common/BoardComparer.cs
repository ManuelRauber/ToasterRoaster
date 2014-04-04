using System;

namespace ToasterRoaster.Game.Common
{
	public class BoardComparer
	{
		/// <summary>
		/// This method returns the accuracy of the redrawn pattern in percent.
		/// </summary>
		/// <param name="array1">One byte array containing either the user-drawn or the computer-drawn pattern.</param>
		/// <param name="array2">One byte array containing either the user-drawn or the computer-drawn pattern.</param>
		/// <returns>The accuracy of the redrawn pattern in percent</returns>
		public double GetAccuracyInPercent(bool[,] array1, bool[,] array2)
		{
			var totalCells = array1.GetLength(0) * array1.GetLength(1);
			var matchingCells = 0d;

			if (!(array1.GetLength(0) == array2.GetLength(0) && array1.GetLength(1) == array2.GetLength(1)))
			{
				throw new InvalidOperationException("The bounds of the arrays do not match.");
			}

			for (var xPosition = 0; xPosition < array1.GetLength(0); xPosition++)
			{
				for (var yPosition = 0; yPosition < array1.GetLength(0); yPosition++)
				{
					if (array1[xPosition, yPosition] == array2[xPosition, yPosition])
					{
						matchingCells++;
					}
				}
			}

			return (matchingCells / totalCells * 100);
		}
	}
}
