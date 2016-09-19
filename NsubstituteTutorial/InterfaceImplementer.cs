using System;
using NSubstitute;
using System.Diagnostics;
using NUnit.Framework;

class InterfaceImplementer : ICalculator
{
	static void Main()
	{
		InterfaceImplementer iImp = new InterfaceImplementer();

		// Substitute instance for interface type
		var cal = Substitute.For<ICalculator>();
		cal.Add(1, 2).Returns(3);
		Assert.That(cal.Add(1, 2), Is.EqualTo(3));


	}

}
