using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using WinForms = System.Windows.Forms;

namespace WPFRibbon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
           
            InitializeComponent();
        }

        /// <summary>
        /// 添加界面
        /// </summary>
        /// <param name="control"></param>
        internal void AddControl(string title, FrameworkElement control)
        {
           
            if (control == null)
            {
                MessageBox.Show("没有配置该插件", "提示");
            }
            else
            {
                if (control is Window)
                {
                    Window cur = control as Window;
                    cur.Show();
                }
                else
                {

                    Frame frame = new Frame
                    {
                        Margin = new Thickness(5, 5, 5, 5)
                    };
                    frame.Navigate(control);
                  
                    var panel = new DocumentPanel() { Content = frame,Caption=title };
                    documentGroup.Items.Add(panel);
                    documentGroup.SelectedTabIndex = documentGroup.Items.Count;
                }
            }
        }

        /// <summary>
        /// 添加界面
        /// </summary>
        /// <param name="control"></param>
        internal void AddControl(string title, WinForms.Control control)
        {
            if (control == null)
            {
                MessageBox.Show("没有配置该插件", "提示");
            }
            else
            {
                if (control is WinForms.Form)
                {
                    WinForms.Form frm = control as WinForms.Form;
                    if (frm.TopLevel)
                    {
                        frm.Show();
                        return;
                    }
                }
                //
                WindowsFormsHost formsHost = new WindowsFormsHost
                {
                    Child = control
                };
               
                control.Show();
                var panel = new DocumentPanel() { Content = formsHost,Caption=title };
                documentGroup.Items.Add(panel);
                documentGroup.SelectedTabIndex = documentGroup.Items.Count;
            }
        }



    }
}
