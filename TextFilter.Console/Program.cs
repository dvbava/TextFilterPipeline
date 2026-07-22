using TextFilter.Core;
using TextFilter.Core.Filters;
using TextFilter.Core.Pipeline;

namespace TextFilter.Console;

public static class Program
{
	public static int Main(string[] args)
	{
		if (args.Length != 1)
		{
			System.Console.Error.WriteLine("Usage: TextFilter.Console <path-to-input-file>");
			return 1;
		}

        //compose the pipeline of filters.
        var pipeline = new TextFilterPipeline(
		[
            //todo: consider setting can come from configuration file.

            new MiddleCharsFilter(['a', 'e', 'i', 'o', 'u']),
			new MinWordLengthFilter(3),
			new ContainsCharFilter('t')
		]);

		var orchestrator = new TextProcessingOrchestrator(new TextFileWordSource(), pipeline);

        // Process the input file and get the filtered words.
        var words = orchestrator.Process(args[0]);

		foreach (var word in words)
		{
			System.Console.Write($"{word} ");
		}

		return 0;
	}
}
