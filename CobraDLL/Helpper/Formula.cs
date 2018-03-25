//***************************************************************
//********Formula CLASS；                                       *
//********  主程序全局函数，继承此类
//********      使用到的全局函数加入此类中
//********          创建/2018-3-22/宋仕钊
//********              上次编辑/2018-3-22/宋仕钊
//****************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobraDLL
{
    public class Formula
    {
        /// <summary>
        /// 计算摩擦因子
        /// </summary>
        /// <param name="Re">雷诺数</param>
        /// <param name="a">缺省0.32</param>
        /// <param name="b">缺省-0.25</param>
        /// <param name="c">缺省0</param>
        /// <returns></returns>
        public double FrictionFactor(double Re, double a=0.32, double b=-0.25,double c=0)
        {
            double r = a / Math.Pow(Re, b) + c;
            return r;
        }

        /// <summary>
        /// 经验公式，计算努塞尔数
        /// </summary>
        /// <param name="Re">雷诺数</param>
        /// <param name="Pr">普朗特数</param>
        /// <returns>努塞尔数</returns>
        public double Nu(double Re, double Pr)
        {
            return 0.023 * Math.Pow(Re, 0.8) * Math.Pow(Pr, 0.4);
        }
        //[计算对流换热系数]
        public double h_convect(double Re, double Pr, double Lamd, double de)
        {
            return Nu(Re, Pr) * Lamd / de;
        }

        /// <summary>
        /// 计算临界热流密度
        /// </summary>
        /// <param name="Xe">热平衡寒气率</param>
        /// <param name="d">等效水力直径</param>
        /// <param name="G">质量流密度</param>
        /// <param name="h_f">饱和比焓</param>
        /// <param name="h_in">入口比焓</param>
        /// <param name="formlula">公式选取1~n</param>
        /// <returns></returns>
        public double Q_Critical(double Xe, double d, double G, double h_f, double h_in, int formlula)
        {
            double Qc = 0;

            switch (formlula)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    Qc = W3_Formula(Xe, d, G, h_f, h_in);

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

                default:
                    break;
            }

            //            DNBR临界热流密度关系式选用
            //= 0，不进行CHF分析；
            //= 1，BA & W - 2关系式 *
            //     = 2，W - 3关系式
            //        = 3，EPRI关系式 *
            //         = 4，Macbeth 关系式（12系数）*
            //          = 5，Macbeth 关系式（6系数）*
            //           = 6，Biasi 关系式*
            //            = 7，改进Barnett关系式 *


            return Qc;
        }


        //[W3临界热流密度关系式]
        public double W3_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }



        //[EPRI临界热流密度关系式]
        public double EPRI_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }



        //[Biasi临界热流密度关系式]
        public double Biasi_Formula(double Xe, double d, double G, double h_f, double h_in)
        {
            if (Xe < -0.15) { Xe = -0.15; }
            double Qc = 3154600 * (2.022 - 6.24 * 0.148 + (0.1722 - 1.43 * 0.148) * Math.Exp((18.177 - 5.99 * 1.48) * Xe)) * ((0.1484 - 1.596 * Xe + 0.1729 * Math.Abs(Xe) * Xe) * 7.374 / 10000 * G + 1.037) * (1.157 - 0.869 * Xe) * (0.2664 + 0.8357 * Math.Exp(-124.1 * d)) * (0.8258 + 3.41 / 10000000 * (h_f - h_in));
            return Qc;
        }


    }
}
