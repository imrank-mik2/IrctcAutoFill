using System.Windows.Navigation;

//using Microsoft.Windows.Controls;

namespace AutoFillerAdvance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Content = new FirstPage();
            #region Old Code
            //    //ExcelImport.dtTbl = ExcelImport.exceldata(@"C:\Users\Imran\Desktop\AutoFillInput.xlsx");
            //    ExcelImport.dsExcel = ExcelImport.ExcelData(@"C:\Users\Imran\Desktop\AutoFillInput.xlsx");
            //    JourneyDetails jDet = new JourneyDetails(ExcelImport.dsExcel.Tables[0]);
            //    acbFrom.ItemsSource = ValidateStations.ReturnAllStations();
            //    acbTo.ItemsSource = ValidateStations.ReturnAllStations();
            //    //List<string> matchingStations = new List<string>();
            //    //matchingStations = ValidateStations.ReturnMatchingStations(acbFrom.Text);
            //    Populate(jDet);
            //    CreatePassengerList(jDet);
            //    txtPass.Focus();
            //}

            //public void Populate(JourneyDetails jD)
            //{
            //    txtID.Text = jD.loginID;
            //    txtPass.Password = jD.pass;
            //    //txtFrom.Text = jD.from;
            //    //txtTo.Text = jD.to;
            //    acbFrom.Text = jD.from;
            //    acbTo.Text = jD.to;
            //    dpDate.Text = jD.date;
            //    cbClass.Text = jD.coachClass;
            //    cbQuota.Text = jD.quota;
            //    txtMobile.Text = jD.mobile;
            //}

            //public void CreatePassengerList(JourneyDetails jD)
            //{
            //    var i = 0;
            //    foreach (var passenger in jD.lsPassenger)
            //    {
            //        TextBox txtName = new TextBox();
            //        txtName.Margin = tbName.Margin;
            //        txtName.MinWidth = 140;
            //        txtName.Height = tbName.Height;
            //        txtName.HorizontalAlignment = HorizontalAlignment.Left;
            //        txtName.VerticalAlignment = VerticalAlignment.Center;
            //        txtName.Padding = tbName.Padding;
            //        txtName.Text = passenger.name;
            //        Grid.SetRow(txtName, 0);
            //        Grid.SetColumn(txtName, i);
            //        this.grdPassengerDetails.Children.Add(txtName);

            //        TextBox txtAge = new TextBox();
            //        txtAge.Margin = tbAge.Margin;
            //        txtAge.MinWidth = 20;
            //        txtAge.Height = tbAge.Height;
            //        txtAge.HorizontalAlignment = HorizontalAlignment.Left;
            //        txtAge.VerticalAlignment = VerticalAlignment.Center;
            //        txtAge.Padding = tbAge.Padding;
            //        txtAge.Text = passenger.age;
            //        Grid.SetRow(txtAge, 1);
            //        Grid.SetColumn(txtAge, i);

            //        this.grdPassengerDetails.Children.Add(txtAge);

            //        ComboBox cbGender = new ComboBox();
            //        List<string> lsGender = new List<string>();
            //        lsGender.Add("Male");
            //        lsGender.Add("Female");
            //        cbGender.ItemsSource = lsGender;
            //        cbGender.Margin = tbGender.Margin;
            //        cbGender.Height = tbBerthPref.Height;
            //        cbGender.Width = 70;
            //        cbGender.HorizontalAlignment = HorizontalAlignment.Left;
            //        cbGender.VerticalAlignment = VerticalAlignment.Center;
            //        cbGender.Padding = tbGender.Padding;
            //        cbGender.Text = passenger.gender;
            //        Grid.SetRow(cbGender, 2);
            //        Grid.SetColumn(cbGender, i);
            //        this.grdPassengerDetails.Children.Add(cbGender);

            //        ComboBox cbBerthPreference = new ComboBox();
            //        List<string> lsBerthPref = new List<string>();
            //        lsBerthPref.Add("UB");
            //        lsBerthPref.Add("LB");
            //        lsBerthPref.Add("MB");
            //        cbBerthPreference.ItemsSource = lsBerthPref;
            //        cbBerthPreference.Margin = tbBerthPref.Margin;
            //        cbBerthPreference.Height = tbBerthPref.Height;
            //        cbBerthPreference.Width = 70;
            //        cbBerthPreference.HorizontalAlignment = HorizontalAlignment.Left;
            //        cbBerthPreference.VerticalAlignment = VerticalAlignment.Center;
            //        cbBerthPreference.Padding = tbBerthPref.Padding;
            //        cbBerthPreference.Text = passenger.berthPref;
            //        Grid.SetRow(cbBerthPreference, 3);
            //        Grid.SetColumn(cbBerthPreference, i);
            //        this.grdPassengerDetails.Children.Add(cbBerthPreference);

            //        i++;
            //    }
            //}

            //private void btnLogin_Click(object sender, RoutedEventArgs e)
            //{

            //}

            #endregion

        }

        

    }
}
