using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace StringVsStringBuilderDemo
{
	//dotnet run --project StringVsStringBuilderDemo.csproj -c Release 

	//Aktiviert die Speicher Diagnose 
	[MemoryDiagnoser]
	//Ordnet die Ergebnisse
	[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
	//Fügt eine Rangspalte zur ausgabe der Ergebnise hinzu
	[RankColumn]
	public class Demo 
	{
		private int NumberOfRuns = 1000;

		// Benchmark, der die Leistung der Zeichenkettenverkettung mit StringBuilder vergleicht
		[Benchmark]
		public string StringConcatWithSB()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < NumberOfRuns; i++)
			{
				sb.Append("Hello World" + i);
			}
			return sb.ToString();
		}

		// Benchmark, der die Leistung der Zeichenkettenverkettung unter Verwendung des + Operators vergleicht
		[Benchmark]
		public string StringConcatWithString()
		{
			string str = "";
			for (int i = 0; i < NumberOfRuns; i++)
			{
				str += "Hello World" + i;
			}
			return str;
		}

		// Benchmark, der die Leistung der Teilzeichenkettenextraktion durch Verkettung vergleicht
		[Benchmark]
		public string ExtractStringUsingSubstring()
		{
			const string str = "This is a sample text";
			string result = "";
			for (int i = 0; i < NumberOfRuns; i++)
			{
				result += str.Substring(0, 10);
			}
			return result;
		}

		// Benchmark, der die Leistung der Teilzeichenkettenextraktion unter Verwendung von StringBuilder vergleicht
		[Benchmark]
		public string ExtractStringUsingAppend()
		{
			const string str = "This is a sample text";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < NumberOfRuns; i++)
			{
				stringBuilder.Append(str, 0, 10);
			}
			return stringBuilder.ToString();
		}

		// Benchmark, der die Leistung der Zeichenkettenverkettung unter Verwendung von string.Join vergleicht
		[Benchmark]
		public string JoinStringsUsingStringJoin()
		{
			string result = "";
			for (int i = 0; i < NumberOfRuns; i++)
			{
				result += string.Join("Hello", ' ', "World");
			}
			return result;
		}

		// Benchmark, der die Leistung der Zeichenkettenverkettung unter Verwendung von StringBuilder.AppendJoin vergleicht
		[Benchmark]
		public string JoinStringsUsingAppendJoin()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < NumberOfRuns; i++)
			{
				stringBuilder.AppendJoin("Hello", ' ', "World");
			}
			return stringBuilder.ToString();
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<Demo>();

		}
	}
}
