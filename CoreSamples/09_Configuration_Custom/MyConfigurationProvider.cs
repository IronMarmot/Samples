using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace _09_Configuration_Custom
{
    class MyConfigurationProvider : ConfigurationProvider
    {
        Timer timer;
        public MyConfigurationProvider():base()
        {
            timer = new Timer() ;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 3000;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Load(true);
        }

        public override void Load()
        {
            Load(false);
        }

        private void Load(bool reload)
        {
            this.Data["time"] = System.DateTime.Now.ToString();
            if (reload)
            {
                base.OnReload();
            }
        }
    }
}
