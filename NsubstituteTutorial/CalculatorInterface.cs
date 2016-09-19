using System;
using NSubstitute;
using System.Diagnostics;
using NUnit.Framework;


public interface ICalculator
{
	int Add(int a, int b);
	string Mode { get; set; }
	event EventHandler PoweringUp;


}