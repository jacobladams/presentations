using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeCalculator
{
	public class Rectangle
	{
		public int Width { get; set; }
		public int Length { get; set; }

		public Rectangle(int width, int length)
		{
			Width = width;
			Length = length;
		}
	}
}
