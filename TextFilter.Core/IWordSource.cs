namespace TextFilter.Core;

public interface IWordSource
{
    IEnumerable<string> ReadTokens(string path);
}
