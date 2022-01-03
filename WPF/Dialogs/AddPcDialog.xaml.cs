
using ModernWpf.Controls;
using RemoteExecuter.Entities;

namespace Gui.Dialogs
{
    /// <summary>
    /// Interaction logic for AddPcDialog.xaml
    /// </summary>
    public partial class AddPcDialog : ContentDialog
    {
        public RemotePcInfo PcInfo { get; set; }

        public AddPcDialog()
        {
            InitializeComponent();
        }

        private void AddClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (ipAddressTextBox.Text?.Length <= 0)
            {
                errorTextBlock.Text = "Ip Address is required.";

                args.Cancel = true;
            }
            else
            {
                PcInfo = new RemotePcInfo
                {
                    IpAddress = ipAddressTextBox.Text,
                    DisplayName = displayNameTextBox.Text
                };
            }
        }
    }
}
