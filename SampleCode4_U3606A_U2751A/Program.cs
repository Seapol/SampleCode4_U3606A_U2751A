using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypressSemiconductor.ChinaManufacturingTest;

namespace SampleCode4_U3606A_U2751A
{
    class Program
    {
        

        static void Main(string[] args)
        {
            MultiMeter dmm = new MultiMeter();
            Agilent sw = new Agilent();


            bool dmmConnected = false;
            bool swConnected = false;
            try
            {
                dmmConnected = dmm.InitializeU3606A("dmm");
                swConnected = sw.InitializeU2751A_WELLA("swA");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            

            if (!dmmConnected&&swConnected)
            {
                if (!dmmConnected)
                {
                    Console.WriteLine("U3606A is not connected. Please check the connection...");
                }
                if (!swConnected)
                {
                    Console.WriteLine("U2751A is not connected. Please check the connection...");
                    
                }
            }
            else
            {
                
                Console.WriteLine("U3606A/U2751A are connected successfully!");
                Console.WriteLine("");
                try
                {
                    Console.WriteLine("Perform Current Test on CH1 to CH8");
                    //Perform Current Test on CH1 to CH8
                    for (int i = 0; i < 8; i++)
                    {
                        PerformCurrentTest(dmm, sw, i+1);
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Perform Voltage Test on CH1 to CH8");

                    //Perform Voltage Test on CH1 to CH8
                    for (int i = 0; i < 8; i++)
                    {
                        PerformVoltageTest(dmm, sw, i + 1);
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                
            }

            Console.ReadKey();
            
        }

        private static double PerformCurrentTest(MultiMeter dmm, Agilent sw, int channel)
        {
            double value = -1.0;

            MultiMeter.current current_meas;

            sw.SetRelayWellA_byCH(channel, true);


            current_meas = dmm.MeasureChannelCurrent(500,500);

            value = current_meas.average;

            Console.WriteLine(string.Format("Obtain the average result of PerformCurrentTest: {0} mA on CH{1}", Math.Round(value*1000,4), channel));

            return value;
        }

        private static double PerformVoltageTest(MultiMeter dmm, Agilent sw, int channel)
        {
            double value = -1.0;

            MultiMeter.volt volt_meas;

            sw.SetRelayWellA_byCH(channel, true);


            volt_meas = dmm.MeasureChannelVoltage(500, 500);

            value = volt_meas.average;

            Console.WriteLine(string.Format("Obtain the average result of PerformVoltageTest: {0} V on CH{1}", Math.Round(value, 4), channel));

            return value;
        }

    }
}
