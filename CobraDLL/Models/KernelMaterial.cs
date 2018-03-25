
/////////////////////////////////////////////////////////////
//********COBRA MODEL CLASS
//********FIRST CREATED BY SONGSHIZHAO @ 2017年12月18日17:53:47
//********内置模型参数
//********LASTEST EDITED BY SONGSHIZHAO @ ？？？？
//********END
/////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobraDLL.Models
{
    ///<summary>内部物性参数</summary>
    public class KernelMaterial
    {
        /// <summary>内置的UO2材料</summary>
        public static Material UO2 = new Material()
        {
            Index = -1,
            Name = "UO2",
            Type = "GetPropertyByName",
            K = new KCollection()
            {
                Value = -1,
                KData = new List<Data>(){
                new Data(){T=0,Value=9.5},
                new Data(){T=100,Value=7.61},
                new Data(){T=200,Value=6.35},
                new Data(){T=300,Value=5.44},
                new Data(){T=400,Value=4.77},
                new Data(){T=500,Value=4.24},
                new Data(){T=600,Value=3.82},
                new Data(){T=700,Value=3.47},
                new Data(){T=800,Value=3.19},
                new Data(){T=900,Value=2.92},
                new Data(){T=1000,Value=2.74},
                new Data(){T=1100,Value=2.56},
                new Data(){T=1200,Value=2.40},
                new Data(){T=1300,Value=2.26},
                new Data(){T=1400,Value=2.14},
                new Data(){T=1500,Value=2.04},
                new Data(){T=1600,Value=1.94},
                new Data(){T=1700,Value=1.86},
                new Data(){T=1800,Value=1.78},
                new Data(){T=1900,Value=1.71},
                new Data(){T=2000,Value=1.65},
                new Data(){T=2100,Value=1.59},
                new Data(){T=2200,Value=1.54},
                new Data(){T=2300,Value=1.50},
                new Data(){T=2400,Value=1.46},
                new Data(){T=2500,Value=1.42},
                new Data(){T=2600,Value=1.39},
                },
            },
            Cp = new CpCollection()
            {
                Value = -1,
                CpData = new List<Data>(){                
                    new Data(){T=0,Value=304.38},
                    new Data(){T=100,Value=306.896},               
                    new Data(){T=200,Value=309.424},                
                    new Data(){T=300,Value=311.964},               
                    new Data(){T=400,Value=314.516},               
                    new Data(){T=500,Value=317.08},               
                    new Data(){T=600,Value=319.656},              
                    new Data(){T=700,Value=322.244},              
                    new Data(){T=800,Value=324.844},              
                    new Data(){T=900,Value=327.456},               
                    new Data(){T=1000,Value=330.08},              
                    new Data(){T=1100,Value=332.716},     
                    new Data(){T=1200,Value=335.364},              
                    new Data(){T=1300,Value=340},              
                    new Data(){T=1400,Value=345},              
                    new Data(){T=1500,Value=350},                   
                    new Data(){T=1600,Value=370},      
                    new Data(){T=1700,Value=390},   
                    new Data(){T=1800,Value=410},   
                    new Data(){T=1900,Value=430},    
                    new Data(){T=2000,Value=450}, 
                    new Data(){T=2100,Value=470},  
                    new Data(){T=2200,Value=500}, 
                    new Data(){T=2300,Value=520},
                    new Data(){T=2400,Value=580},
                    new Data(){T=2500,Value=620}, 
                    new Data(){T=2600,Value=650},
                },
            }



        };
        /// <summary>内置的Zr4材料</summary>
        public static Material Zr4 = new Material()
        {
            Index = -1,
            Name = "Zr4",
            Type = "GetPropertyByName",
            K = new KCollection()
            {
                Value = -1,
                KData = new List<Data>()            
                {
                new Data(){T=0,Value=7.93},
                new Data(){T=100,Value=10.81},
                new Data(){T=200,Value=13.21},
                new Data(){T=300,Value=15.22},
                new Data(){T=400,Value=16.93},
                new Data(){T=500,Value=18.45},
                new Data(){T=600,Value=19.85},
                new Data(){T=700,Value=21.24},
                new Data(){T=800,Value=22.71},
                new Data(){T=900,Value=24.35},
                new Data(){T=1000,Value=26.25},
                new Data(){T=1100,Value=28.51},
                new Data(){T=1200,Value=31.22},
                new Data(){T=1300,Value=34.47},
                new Data(){T=1400,Value=38.36},
                new Data(){T=1500,Value=42.99},  
                },
            },
            Cp = new CpCollection()
            {
                Value = -1,
                CpData = new List<Data>()
           
                {
                new Data(){T=0,Value=286.5},
                new Data(){T=100,Value=296.5},
                new Data(){T=200,Value=306.5},
                new Data(){T=300,Value=316.5},
                new Data(){T=400,Value=326.5},
                new Data(){T=500,Value=336.5},
                new Data(){T=600,Value=346.5},
                new Data(){T=700,Value=356.5},
                new Data(){T=800,Value=360},
                new Data(){T=2000,Value=360},
                },
            }

        };

        /// <summary>内置的Zr2材料，目前与Zr4相同</summary>
        public static Material Zr2 = new Material()
        {
            Index = -1,
            Name = "Zr4",
            Type = "GetPropertyByName",
            K = new KCollection()
            {
                Value = -1,
                KData = new List<Data>()            
                {
                new Data(){T=0,Value=7.93},
                new Data(){T=100,Value=10.81},
                new Data(){T=200,Value=13.21},
                new Data(){T=300,Value=15.22},
                new Data(){T=400,Value=16.93},
                new Data(){T=500,Value=18.45},
                new Data(){T=600,Value=19.85},
                new Data(){T=700,Value=21.24},
                new Data(){T=800,Value=22.71},
                new Data(){T=900,Value=24.35},
                new Data(){T=1000,Value=26.25},
                new Data(){T=1100,Value=28.51},
                new Data(){T=1200,Value=31.22},
                new Data(){T=1300,Value=34.47},
                new Data(){T=1400,Value=38.36},
                new Data(){T=1500,Value=42.99},  
                },
            },
            Cp = new CpCollection()
            {
                Value = -1,
                CpData = new List<Data>()
           
                {
                new Data(){T=0,Value=286.5},
                new Data(){T=100,Value=296.5},
                new Data(){T=200,Value=306.5},
                new Data(){T=300,Value=316.5},
                new Data(){T=400,Value=326.5},
                new Data(){T=500,Value=336.5},
                new Data(){T=600,Value=346.5},
                new Data(){T=700,Value=356.5},
                new Data(){T=800,Value=360},
                new Data(){T=2000,Value=360},
                },
            }

        };

    }
}
