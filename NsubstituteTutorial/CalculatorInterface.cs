using System;


public interface ICalculator
{
	int Add(int a, int b);
	string Mode { get; set; }
	event EventHandler PoweringUp;
}