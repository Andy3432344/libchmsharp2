using System;
using System.Collections.Generic;
using System.IO;

using CHMsharp;

namespace NetCoreTest
{
    public class Program
    {
        private static string _outdir;
        private static string _fileName;

        public static void Main(string[] args)
        {
            if (args.Length != 1 && args.Length != 2)
            {
                Console.WriteLine("USAGE: NetCoreTest.exe <input file> [ <output directory> ]");
                return;
            }

            // check if we can find the source file
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("ERROR: cannot find " + args[0]);
                return;
            }

            // check if an output directory was specified
            if (args.Length == 1)
            {
                _outdir = Path.Combine(Path.GetDirectoryName(args[0]), Path.GetFileNameWithoutExtension(args[0]));
                if (!Directory.Exists(_outdir))
                {
                    Console.WriteLine("Creating output folder: " + _outdir);
                    Directory.CreateDirectory(_outdir);
                }
                else
                {
                    Console.WriteLine("Using output folder: " + _outdir);
                }
            }
            else
            {
                _outdir = args[1];
            }

            _fileName = args[0];
            var o = new object();
            var chmf = ChmFile.Open(_fileName);
            chmf.Enumerate(
                EnumerateLevel.Normal,
                EnumeratorCallback,
                o);
            chmf.Close();
        }

        private static EnumerateStatus EnumeratorCallback(ChmFile file, ChmUnitInfo ui, object context)
        {
            if (!ui.path.EndsWith("/"))
            {
                Console.WriteLine(_fileName + ": " + ui.path);
            }

            if (ui.length > 0)
            {
                var buf = new byte[ui.length];
                var ret = file.RetrieveObject(ui, ref buf, 0, buf.Length);

                if (ret > 0)
                {
                    try
                    {
                        var fi = new FileInfo(Path.Combine(_outdir, ui.path.Trim('/')));

                        List<DirectoryInfo> created;

                        fi.Directory.CreateDirectory(out created);
                        File.WriteAllBytes(fi.FullName, buf);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return EnumerateStatus.Continue;
        }
    }
}
