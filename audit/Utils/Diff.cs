using JsonDiffPatchDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Utils;

public static class Diff
{
    /// <summary>
    /// Get difference between objects:  a and b
    /// </summary>
    /// <param name="a">object a</param>
    /// <param name="b">object b</param>
    /// <returns></returns>
    public static string Get(string a, string b)
    {
        var jdp = new JsonDiffPatch();
        var diff = jdp.Diff(a, b);
        return diff;
    }

    /// <summary>
    /// Patch object a with difference
    /// </summary>
    /// <param name="a">object a</param>
    /// <param name="diff">difference</param>
    /// <returns></returns>
    public static string Patch(string a, string diff)
    {
        var jdp = new JsonDiffPatch();
        var result = jdp.Patch(a, diff);
        return result;
    }

    /// <summary>
    /// Unpatch object b with difference
    /// </summary>
    /// <param name="b">object b</param>
    /// <param name="diff">difference</param>
    /// <returns></returns>
    public static string Unpatch(string b, string diff)
    {
        var jdp = new JsonDiffPatch();
        var result = jdp.Unpatch(b, diff);
        return result;
    }
}
