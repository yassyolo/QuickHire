using Microsoft.EntityFrameworkCore;

namespace QuickHire.Infrastructure.Persistence.EFHelpers;

public static class SqlFunctions
{
    [DbFunction("SOUNDEX", IsBuiltIn = true)]
    public static string Soundex(string input)
    {       
        throw new NotSupportedException();
    }
}
