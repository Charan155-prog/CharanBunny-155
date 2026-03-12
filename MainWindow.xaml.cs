using System.Windows;
using System.Windows.Input;
using Sterilization.Views;

namespace Sterilization
{
    
    public partial class MainWindow : Window
    {
        public readonly DashboardPage    _pageDashboard   = new DashboardPage();
        public readonly DataViewPage     _pageTagDesc     = new DataViewPage("TagDescriptions",     "Tag Descriptions");
        public readonly DataViewPage     _pageGoodCycles  = new DataViewPage("GoodCycles",          "2 Days of Good Cycles");
        public readonly DataViewPage     _pageFailedCycle1= new DataViewPage("FailedCycle1",        "Failed Cycle 1");
        public readonly DataViewPage     _pageFailedCycle2= new DataViewPage("FailedCycle2",        "Failed Cycle 2");

        public MainWindow()
        {
            InitializeComponent();
            NavigateTo(_pageDashboard, BtnNavDashboard);
        }

        // ── Navigation ──────────────────────────────────────────────
        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = (System.Windows.Controls.Button)sender;

            if      (btn == BtnNavDashboard)   NavigateTo(_pageDashboard,    btn);
            else if (btn == BtnNavTagDesc)      NavigateTo(_pageTagDesc,      btn);
            else if (btn == BtnNavGoodCycles)   NavigateTo(_pageGoodCycles,   btn);
            else if (btn == BtnNavFailed1)      NavigateTo(_pageFailedCycle1, btn);
            else if (btn == BtnNavFailed2)      NavigateTo(_pageFailedCycle2, btn);
        }

        private bool _menuExpanded = true;
        private void BtnToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            _menuExpanded = !_menuExpanded;

            SidebarColumn.Width = new GridLength(_menuExpanded ? 220 : 52);

            var visibility = _menuExpanded ? Visibility.Visible : Visibility.Collapsed;

            // Hide/show text labels
            TxtNavLabel.Visibility = visibility;
            TxtMenuLabel.Visibility = visibility;
            LblDashboard.Visibility = visibility;
            LblTagDesc.Visibility = visibility;
            LblGoodCycles.Visibility = visibility;
            LblFailed1.Visibility = visibility;
            LblFailed2.Visibility = visibility;
            LblSettings.Visibility = visibility;
        }

        public System.Windows.Controls.Button _activeBtn;
        public void NavigateTo(System.Windows.Controls.Page page,
                                 System.Windows.Controls.Button btn)
        {
            if (_activeBtn != null) _activeBtn.Tag = null;
            btn.Tag = "Active";
            _activeBtn = btn;
            MainFrame.Navigate(page);
        }
        //private void NavigateTo(System.Windows.Controls.Page page,
        //                 System.Windows.Controls.Button btn)
        //{
        //    if (_activeBtn != null) _activeBtn.Tag = null;
        //    btn.Tag = "Active";
        //    _activeBtn = btn;
        //    MainFrame.Navigate(page);

        //    // Update hamburger label to show current page name
        //    if (btn == BtnNavDashboard) TxtMenuLabel.Text = "Dashboard";
        //    else if (btn == BtnNavTagDesc) TxtMenuLabel.Text = "Tag Descriptions";
        //    else if (btn == BtnNavGoodCycles) TxtMenuLabel.Text = "Good Cycles";
        //    else if (btn == BtnNavFailed1) TxtMenuLabel.Text = "Failed Cycle 1";
        //    else if (btn == BtnNavFailed2) TxtMenuLabel.Text = "Failed Cycle 2";
        //}

        // ── Settings ─────────────────────────────────────────────────
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.SettingsWindow { Owner = this };
            if (win.ShowDialog() == true)
            {
                // Refresh all data pages after successful import
                _pageTagDesc.Refresh();
                _pageGoodCycles.Refresh();
                _pageFailedCycle1.Refresh();
                _pageFailedCycle2.Refresh();
                _pageDashboard.Refresh();
            }
        }

        // ── Window chrome ─────────────────────────────────────────────
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) ToggleMaximize();
            else DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState.Minimized;

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
            => ToggleMaximize();

        private void BtnClose_Click(object sender, RoutedEventArgs e)
            => Close();

        private void ToggleMaximize()
            => WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
    }
}
