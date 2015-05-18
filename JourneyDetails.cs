using System;
using System.Collections.Generic;
using System.Data;

namespace AutoFillerAdvance
{
    public class JourneyDetails
    {
        public DateTime TimeStamp;
        public int Firecounter = 0;
        public DateTime FireCounterTimeStamp=new DateTime(2099,12,30);
        public int InvokePaymentCounter = 0;
        public String LoginId;
        public String Pass;
        public String From;
        public String To;
        public String Date;
        public String TrainNumber;
        public String CoachClass;
        public String Quota;
        public String Mobile;
        public List<PassengerDetails> LsPassenger = new List<PassengerDetails>();
        public String DbCardNumber;
        public String DbCardExpiryMonth;
        public String DbCardExpiryYear;
        public String DbCardName;
        public String DbCardPin;

        public JourneyDetails()
        {

        }

        public JourneyDetails(DataTable dt)
        {
            LoginId = Convert.ToString(dt.Rows[0][1]);
            Pass = Convert.ToString(dt.Rows[1][1]);
            TrainNumber = Convert.ToString(dt.Rows[2][1]);
            From = Convert.ToString(dt.Rows[3][1]);
            To = Convert.ToString(dt.Rows[4][1]);
            Date = Convert.ToString(dt.Rows[5][1]);
            CoachClass = Convert.ToString(dt.Rows[6][1]);
            Quota = Convert.ToString(dt.Rows[7][1]);
            Mobile = Convert.ToString(dt.Rows[8][1]);
            DbCardNumber= Convert.ToString(dt.Rows[15][1]);
            DbCardExpiryMonth= Convert.ToString(dt.Rows[16][1]);
            DbCardExpiryYear= Convert.ToString(dt.Rows[16][2]);
            DbCardName = Convert.ToString(dt.Rows[17][1]);
            DbCardPin = Convert.ToString(dt.Rows[18][1]);

            int i = 1;
            do
            {
                PassengerDetails pdet = new PassengerDetails();
                pdet.Name = Convert.ToString(dt.Rows[9][i]);
                pdet.Age = Convert.ToString(dt.Rows[10][i]);
                pdet.Gender = Convert.ToString(dt.Rows[11][i]);
                pdet.BerthPref = Convert.ToString(dt.Rows[12][i]);
                pdet.IdCardType = Convert.ToString(dt.Rows[13][i]);
                pdet.IdCardNumber = Convert.ToString(dt.Rows[14][i]);
                LsPassenger.Add(pdet);
                i++;
            } while (Convert.ToString(dt.Rows[9][i]) != "");
        }
    }

    public class PassengerDetails
    {
        public String Name;
        public String Age;
        public String Gender;
        public String BerthPref;
        public String IdCardType;
        public String IdCardNumber;
        
    }
}
