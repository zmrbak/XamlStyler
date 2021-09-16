using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xavalon.XamlStyler.Options;

namespace XamlStyler2022
{
    public class PackageOptions : DialogPage
    {
        private readonly IStylerOptions options;

        public PackageOptions()
        {
            this.options = new StylerOptions();
        }

        public override object AutomationObject => this.options;
    }
}