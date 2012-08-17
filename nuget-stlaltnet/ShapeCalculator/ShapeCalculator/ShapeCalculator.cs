using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator;

namespace ShapeCalculator
{
	public class ShapeCalculator
	{
		public int GetArea(Rectangle rect)
		{
			return SimpleCalculator.Multiply(rect.Length, rect.Width);
		}

		public int GetPermiter(Rectangle rect)
		{
			return SimpleCalculator.Add(
				SimpleCalculator.Multiply(2, rect.Length),
				SimpleCalculator.Multiply(2, rect.Width));
		}
	}
}
