using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alinco
{
    public class AlincoConverter
    {
        public const int FREQ_LENGTH = 8;
        public const int CHAN_LENGTH = 16;

        public static string ByteToHex(byte b)
        {
            byte[] byteArray = new byte[] { b };
            return BitConverter.ToString(byteArray);
        }

        public static string FrequencyToHex(int frequency)
        {
            byte[] bytes = BitConverter.GetBytes(frequency);

            return BitConverter.ToString(bytes).Replace("-", String.Empty);
        }

        public static int HexToFrequency(string hex)
        {
            int bigEndian = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            byte[] bytes = BitConverter.GetBytes(bigEndian);
            return BinaryPrimitives.ReadInt32BigEndian(bytes);
        }


        public static bool IsFrequencyValid(int frequency)
        {
            return frequency >= 1600000 && frequency <= 29999999;
        }

        public static string ChannelNameToHex(string channelName)
        {
            char[] chars = channelName.ToCharArray();
            StringBuilder sb = new StringBuilder();

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 0x39) // Non numeral Values are ASCII shifted by 7
                {
                    value -= 7;
                }
                if (value == 0x20) // Space is 0x54
                {
                    value = 0x54;
                }
                sb.Append(String.Format("{0:X}", value));
            }

            return PadStringWithZeros(sb.ToString(), CHAN_LENGTH);
        }

        public static string HexToChannelName(string hex)
        {
            hex = PadStringWithZeros(hex, CHAN_LENGTH);
            byte[] byteArray = StringToByteArray(hex);

            int len = byteArray.Length;

            for (int i = 0; i < len; i++)
            {
                if (byteArray[i] == 0x54)
                {
                    byteArray[i] = 0x20;
                }
                else
                {
                    if (byteArray[i] > 0x39 && byteArray[i] != 0x54)
                    {
                        byteArray[i] += 7;
                    }
                }
            }

            return System.Text.Encoding.ASCII.GetString(byteArray);
        }

        public static string PadStringWithZeros(string s, int targetLength)
        {
            if (s.Length > targetLength)
            {
                throw new ArgumentOutOfRangeException();
            }
            int padLength = targetLength - s.Length;
            return s + new string('0', padLength);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }

}
