using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace StringVsStringBuilderDemo
{
	//Command zum Ausführen
	//dotnet run --project StringVsStringBuilderDemo.csproj -c Release 

	//Aktiviert die Speicher Diagnose 
	[MemoryDiagnoser]
	public class Demo
	{
		private int NumberOfRuns = 1000;

		//Einer Zeichenkette wird mit StringBuilder ein weiters Zeichen hinzugefügt
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

		//Einem String wird n-Mal ein weiters Zeichen hinzugefügt
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

		//Ein Teil der Zeichenkette wird vom String mit SubString extrahiert
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

		//Ein Teil der Zeichenkette wird mithilfe von StringBuilder extrahiert
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

		//Mehrere Zeichenketten werden zu einem String zusammengesetzt
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

		//Mehrere Zeichenketten werden mit dem StringBuilder zusammengesetzt
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
			//Aufruf für den Benchmark
			var summary = BenchmarkRunner.Run<Demo>();
		}
	}
}