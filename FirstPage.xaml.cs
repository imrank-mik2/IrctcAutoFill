using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoFillerAdvance
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        JourneyDetails _jDet;
        BrowserPage _browserPage;
        public FirstPage()
        {
            InitializeComponent();

            //ExcelImport.dtTbl = ExcelImport.exceldata(@"C:\Users\Imran\Desktop\AutoFillInput.xlsx");
            ExcelImport.DsExcel = ExcelImport.ExcelData(@"C:\Users\Imran\Desktop\AutoFillInput.xlsx");
            _jDet = new JourneyDetails(ExcelImport.DsExcel.Tables[0]);
            AcbFrom.ItemsSource = ValidateStations.ReturnAllStations();
            AcbTo.ItemsSource = ValidateStations.ReturnAllStations();
            Populate(_jDet);
            CreatePassengerList(_jDet);
            TxtPass.Focus();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            TxtTimer.Text = DateTime.Now.ToString("ddd dd MMM yyyy     hh:mm:ss");
            CommandManager.InvalidateRequerySuggested();
        }

        public void Populate(JourneyDetails jD)
        {
            TxtId.Text = jD.LoginId;
            TxtPass.Password = jD.Pass;
            AcbFrom.Text = jD.From;
            AcbTo.Text = jD.To;
            TxtTrainNumber.Text = jD.TrainNumber;
            DpDate.Text = jD.Date;
            CbClass.Text = jD.CoachClass;
            CbQuota.Text = jD.Quota;
            TxtMobile.Text = jD.Mobile;
            TxtCardNumber.Text = jD.DbCardNumber;
            TxtCardExpiryMonth.Text = jD.DbCardExpiryMonth;
            TxtCardExpiryYear.Text = jD.DbCardExpiryYear;
            TxtCardName.Text = jD.DbCardName;
            //TxtCardPin.Password = jD.DbCardPin;

        }

        public void CreatePassengerList(JourneyDetails jD)
        {
            var i = 0;
            foreach (var passenger in jD.LsPassenger)
            {
                TextBox txtName = new TextBox();
                txtName.Name =  "txtPsName" + (i+1);
                RegisterName("txtPsName" + (i + 1), txtName);
                txtName.Margin = TbName.Margin;
                txtName.MinWidth = 140;
                txtName.Height = TbName.Height;
                txtName.HorizontalAlignment = HorizontalAlignment.Left;
                txtName.VerticalAlignment = VerticalAlignment.Center;
                txtName.Padding = TbName.Padding;
                txtName.Text = passenger.Name;
                Grid.SetRow(txtName, 0);
                Grid.SetColumn(txtName, i);
                GrdPassengerDetails.Children.Add(txtName);

                TextBox txtAge = new TextBox();
                txtAge.Name = "txtPsAge" + (i + 1);
                RegisterName("txtPsAge" + (i + 1), txtAge);
                txtAge.Margin = TbAge.Margin;
                txtAge.MinWidth = 20;
                txtAge.Height = TbAge.Height;
                txtAge.HorizontalAlignment = HorizontalAlignment.Left;
                txtAge.VerticalAlignment = VerticalAlignment.Center;
                txtAge.Padding = TbAge.Padding;
                txtAge.Text = passenger.Age;
                Grid.SetRow(txtAge, 1);
                Grid.SetColumn(txtAge, i);
                GrdPassengerDetails.Children.Add(txtAge);

                ComboBox cbGender = new ComboBox();
                cbGender.Name = "cbPsGender" + (i + 1);
                RegisterName("cbPsGender" + (i + 1),cbGender);
                List<string> lsGender = new List<string>();
                lsGender.Add("Male");
                lsGender.Add("Female");
                cbGender.ItemsSource = lsGender;
                cbGender.Margin = TbGender.Margin;
                cbGender.Height = TbBerthPref.Height;
                cbGender.Width = 70;
                cbGender.HorizontalAlignment = HorizontalAlignment.Left;
                cbGender.VerticalAlignment = VerticalAlignment.Center;
                cbGender.Padding = TbGender.Padding;
                cbGender.Text = passenger.Gender;
                Grid.SetRow(cbGender, 2);
                Grid.SetColumn(cbGender, i);
                GrdPassengerDetails.Children.Add(cbGender);

                ComboBox cbBerthPreference = new ComboBox();
                cbBerthPreference.Name = "cbPsBerthPreference" + (i + 1);
                RegisterName("cbPsBerthPreference" + (i + 1), cbBerthPreference);
                List<string> lsBerthPref = new List<string>();
                lsBerthPref.Add("UB");
                lsBerthPref.Add("LB");
                lsBerthPref.Add("MB");
                cbBerthPreference.ItemsSource = lsBerthPref;
                cbBerthPreference.Margin = TbBerthPref.Margin;
                cbBerthPreference.Height = TbBerthPref.Height;
                cbBerthPreference.Width = 70;
                cbBerthPreference.HorizontalAlignment = HorizontalAlignment.Left;
                cbBerthPreference.VerticalAlignment = VerticalAlignment.Center;
                cbBerthPreference.Padding = TbBerthPref.Padding;
                cbBerthPreference.Text = passenger.BerthPref;
                Grid.SetRow(cbBerthPreference, 3);
                Grid.SetColumn(cbBerthPreference, i);
                GrdPassengerDetails.Children.Add(cbBerthPreference);

                ComboBox cbIdCardType = new ComboBox();
                cbIdCardType.Name = "cbPsIdCardType" + (i + 1);
                RegisterName("cbPsIdCardType" + (i + 1), cbIdCardType);
                List<string> lsIdCardType = new List<string>();
                lsIdCardType.Add("NULL_IDCARD");
                lsIdCardType.Add("UNIQUE_ICARD");            
                lsIdCardType.Add("DRIVING_LICENSE");
                lsIdCardType.Add("PASSPORT");
                lsIdCardType.Add("PANCARD");
                lsIdCardType.Add("VOTER_ICARD");
                lsIdCardType.Add("GOVT_ICARD");
                lsIdCardType.Add("STUDENT_ICARD");
                lsIdCardType.Add("BANK_PASSBOOK");
                lsIdCardType.Add("CREDIT_CARD");
                cbIdCardType.ItemsSource = lsIdCardType;
                cbIdCardType.Margin = TbIdCardType.Margin;
                cbIdCardType.Height = TbIdCardType.Height;
                cbIdCardType.Width = 70;
                cbIdCardType.HorizontalAlignment = HorizontalAlignment.Left;
                cbIdCardType.VerticalAlignment = VerticalAlignment.Center;
                cbIdCardType.Padding = TbIdCardType.Padding;
                cbIdCardType.Text = passenger.IdCardType.Trim() == "" ? "NULL_IDCARD" : passenger.IdCardType;
                Grid.SetRow(cbIdCardType, 4);
                Grid.SetColumn(cbIdCardType, i);
                GrdPassengerDetails.Children.Add(cbIdCardType);

                TextBox txtIdCardNumber = new TextBox();
                txtIdCardNumber.Name = "txtPsIdCardNumber" + (i + 1);
                RegisterName("txtPsIdCardNumber" + (i + 1), txtIdCardNumber);
                txtIdCardNumber.Margin = TbIdCardNumber.Margin;
                txtIdCardNumber.MinWidth = 20;
                txtIdCardNumber.Height = TbIdCardNumber.Height;
                txtIdCardNumber.HorizontalAlignment = HorizontalAlignment.Left;
                txtIdCardNumber.VerticalAlignment = VerticalAlignment.Center;
                txtIdCardNumber.Padding = TbIdCardNumber.Padding;
                txtIdCardNumber.Text = passenger.IdCardNumber;
                Grid.SetRow(txtIdCardNumber, 5);
                Grid.SetColumn(txtIdCardNumber, i);
                GrdPassengerDetails.Children.Add(txtIdCardNumber);

                i++;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        void Login()
        {
            bool validated = GetJourneyDetailsFromGui();
            _browserPage = new BrowserPage(_jDet);
            NavigationService.Navigate(_browserPage);
        }

        private bool GetJourneyDetailsFromGui()
        {
            bool validation = false;
            int i = 0;
            _jDet.LoginId = TxtId.Text;
            _jDet.Pass = TxtPass.Password;
            _jDet.From = AcbFrom.Text;
            _jDet.To = AcbTo.Text;
            _jDet.TrainNumber = TxtTrainNumber.Text;
            _jDet.Date = DpDate.Text;
            _jDet.CoachClass = CbClass.Text;
            _jDet.Quota = CbQuota.Text;
            _jDet.Mobile = TxtMobile.Text;
            _jDet.DbCardNumber = TxtCardNumber.Text;
            _jDet.DbCardExpiryMonth = TxtCardExpiryMonth.Text;
            _jDet.DbCardExpiryYear = TxtCardExpiryYear.Text;
            _jDet.DbCardName = TxtCardName.Text;
            _jDet.DbCardPin = TxtCardPin.Password;

            foreach (var passenger in _jDet.LsPassenger)
            {

                _jDet.LsPassenger[i].Name = ((TextBox)GrdPassengerDetails.FindName("txtPsName"+(i+1))).Text;
                _jDet.LsPassenger[i].Age = ((TextBox)GrdPassengerDetails.FindName("txtPsAge" + (i + 1))).Text;
                _jDet.LsPassenger[i].Gender = ((ComboBox)GrdPassengerDetails.FindName("cbPsGender" + (i + 1))).Text;
                _jDet.LsPassenger[i].BerthPref = ((ComboBox)GrdPassengerDetails.FindName("cbPsBerthPreference" + (i + 1))).Text;
                _jDet.LsPassenger[i].IdCardType = ((ComboBox)GrdPassengerDetails.FindName("cbPsIdCardType" + (i + 1))).Text;
                _jDet.LsPassenger[i].IdCardNumber = ((TextBox)GrdPassengerDetails.FindName("txtPsIdCardNumber" + (i + 1))).Text;
                
                i++;
            }
            return validation;
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }


    }

}
