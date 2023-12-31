using System;
using System.Collections.Specialized;
using System.Diagnostics;

/// <summary>
/// Provides methods for more advanced parsing of command line parameters.
/// </summary>
/// <remarks>
/// A parameter that has no associated value returns an empty string, rather
/// than null which is also used for non-existing values. (See http://msdn.microsoft.com/en-us/library/4ba5htte.aspx)
/// </remarks>
public class CommandlineParser
{
    private NameValueCollection m_nvcParams;
    private string[] m_sArgs;
    private string[] m_sParamIndicators = new string[] { "/", "-" };
    private char[] m_sValueIndicators = new char[] { ':', '=' };

    /// <summary>
    /// Initializes a new instance specifying a string array of parameters.
    /// </summary>
    /// <param name="args">
    /// A string array of parameters as provided by void Main()
    /// </param>
    public CommandlineParser(string[] args)
    {
        m_sArgs = args;
        m_nvcParams = new NameValueCollection();
        Parse(args);
    }

    /// <summary>
    /// Returns the internally used NameValueCollection for storing the
    /// parameters and their values.
    /// </summary>
    public NameValueCollection Items
    {
        get { return m_nvcParams; }
    }

    /// <summary>
    /// Gets or sets a list of supported characters or string that indicate a parameter.
    /// </summary>
    public string[] ParamIndicators
    {
        get { return m_sParamIndicators; }
        set { m_sParamIndicators = value; }
    }

    /// <summary>
    /// Gets or sets a list of supported characters or strings that indicate the
    /// value of a parameter.
    /// </summary>
    public char[] ValueIndicators
    {
        get { return m_sValueIndicators; }
        set { m_sValueIndicators = value; }
    }

    /// <summary>
    /// Returns the value that belong to the specified parameter. Use the method
    /// GetValues on the property Items for all values.
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public string this[string param]
    {
        get
        {
            string value = m_nvcParams.Get(param);
            return value;
        }
    }

    /// <summary>
    /// Returns whether the beginning of this string matches any of the
    /// specified strings.
    /// </summary>
    /// <param name="s">The string to check.</param>
    /// <param name="values">
    /// The string array that contains the values to compare.
    /// </param>
    /// <returns>
    /// True if any of the specifies strings matches the beginning of the
    /// string, false otherwise.
    /// </returns>
    private static bool StartsWithAny(string s, string[] values)
    {
        foreach (string value in values)
        {
            if (s.StartsWith(value)) return true;
        }

        return false;
    }

    private void Parse(string[] args)
    {
        if (args == null && args.Length == 0)
        {
            Trace.WriteLine("Warning: no arguments specified!");
        }

        foreach (string param in args)
        {
            try
            {
                if (StartsWithAny(param, ParamIndicators))
                {
                    string item = param.Substring(1);
                    int iValueIndicatorPos = item.IndexOfAny(m_sValueIndicators);
                    if (iValueIndicatorPos > -1)
                    {
                        //Item is of the format /name:value
                        string paramName = item.Substring(0, iValueIndicatorPos);
                        string paramValue = item.Substring(iValueIndicatorPos + 1);
                        m_nvcParams.Add(paramName, paramValue);
                    }
                    else
                    {
                        //Item is of the format /name
                        m_nvcParams.Add(item, string.Empty);
                    }
                }
                else
                {
                    Trace.WriteLine("Parameter \'" + param + "\' is not a supported parameter");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("An exception occurred while parsing \'" + param + "\': " + ex.Message + Environment.NewLine + "Stacktrace: " + ex.StackTrace);
            }
        }
    }
}
