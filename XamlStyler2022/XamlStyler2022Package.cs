using EnvDTE;
using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using XamlStyler2022.Helpers;
using Xavalon.XamlStyler.Options;
using Task = System.Threading.Tasks.Task;

namespace XamlStyler2022
{
    [ProvideAutoLoad(XamlStyler2022Package.UIContextGuidString, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideUIContextRule(XamlStyler2022Package.UIContextGuidString, name: "XAML load", expression: "Dotxaml", termNames: new[] { "Dotxaml" }, termValues: new[] { "HierSingleSelectionName:.xaml$" })]

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(XamlStyler2022Package.PackageGuidString)]
    public sealed partial class XamlStyler2022Package : AsyncPackage
    {
        public const string PackageGuidString = "72a69844-4a21-4940-a3c1-6a9018f64c91";
        public const string UIContextGuidString = VSConstants.UICONTEXT.CodeWindow_string;
        public const string StandardCommandSet97String = VSConstants.CMDSETID.StandardCommandSet97_string;

        private CommandEvents saveCommandEvent;
        public DTE IDE { get; private set; }
        private OptionsHelper optionsHelper;

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            IDE = await this.GetServiceAsync(typeof(DTE)) as DTE;
            Assumes.Present(IDE);

            this.saveCommandEvent = IDE.Events.CommandEvents[StandardCommandSet97String, 331];
            saveCommandEvent.BeforeExecute += BeforeSaveCommand;

            this.optionsHelper = new OptionsHelper(this);

            DebugHelper.WriteDebug();
            //MessageBox.Show("InitializeAsync");
        }

        private void BeforeSaveCommand(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IStylerOptions globalOptions = this.optionsHelper.GetGlobalStylerOptions();
            if (!globalOptions.FormatOnSave)
            {
                return;
            }

            Document document = this.IDE.ActiveDocument;
            IStylerOptions options = this.optionsHelper.GetDocumentStylerOptions(document);
            if (options.FormatOnSave)
            {
                this.FormatDocument(document, options);
            }

            DebugHelper.WriteDebug();
        }

        private void ShowMessageBox(Exception ex)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string message = string.Format(
                CultureInfo.CurrentCulture,
                "{0}\r\n\r\nIf this deems a malfunctioning of styler, please kindly submit an issue at https://github.com/Zmrbak/XamlStyler.",
                ex.Message);
            string title = $"Error in {this.GetType().Name}:";

            VsShellUtilities.ShowMessageBox(
                this,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        #endregion
    }
}
