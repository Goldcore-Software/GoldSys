using libDotNetClr;
using LibDotNetParser.CILApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSysKernel.Core
{
    internal class Process
    {
        public DotNetClr clr;
        public DotNetFile fl;
        public Process(DotNetFile file)
        {
            fl = file;
            clr = new DotNetClr(fl);
        }
        public Process(byte[] file)
        {
            fl = new DotNetFile(file);
            clr = new DotNetClr(fl);
        }
        public Process(DotNetFile file, DotNetClr dclr)
        {
            fl = file;
            clr = dclr;
        }
    }
}
