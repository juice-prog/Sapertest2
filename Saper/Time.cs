using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfSaper6

{
    public class Time
    {

        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Sec { get; private set; }

        public Time(uint h, uint m, uint s)
        {
            if (h > 23 || m > 59 || s > 59)
            {
                throw new ArgumentException("указано не действ время");
            }
            Hours = (int)h; Minutes = (int)m; Sec = (int)s;
        }

        public Time(DateTime dt)
        {
            Hours = dt.Hour;
            Minutes = dt.Minute;
            Sec = dt.Second;
        }

        public override string ToString()
        {
            string time = "";


                    if (this.Minutes < 10)
            {
                time += "0" + Convert.ToString(this.Minutes);


                    }
                     else
            {
                time += Convert.ToString(this.Minutes);
            }



              time += ":";

             if (this.Sec < 10)
            {
                time += "0" + Convert.ToString(this.Sec);
            }
            else
            {
                time += Convert.ToString(this.Sec);
            }


            return time;
        }

        public void AddHours(uint h)
        {
            this.Hours += (int)h;
        }

        public void AddMinutes(uint m)
        {
            this.Minutes += (int)m;

              if (this.Minutes == 60)
            {
                this.Minutes = 0;
                this.AddHours(1);
            }
        }

        public void AddSeconds(uint s)
        {
            this.Sec += (int)s;
            


                       if(this.Sec==60)
            {
                this.Sec = 0;
                this.AddMinutes(1);
            }
        }
    }
}
