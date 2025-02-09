using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Expression
    {
        int Id;
        public string ExpressionText;
        string LanguageCode;
        public Expression(string expression_text, string language_code)
        {
            ExpressionText = expression_text;
            LanguageCode = language_code;
        }
    }
}
