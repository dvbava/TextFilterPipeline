namespace TextFilter.Core;

public sealed class TextFileWordSource : IWordSource
{
    // Streaming words from a text file.
    public IEnumerable<string> ReadTokens(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        if(!File.Exists(path))
        {
            throw new FileNotFoundException("The specified file does not exist.", path);
        }

        // File.ReadLines, that lazily reads lines from the file. Note, we are eating EOL characters.
        foreach (var line in File.ReadLines(path))
        {
            foreach (var word in line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)) // split by all Unicode whitespace characters
            {
                yield return word;
            }

            yield return Environment.NewLine; // yield a newline token to preserve line breaks
        }
    }
}
