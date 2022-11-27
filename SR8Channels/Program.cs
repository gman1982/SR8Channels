using System;
using System.Buffers.Binary;
using System.Linq;
using System.Text;

namespace Alinco
{
    class Program
    {
        const int FREQ_LENGTH = 8;
        const int CHAN_LENGTH = 16;

        static void Main(string[] args)
        {
            if (args.Length > 0 && string.Equals(args[0], "warranty", StringComparison.OrdinalIgnoreCase))
            {
                PrintWarranty();
            }


            if (args.Length < 3 || (!string.Equals(args[0], "export", StringComparison.OrdinalIgnoreCase)
                                   && !string.Equals(args[0], "import", StringComparison.OrdinalIgnoreCase)))
            {
                PrintHelp();
            }

            if (string.Equals(args[0], "warranty", StringComparison.OrdinalIgnoreCase))
            {
                PrintWarranty();
            }


            if (string.Equals(args[0], "export", StringComparison.OrdinalIgnoreCase))
            {
                AlincoFile alincoFile = new AlincoFile(args[1]);
                alincoFile.ExportCSV(args[2]);
            }

            if (string.Equals(args[0], "import", StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length < 3)
                {
                    PrintHelp();
                }

                AlincoFile alincoFile = new AlincoFile(args[1]);
                alincoFile.ReadCSV(args[2]);
                alincoFile.WriteSR8(args[3]);
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine(@"  SR8CSV-Converter  Copyright (C) 2022 by Gereon Schueller, DK2GB
    This program comes with ABSOLUTELY NO WARRANTY; for details type `warranty'.
    This is free software, and you are welcome to redistribute it
    under certain conditions; type `show c' for details.
");
            Console.WriteLine("Syntax: SR8CSV {export|import} SourceSR8 CSV [TargetSR8]");
            System.Environment.Exit(1);
        }

        private static void PrintWarranty()
        {
            Console.WriteLine(@"
Extract from the GPL Version 3, 29 June 2007
https://www.gnu.org/licenses/gpl-3.0.txt

             [...]
  15. Disclaimer of Warranty.

  THERE IS NO WARRANTY FOR THE PROGRAM, TO THE EXTENT PERMITTED BY
APPLICABLE LAW.  EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT
HOLDERS AND/OR OTHER PARTIES PROVIDE THE PROGRAM ""AS IS"" WITHOUT WARRANTY
OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.  THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM
IS WITH YOU.  SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF
ALL NECESSARY SERVICING, REPAIR OR CORRECTION.

  16. Limitation of Liability.

  IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING
WILL ANY COPYRIGHT HOLDER, OR ANY OTHER PARTY WHO MODIFIES AND/OR CONVEYS
THE PROGRAM AS PERMITTED ABOVE, BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY
GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE
USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED TO LOSS OF
DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD
PARTIES OR A FAILURE OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS),
EVEN IF SUCH HOLDER OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF
SUCH DAMAGES.

  17. Interpretation of Sections 15 and 16.

  If the disclaimer of warranty and limitation of liability provided
above cannot be given local legal effect according to their terms,
reviewing courts shall apply local law that most closely approximates
an absolute waiver of all civil liability in connection with the
Program, unless a warranty or assumption of liability accompanies a
copy of the Program in return for a fee.
");
            System.Environment.Exit(1);
        }
    }
}
