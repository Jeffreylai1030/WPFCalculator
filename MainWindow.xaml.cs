using System.Collections.Generic;
using System.Windows;
using WPFCalculator.models;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<MathQueryModelView> mathQueryList = new List<MathQueryModelView>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var mathQuery = new MathQueryModelView() { MathQueryString = TxtInput.Text };

            mathQueryList.Add(mathQuery);

            lvMathQuery.ItemsSource = mathQueryList;
            lvMathQuery.Items.Refresh();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "1 + 1" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "2 * 2" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "1 + 2 + 3" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "6 / 2" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "11 + 23" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "11.1 + 23" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "1 + 1 * 3" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "( 11.5 + 15.4 ) + 10.1" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "23 - ( 29.3 - 12.5 )" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "( 1 / 2 ) - 1 + 1" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "10 - ( 2 + 3 * ( 7 - 5 ) )" });
            mathQueryList.Add(new MathQueryModelView() { MathQueryString = "2 ( 6 + 7 ) - 8 ** 2" });

            lvMathQuery.ItemsSource = mathQueryList;
            lvMathQuery.Items.Refresh();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            mathQueryList = new List<MathQueryModelView>();
            lvMathQuery.ItemsSource = mathQueryList;
            lvMathQuery.Items.Refresh();
        }
    }
}
