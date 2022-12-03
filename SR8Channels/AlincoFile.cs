using System;
using System.Collections;
using System.IO;
using System.Text;
using static Alinco.AlincoEnums;

namespace Alinco
{
    public class AlincoFile
    {

        public ArrayList channelList { get; } = new ArrayList();
        public string header { get; } = "";
        public string footer { get; } = "";
        private string Filename;

        public AlincoFile(string fileName)
        {
            Filename = fileName;

            int i = 0;
            foreach (string line in System.IO.File.ReadLines(fileName))
            {
                if (i <= 2)
                {
                    header = header + line + Environment.NewLine;
                }

                if (i > 2 && i < 609 && line.Substring(0, 1) == "8")
                {
                    channelList.Add(new Channel(line, i - 3));
                }

                if (i == 609)
                {
                    footer = line;
                }

                i++;
            }
        }

        public void WriteSR8(string filename)
        {
            string[] channels = new string[606];
            StringBuilder stringBuilder = new StringBuilder();

            for (int i=0; i< channels.Length; i++)
            {
                channels[i] = new String('0', 64);
            }

            foreach(Channel c in channelList)
            {
                int n = c.ChannelToNumeric();
                channels[n] = c.Encode();
            }

            stringBuilder.Append(header);
           // stringBuilder.Append(Environment.NewLine);
            for (int i = 0; i < channels.Length; i++)
            {
                stringBuilder.Append(channels[i]);
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append(footer);

            File.WriteAllText(filename, stringBuilder.ToString());
        }

        public void ExportCSV(string filename, char delimiter = ',', bool header = true)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (header)
            {
                stringBuilder.Append("Bank" + delimiter);
                stringBuilder.Append("ChannelNo" + delimiter);
                stringBuilder.Append("Skip" + delimiter);
                stringBuilder.Append("Mode" + delimiter);
                stringBuilder.Append("Gain" + delimiter);
                stringBuilder.Append("AGC" + delimiter);
                stringBuilder.Append("Power" + delimiter);
                stringBuilder.Append("Filter" + delimiter);
                stringBuilder.Append("Tone" + delimiter);
                stringBuilder.Append("Noise Blanker" + delimiter);
                stringBuilder.Append("RX Frequency" + delimiter);
                stringBuilder.Append("Channel Name" + delimiter);
                stringBuilder.Append("TX Frequency" + Environment.NewLine);
            }


            foreach (Channel c in channelList)
            {
                stringBuilder.Append(c.bank.ToString() + delimiter);
                stringBuilder.Append(c.channelNo.ToString() + delimiter);
                stringBuilder.Append(c.skip.ToString() + delimiter);
                stringBuilder.Append(c.mode.ToString() + delimiter);
                stringBuilder.Append(c.gain.ToString() + delimiter);
                stringBuilder.Append(c.agc.ToString() + delimiter);
                stringBuilder.Append(c.power.ToString() + delimiter);
                stringBuilder.Append(c.filter.ToString() + delimiter);
                stringBuilder.Append(c.tone.ToString() + delimiter);
                stringBuilder.Append(c.noiseBlanker.ToString() + delimiter);
                stringBuilder.Append(c.rxFrequency.ToString() + delimiter);
                stringBuilder.Append(c.channelName.ToString() + delimiter);
                stringBuilder.Append(c.txFrequency.ToString() + Environment.NewLine);
            }
            Console.Write(stringBuilder.ToString());

            File.WriteAllText(filename, stringBuilder.ToString());
        }

        public void ReadCSV(string fileName, char delimiter = ',')
        {
            this.Filename = fileName;

            foreach (string line in System.IO.File.ReadLines(fileName))
            {
                if (!line.StartsWith("Bank"))
                {
                    try
                    {
                        Channel channel = new Channel();
                        string[] columns = line.Split(delimiter);
                        channel.bank = (Bank)Enum.Parse(typeof(Bank), columns[0], true);
                        channel.channelNo = Int32.Parse(columns[1]);
                        channel.skip = (Skip)Enum.Parse(typeof(Skip), columns[2], true);
                        channel.mode = (Mode)Enum.Parse(typeof(Mode), columns[3], true);
                        channel.gain = (Gain)Enum.Parse(typeof(Gain), columns[4], true);
                        channel.agc = (AGC)Enum.Parse(typeof(AGC), columns[5], true);
                        channel.power = (Power)Enum.Parse(typeof(Power), columns[6], true);
                        channel.filter = (Filter)Enum.Parse(typeof(Filter), columns[7], true);
                        channel.tone = (Tone)Enum.Parse(typeof(Tone), columns[8], true); //TODO: 
                        channel.noiseBlanker = (NoiseBlanker)Enum.Parse(typeof(NoiseBlanker), columns[9], true);
                        channel.rxFrequency = Int32.Parse(columns[10]);
                        channel.channelName = columns[11];
                        channel.txFrequency = Int32.Parse(columns[12]);

                        this.channelList.Add(channel);
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("CSV Read error", "Error in line " + line.ToString());
                        throw;
                    }
                }


            }


        }

    }
}