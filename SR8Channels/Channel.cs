using System;
using System.Collections.Generic;
using System.Text;
using static Alinco.AlincoEnums;

namespace Alinco
{
    class Channel
    {
        const int CHANNEL_ENABLED = 8;

        public int channelNo = 0;
        public Bank bank = Bank.None;
        public Skip skip = Skip.noSkip;
        public Mode mode = Mode.AM;
        public Gain gain = Gain.db0;
        public AGC agc = AGC.Slow;
        public Power power = Power.High;
        public Filter filter = Filter.Wide;
        public Tone tone = Tone.None;
        public NoiseBlanker noiseBlanker = NoiseBlanker.Off;
        public int rxFrequency = 7040000;
        public int txFrequency = 7040000;
        public string channelName = "";

        public Channel()
        {
        }

        class Memory
        {
            public int channelNo = 0;
            public Bank bank = Bank.None;
        }

        public int ChannelToNumeric()
        {
            switch (bank)
            {
                case Bank.VFO_A: return 0;
                case Bank.VFO_B: return 1;

                case Bank.None: return channelNo + 2;
                case Bank.A: return channelNo + 202;
                case Bank.B: return channelNo + 404;
            }
            return -1;
        }

        static Memory ChannelFromNumeric(int ChannelNo)
        {
            Bank bank;
            int channelNo;

            if (ChannelNo == 0)
            {
                bank = Bank.VFO_A;
                channelNo = 0;
                return new Memory { bank = bank, channelNo = channelNo };
            }
            if (ChannelNo == 1)
            {
                bank = Bank.VFO_B;
                channelNo = 1;
                return new Memory { bank = bank, channelNo = channelNo };
            }

            if (ChannelNo < 200)
            {
                bank = Bank.None;
                channelNo = ChannelNo - 2;
                return new Memory { bank = bank, channelNo = channelNo };
            }

            if (ChannelNo < 402)
            {
                bank = Bank.A;
                channelNo = ChannelNo - 202;
                return new Memory { bank = bank, channelNo = channelNo };
            }

            bank = Bank.B;
            channelNo = ChannelNo - 404;
            return new Memory { bank = bank, channelNo = channelNo };
        }


        public string Encode()
        {
            char[] c = new char[64];
            for (int i = 0; i < 64; i++) c[i] = '0';

            string toneHex = AlincoConverter.ByteToHex((byte)tone);
            char[] rxFreqHex = AlincoConverter.FrequencyToHex(rxFrequency).ToCharArray();
            char[] txFreqHex = AlincoConverter.FrequencyToHex(txFrequency).ToCharArray();
            char[] hexChannelName = AlincoConverter.ChannelNameToHex(channelName).ToCharArray();


            c[0] = (char)(CHANNEL_ENABLED + 0x30);
            c[1] = (char)(skip + 0x30);
            c[3] = (char)(mode + 0x30);
            c[5] = (char)(gain + 0x30);
            c[7] = (char)(agc + 0x30);
            c[9] = (char)(power + 0x30);
            c[13] = (char)(filter + 0x30);

            c[14] = (char)toneHex.ToCharArray()[0];
            c[15] = (char)toneHex.ToCharArray()[1];

            c[19] = (char)(noiseBlanker + 0x30);

            for (int i = 0; i < AlincoConverter.FREQ_LENGTH; i++)
            {
                c[32 + i] = rxFreqHex[i];
            }

            for (int i = 0; i < AlincoConverter.CHAN_LENGTH; i++)
            {
                c[40 + i] = hexChannelName[i];
            }

            for (int i = 0; i < AlincoConverter.FREQ_LENGTH; i++)
            {
                c[56 + i] = txFreqHex[i];
            }

            return new string(c);
        }

        public Channel(string encodedString, Bank bank, int channelNo)
        {
            this.bank = bank;
            this.channelNo = channelNo;

            skip = (Skip)Int32.Parse(encodedString.Substring(1, 1));
            mode = (Mode)Int32.Parse(encodedString.Substring(3, 1));
            gain = (Gain)Int32.Parse(encodedString.Substring(5, 1));
            agc = (AGC)Int32.Parse(encodedString.Substring(7, 1));
            power = (Power)Int32.Parse(encodedString.Substring(9, 1));
            filter = (Filter)Int32.Parse(encodedString.Substring(13, 1));
            tone = (Tone)Int32.Parse(encodedString.Substring(14, 2), System.Globalization.NumberStyles.HexNumber);
            noiseBlanker = (NoiseBlanker)Int32.Parse(encodedString.Substring(19, 1));
            rxFrequency = AlincoConverter.HexToFrequency(encodedString.Substring(32, AlincoConverter.FREQ_LENGTH));
            channelName = AlincoConverter.HexToChannelName(encodedString.Substring(40, AlincoConverter.CHAN_LENGTH));
            txFrequency = AlincoConverter.HexToFrequency(encodedString.Substring(56, AlincoConverter.FREQ_LENGTH));
        }

        public Channel(string encodedString, int channelNo) : this(encodedString, ChannelFromNumeric(channelNo).bank, ChannelFromNumeric(channelNo).channelNo) { }

    }
}
