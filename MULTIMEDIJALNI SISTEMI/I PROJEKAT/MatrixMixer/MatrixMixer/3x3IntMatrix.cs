using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMixer
{
    public class _3x3IntMatrix
    {
        public int a00;
        public int a01;
        public int a02;
        public int a10;
        public int a11;
        public int a12;
        public int a20;
        public int a21;
        public int a22;

        public _3x3IntMatrix(int a00, int a01, int a02, int a10, int a11, int a12, int a20, int a21, int a22)
        {
            this.a00 = a00; 
            this.a01 = a01;
            this.a02 = a02;
            this.a10 = a10;
            this.a11 = a11;
            this.a12 = a12;
            this.a20 = a20;
            this.a21 = a21;
            this.a22 = a22;
        }
    }
}
