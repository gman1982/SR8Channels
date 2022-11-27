using System;
using System.Collections.Generic;
using System.Text;

namespace Alinco
{
    public static class AlincoEnums
    {
        public enum Bank
        {
            None = 0,
            A = 1,
            B = 2,
            VFO_A = 3,
            VFO_B = 4
        }

        public enum Skip
        {
            noSkip = 0,
            skip = 8
        }

        public enum Mode
        {
            USB = 0,
            LSB = 1,
            CWU = 2,
            CWL = 3,
            AM = 4,
            FM = 5
        }

        public enum Gain
        {
            db0 = 0,
            db10minus = 1,
            db20minus = 2,
            db10plus = 3
        }

        public enum AGC
        {
            Fast = 0, Slow = 1
        }

        public enum Power
        {
            High = 0,
            Low = 1,
            SuperLow = 2
        }

        public enum Filter
        {
            Wide = 0,
            Narrow = 1
        }

        public enum Tone
        {
            None = 0,
            Hz67_0 = 0x80,
            Hz69_3 = 0x81,
            Hz71_9 = 0x82,
            Hz74_4 = 0x83,
            Hz77_0 = 0x84,
            Hz79_7 = 0x85,
            Hz82_5 = 0x86,
            Hz85_4 = 0x87,
            Hz88_5 = 0x88,
            Hz91_5 = 0x89,
            Hz94_8 = 0x8A,
            Hz97_4 = 0x8B,
            Hz100_0 = 0x8C,
            Hz103_5 = 0x8D,
            Hz107_2 = 0x8E,
            Hz110_9 = 0x8F,
            Hz114_8 = 0x90,
            Hz118_8 = 0x91,
            Hz123_0 = 0x92,
            Hz127_3 = 0x93,
            Hz131_8 = 0x94,
            Hz136_5 = 0x95,
            Hz141_3 = 0x96,
            Hz146_2 = 0x97,
            Hz151_4 = 0x98,
            Hz156_7 = 0x99,
            Hz162_2 = 0x9A,
            Hz167_9 = 0x9B,
            Hz173_8 = 0x9C,
            Hz179_9 = 0x9D,
            Hz186_2 = 0x9E,
            Hz192_8 = 0x9F,
            Hz203_5 = 0xA0,
            Hz210_7 = 0xA1,
            Hz218_1 = 0xA2,
            Hz225_7 = 0xA3,
            Hz233_6 = 0xA4,
            Hz241_8 = 0xA5,
            Hz250_3 = 0xA6
        }

        public enum NoiseBlanker
        {
            Off = 0,
            On = 1,
        }

    }
}
