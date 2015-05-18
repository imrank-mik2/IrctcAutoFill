using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoFillerAdvance
{
    /// <summary>
    /// Interaction logic for BrowserPage.xaml
    /// </summary>
    public partial class BrowserPage : Page
    {
        JourneyDetails _jD = new JourneyDetails();

        IList<String> lsURLs = new List<string>();

        public BrowserPage()
        {
            InitializeComponent();
            WebBrowser.Navigate("https://irctc.co.in");
        }

        public BrowserPage(JourneyDetails jDet)
        {
            _jD = jDet;
            InitializeComponent();
            WebBrowser.Navigate("https://irctc.co.in");
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            try
            {
                TxtTimer.Text = DateTime.Now.ToString("ddd dd MMM yyyy     hh:mm:ss");
                TxtUrl.Text = WebBrowser.Document.Url.ToString();
                
                if (WebBrowser.Document.Url.ToString().Contains("mainpage.jsf"))
                {
                    #region to be triggered at 10 am when tatkal autopilot on
                    DateTime tenTime = DateTime.ParseExact("10:00:05", "hh:mm:ss", CultureInfo.CurrentCulture);
                    if ((ChkGoOnTen.IsChecked==false) || (ChkGoOnTen.IsChecked==true && DateTime.Now>tenTime))
                    {
                        String fromStn = WebBrowser.Document.All.GetElementsByName("jpform:fromStation")[0].GetAttribute("value");
                        if (fromStn != "" && (ChkTatkalAuto.IsChecked == true))
                        {
                            HtmlElement eleForm = WebBrowser.Document.GetElementById("avlAndFareForm:trainbtwnstns:tb");
                            if (eleForm != null)
                            {
                                if (WebBrowser.Document.GetElementById("j_idt293_body") != null)
                                    //when Book Now available, Fire Book Now button
                                {
                                    HtmlElement ele = WebBrowser.Document.GetElementById("j_idt293_body");
                                    HtmlElement firstATag = ele.GetElementsByTagName("a")[0];
                                    string jsString = firstATag.GetAttribute("href");
                                    WebBrowser.Url =new Uri(jsString,UriKind.Absolute);

                                }
                                //else if (_jD.Firecounter == 0) //After Submit (when Book Now not available)
                                else if (_jD.FireCounterTimeStamp.AddSeconds(2) > DateTime.Now ) //After Submit (when Book Now not available)
                                { //Fire SL or 3AC or 2AC button
                                    if (_jD.TimeStamp.AddSeconds(3) < DateTime.Now)
                                    {
                                        DateTime date = DateTime.ParseExact(_jD.Date, "dd/M/yyyy",
                                            CultureInfo.CurrentCulture);
                                        String jsDate = date.ToString("ddd MMM dd hh:mm:ss IST yyyy")
                                            .Replace("12:00:00", "00:00:00");
                                        var arrFromStrings = _jD.From.Split(' ');
                                        var jsFrom = arrFromStrings.Reverse().First();
                                        var arrToStrings = _jD.To.Split(' ');
                                        var jsTo = arrToStrings.Reverse().First();
                                        String jsClass = _jD.CoachClass.ToUpper().Substring(0, 2);
                                        String jsBookNow = "javascript:availFareEnq(this,'" + _jD.TrainNumber + "','" +
                                                           jsDate + "','" + jsClass + "','" + jsFrom + "','" + jsTo +
                                                           "',true)";

                                        if (_jD.Quota=="Tatkal")
                                        {
                                            WebBrowser.Document.GetElementById("quota").SetAttribute("value", "CK");
                                        }
                                        WebBrowser.Url = new Uri(jsBookNow
                                                //"javascript:availFareEnq(this,'11077','Tue May 26 00:00:00 IST 2015','SL','PUNE','BPL',true)"
                                                , UriKind.Absolute);
                                        _jD.Firecounter++;
                                        _jD.FireCounterTimeStamp = DateTime.Now;
                                        TbFireCounter.Text = _jD.Firecounter.ToString();
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex){}
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            lsURLs.Add(e.Url.ToString());
            if (e.Url == WebBrowser.Document.Url)
            {
                WebBrowser.ScriptErrorsSuppressed = true;

                #region Login
                try
                {
                    if (WebBrowser.Document.GetElementById("usernameId") != null)
                    {
                        WebBrowser.Document.GetElementById("usernameId").SetAttribute("value", _jD.LoginId);
                        HtmlElementCollection col = WebBrowser.Document.All.GetElementsByName("j_password");
                        col[0].SetAttribute("value", _jD.Pass);
                        HtmlElementCollection col1 = WebBrowser.Document.All.GetElementsByName("j_captcha");
                        col1[0].Focus();
                    }
                    else if (WebBrowser.Document.GetElementById("j_idt109:j_idt111:j_idt114")!=null)
                    {
                        WebBrowser.Document.All.GetElementsByName("j_idt109:j_idt111:j_idt115")[0].InvokeMember("click");
                    }
                }
                catch (Exception){}
                
                #endregion
                

            #region JourneyDetails using Planner
            try
                {
                    HtmlElementCollection elemCol = WebBrowser.Document.All.GetElementsByName("jpform:fromStation");
                    if (elemCol.Count > 0)
                    {
                        String str = WebBrowser.Document.All.GetElementsByName("jpform:fromStation")[0].GetAttribute("value");
                        if (str == "")
                        {
                            HtmlElementCollection col1 = WebBrowser.Document.All.GetElementsByName("jpform:fromStation");
                            col1[0].SetAttribute("value", _jD.From);
                            HtmlElementCollection col2 = WebBrowser.Document.All.GetElementsByName("jpform:toStation");
                            col2[0].SetAttribute("value", _jD.To);
                            HtmlElementCollection col3 = WebBrowser.Document.All.GetElementsByName("jpform:journeyDateInputDate");
                            col3[0].SetAttribute("value", _jD.Date.Replace('/', '-'));
                            WebBrowser.Document.GetElementById("jpform:jpsubmit").InvokeMember("click");
                            _jD.TimeStamp = DateTime.Now;
                        }
                    }
                }
                catch (Exception){}
                
                #endregion
                
            }

            #region PassengerDetails

            if (WebBrowser.Document.Url.ToString().Contains("trainbetweenstns.jsf"))
            {

                try
                {
                    if (WebBrowser.Document.GetElementById("addPassengerForm:psdetail:0:psgnName") != null)
                    {
                        int i = 0;
                        foreach (var passenger in _jD.LsPassenger)
                        {
                            WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":psgnName")
                                .SetAttribute("value", passenger.Name);
                            WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":psgnAge")
                                .SetAttribute("value", passenger.Age);
                            WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":psgnGender")
                                .SetAttribute("value", passenger.Gender.Substring(0, 1));
                            WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":berthChoice")
                                .SetAttribute("value", passenger.BerthPref);
                            var elementCardType = WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":idCardType");
                            if (elementCardType !=null)
                                elementCardType.SetAttribute("value", passenger.IdCardType);
                            var elementCardNumber = WebBrowser.Document.GetElementById("addPassengerForm:psdetail:" + i + ":idCardNumber");
                            if (elementCardNumber !=null)
                                elementCardNumber.SetAttribute("value", passenger.IdCardNumber);
                            i++;
                        }
                        WebBrowser.Document.GetElementById("addPassengerForm:mobileNo")
                            .SetAttribute("value", "9011249878");
                        //webBrowser.Document.GetElementById("addPassengerForm:boardingStation").SetAttribute("value", "PUNE JN - PUNE");
                        //WebBrowser.Document.GetElementById("addPassengerForm:replan").RemoveFocus();
                        WebBrowser.Document.GetElementById("j_captcha").Focus();

                    }
                }
                catch
                {
                }


            }
            #endregion


            #region Select Transaction Type

            if (WebBrowser.Document.Url.ToString().Contains("jpInput.jsf"))
            {
                try
                {
                    {
                        WebBrowser.Document.GetElementById("jpBook:payOption:2")
                                            .SetAttribute("checked", "checked");
                        WebBrowser.Document.All.GetElementsByName("jpBook:payOption")[0]
                                .SetAttribute("value","DEBIT_CARD");
                        
                        WebBrowser.Document.GetElementById("jpBook:bankDCList")
                                .SetAttribute("value", "57");
                        if (e.Url.Equals(WebBrowser.Document.Url))
                        {
                            if (_jD.InvokePaymentCounter==0)
                            {
                                WebBrowser.Document.GetElementById("validate").InvokeMember("click");
                                _jD.InvokePaymentCounter++;
                            }
                        }
                    }

                }
                catch{}
            }
            #endregion

            #region Fill Payment details

            else if (WebBrowser.Document.Url.ToString().Contains("payment.jsp"))
            {
                try
                {
                    WebBrowser.Document.GetElementById("Ecom_Payment_Card_Number_Debit")
                            .SetAttribute("value", _jD.DbCardNumber);
                    WebBrowser.Document.GetElementById("Ecom_Payment_Card_ExpDate_Month_Debit")
                            .SetAttribute("value", _jD.DbCardExpiryMonth);
                    WebBrowser.Document.GetElementById("Ecom_Payment_Card_ExpDate_Year_Debit")
                            .SetAttribute("value", _jD.DbCardExpiryYear);
                    WebBrowser.Document.GetElementById("Ecom_Payment_Card_Name_Debit")
                            .SetAttribute("value", _jD.DbCardName);
                    WebBrowser.Document.GetElementById("Ecom_Payment_Pin")
                            .SetAttribute("value", _jD.DbCardPin);
                    WebBrowser.Document.GetElementById("Ecom_Payment_Pin").Focus();

                }
                catch { }
            }
            #endregion
        }

        
        


        private void BtnAutoPilot_OnClick(object sender, RoutedEventArgs e)
        {
            _jD.Firecounter = 0;
            TbFireCounter.Text = _jD.Firecounter.ToString();
        }

        private void BtnGoOnTen_OnClick(object sender, RoutedEventArgs e)
        {
            TbFireCounter.Text = _jD.Firecounter.ToString();
            if (ChkGoOnTen.IsChecked==true)
            {
                ChkTatkalAuto.IsChecked = true;
                BtnAutoPilot.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnAutoPilot.Visibility=Visibility.Visible;
            }
        }

        
    }
}
