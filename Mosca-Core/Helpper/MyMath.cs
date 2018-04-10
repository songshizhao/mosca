//***************************************************************
//********自定义的数学函数；                                       *
//********  主程序全局函数，继承此类
//********      使用到的全局函数加入此类中
//********          创建/2018-3-22/宋仕钊
//********              上次编辑/2018-3-22/宋仕钊
//********                  通用函数使用开源数学函数库：https://numerics.mathdotnet.com/
//****************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
namespace MoscaCore
{
    class MatrixSolver
    {
        //系数矩阵
        private double[,] Matrix;
        private double[] Right;
        private double[] Result;
        private double[] Beta;
        private double[] Y;
        int N;

        public MatrixSolver(double[,] M, double[] F)
        {
            N = Right.Length;
            Matrix = M;
            Right = F;
            Result = new double[N];
            Beta = new double[N];
            Y = new double[N];
        }


        public double[] Caculate()
        {
            Beta[0] = Matrix[0, 1] / Matrix[0, 0];
            for (int i = 1; i < N-1; i++)
            {
                Beta[i] = Matrix[i, i + 1] / (Matrix[i, i] - Matrix[i, i - 1] * Beta[i - 1]);
            }
            Y[0] = Right[0] / Matrix[0, 0];
            for (int i = 1; i <N; i++)
            {
                Y[i] = (Right[i] - Matrix[i, i - 1] * Y[i-1]) / (Matrix[i,i] - Matrix[i,i-1] * Beta[i-1]);
            }
            Result[N - 1] = Y[N - 1];
            for (int i = N-2; i >0; i--)
            {
                Result[i] = Y[i]-Beta[i]*Result[i+1];
            }
            return Result;
        }

        void Tutorial()
        {

           
            //Matrix<double> A = DenseMatrix.OfArray(new double[,] {
            //    {1,1,1,1},
            //    {1,2,3,4},
            //    {4,3,2,1}});
            //Vector<double>[] nullspace = A.Kernel();

            //// verify: the following should be approximately (0,0,0)
            ////(A * (2 * nullspace[0] - 3 * nullspace[1]))


            //对于矩阵，有稠密的、稀疏和对角线三种，而向量则有稠密和稀疏的两种。


            //创建一个3行4列的矩阵，且所有的元素值均为0。
            Matrix<double> m1 = Matrix<double>.Build.Dense(3, 4);
            //创建一个3行4列的矩阵，所有的元素值均为介于0-1之间的随机数
            Matrix<double> m2 = Matrix<double>.Build.Random(3, 4);
            //创建一个3阶单位矩阵
            Matrix<double> m3 = Matrix<double>.Build.DenseIdentity(3);
            //创建一个4阶对角矩阵，对角线元素为1,3,5;对角矩阵
            Matrix<double> m4 = Matrix<double>.Build.Diagonal(new double[] { 1, 3, 5 });


            //不过很多时候，我们都是针对double类型的矩阵和向量，因此，完全可以建立一个构建器，如下：
            MatrixBuilder<double> M = Matrix<double>.Build;
            // 这样在书写的时候会得到简化。
            //Builder的函数一般都是以Dense、Sparse和Diagonal开头，在使用InteliSence的时候，能方便用户在构建不同类型矩阵时便于找到合适的方法。
            //以Dense方法为例，可以接受一个数组，按给定方式摆放：
            Matrix<double> m01 = M.Dense(3, 3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            //注意，该方法按列摆放，上面这个矩阵的形式是
            //1 4 7
            //2 5 8
            //3 6 9
            ///而不是
            //1 2 3
            //4 5 6
            //7 8 9
            //另外，Dense方法还可以使用一个lambda表达式，在创建矩阵的时候给元素赋值，例如：
            //使用lambda表达式初始化矩阵元素
            m1=M.Dense(3, 4, (i, j) => i + j);
            //其中i，j即代表了矩阵中元素的行标和列标（注意是从0开始，这一点可能和大部分线性代数教科书上不一致）。
            //这种方式在创建一些比较特殊的矩阵的时候会很有用，例如大名鼎鼎的希尔伯特矩阵：
            m1=M.Dense(4, 4, (i, j) => 1.0/(i + j+1));
            //有时候，我们已经具备了和矩阵类似结构的其他结构（矩阵本身，或者二维数组等），这时可以采用带“Of”的方法，复制这一数据结构。
            double[,] a = new double[,]
            {                           
                {1,3,5},                           
                {2,-1,3},                            
                {3,3,-1}
            };
            Matrix<double> m02 = M.DenseOfArray(a);
            //如果想按行创建一个矩阵，可以使用DenseOfRowArrays方法
            //            /*
            //             * 4×2矩阵
            //             * 1 2 3 4
            //             * 4 3 2 1
            //             */
            Matrix<double> m03 = M.DenseOfRowArrays(new double[] { 1, 2, 3, 4 }, new double[] { 4, 3, 2, 1 });
            //对于向量，其创建方法类似，这里就不再举例了。总之，创建的方法很多，总有一款适合你。
            //矩阵的修改
            //例如已经创建了一个矩阵m，修改其中一个元素，和对二维矩阵赋值的操作是类似的：
            m03[2, 1]=4;//第三行第二列元素值为4

            //对角矩阵
            var diagMaxtrix = M.DenseDiagonal(3, 3, 5);


            // 1.使用LU分解方法求解



            //由于求解线性方程组主要用到了矩阵的分解，Math.NET实现了5种矩阵分解的算法：
            //LU，
            //QR，
            //Svd，
            //Evd，
            //Cholesky。
            //而GramSchmidt是继承QR的，每一个都是实现了ISolver<T> 接口，因此就可以直接使用矩阵的分解功能，直接进行线性方程组的求解。


            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";

            //先创建系数矩阵A 
            var matrixA = DenseMatrix.OfArray(new[,] { { 5.00, 2.00, -4.00 }, { 3.00, -7.00, 6.00 }, { 4.00, 1.00, 5.00 } });

            //创建向量b
            var vectorB = new DenseVector(new[] { -7.0, 38.0, 43.0 });

            // 1.使用LU分解方法求解
            var resultX = matrixA.LU().Solve(vectorB);
            //Console.WriteLine(@"1. Solution using LU decomposition");
            //Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));

            // 2.使用QR分解方法求解
            resultX = matrixA.QR().Solve(vectorB);
            //Console.WriteLine(@"2. Solution using QR decomposition");
            //Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));

            // 3. 使用SVD分解方法求解
            matrixA.Svd().Solve(vectorB, resultX);
            //Console.WriteLine(@"3. Solution using SVD decomposition");
            //Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));

            // 4.使用Gram-Shmidt分解方法求解
            matrixA.GramSchmidt().Solve(vectorB, resultX);
            //Console.WriteLine(@"4. Solution using Gram-Shmidt decomposition");
            //Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));

            // 5.验证结果，就是把结果和A相乘，看看和b是否相等
            var reconstructVecorB = matrixA * resultX;
            //Console.WriteLine(@"5. Multiply coefficient matrix 'A' by result vector 'x'");
            //Console.WriteLine(reconstructVecorB.ToString("#0.00\t", formatProvider));









        }
















    }
}
