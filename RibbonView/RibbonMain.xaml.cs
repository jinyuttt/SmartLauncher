using DevExpress.Xpf.Core;

namespace RibbonView
{
    /// <summary>
    /// RibbonMain.xaml 的交互逻辑
    /// </summary>
    public partial class RibbonMain : ThemedWindow
    {
        public RibbonMain()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
            InitializeComponent();
        }
    }
}
