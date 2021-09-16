using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlStyler2022.Extensions
{
    public sealed class Constants
    {
        public const string XamlFileExtension = ".xaml";
        public const string XamlLanguageType = "XAML";
        public const string XmlLanguageType = "XML";

        public const uint CommandIDFormatXamlFile = 0x100;
        public const uint CommandIDFormatAllXaml = 0x200;
        public const uint CommandIDFormatSelectedXaml = 0x300;
    }
}

