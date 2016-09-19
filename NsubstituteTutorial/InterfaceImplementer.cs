using System;
using NSubstitute;
using System.Diagnostics;
using NUnit.Framework;

class InterfaceImplementer 
{
	static void Main()
	{
		// Substitute instance for interface type
		var cal = Substitute.For<ICalculator>();

		// Testing functions
		// ----------Testing "int Add(int a, int b)"------------
		// Substitute return a value for a call
		cal.Add(1, 2).Returns(3);
		// Make sure the return value from cal.Add81,2) is correct
		Assert.That(cal.Add(1, 2), Is.EqualTo(3));

		// Check subsititute received a call, and did not receive others
		cal.Add(1, 2);
		// Make sure cal has received Add(1,2) calls
		cal.Received().Add(1, 2);
		// Make sure cal did not redeive this Add(1,7) calls
		cal.DidNotReceive().Add(1, 7);

		// If replace Received() with "cal.Received().Add(1, 3);"
		// When cal.Received assertation fails:
		//NSubstitute.Exceptions.ReceivedCallsException : Expected to receive a call matching:
		//Add(1, 3)
		//Actually received no matching calls.
		//Received 2 non - matching calls(non - matching arguments indicated with '*' characters):
		//Add(1, *2*)
		//Add(1, *2*)

		// Testing Porperties
		// -------------Testing string Mode { get; set; }-----------
		// ---Testing Get method
		cal.Mode.Returns("DEC");
		Assert.That(cal.Mode, Is.EqualTo("DEC"));

		// ----Testing Set method
		cal.Mode = "Hex";
		Assert.That(cal.Mode, Is.EqualTo("Hex"));

		// Argment matching for setting return values and asserting a call was received
		cal.Add(10, -5);
		// Check type of the second argument of Add(), 
		// Same as: Pattern Match
		cal.Received().Add(10, Arg.Any<int>());
		// Check value of the second argument of Add(), 
		cal.Received().Add(10, Arg.Is<int>(x => x < 0));

		// Use argument matching as well as passing a function to Returns() to get
		// 	some more behaviour out of the subsititute.
		// Checking both value and type for two argument as well as result accuracy.
		cal
			.Add(Arg.Any<int>(), Arg.Any<int>())
			.Returns(x => (int)x[0] + (int)x[1]);
		Assert.That(cal.Add(5, 10), Is.EqualTo(15));

		// Results() can also be called with multiple arguments to setup a sequence of return values
		cal.Mode.Returns("HEX", "DEC", "BIN");
		Assert.That(cal.Mode, Is.EqualTo("HEX"));
		Assert.That(cal.Mode, Is.EqualTo("DEC"));
		Assert.That(cal.Mode, Is.EqualTo("BIN"));

		// Raise events on subsitutes
		// Use a variable to store if cal.PoweringUp is true of false
		bool eventWasRaise = false;
		cal.PoweringUp += (sender, args) => eventWasRaise = true;
		//Console.WriteLine("PoweringUp Value: " + eventWasRaise);
		// Raise event handler to change cal.PoweringUp from faluse to true
		cal.PoweringUp += Raise.Event();
		Assert.That(eventWasRaise);

		            




	}

}
