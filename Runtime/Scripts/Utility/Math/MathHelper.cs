
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LJVoyage.Game.Utility
{
    public static class MathHelper
    {
       

        /// <summary>
        /// 标准化数据
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float[] NormalizeData(IEnumerable<float> data, float min, float max)
        {
            float dataMax = data.Max();
            float dataMin = data.Min();
            float range = dataMax - dataMin;

            return data
                .Select(d => (d - dataMin) / range)
                .Select(n => (float)((1 - n) * min + n * max))
                .ToArray();
        }

        /// <summary>
        /// 余弦相似性
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float CosineSimilarity(float[] a, float[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("ab 长度不一致");
            }

            a= NormalizeData(a,0,1);

            b = NormalizeData(b, 0, 1);

            var aSquareSum = a.Select(i => i * i).Sum();

            var bSquareSum = b.Select(i => i * i).Sum();

            var sum = 0f;

            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }

            return (float)(sum /(Math.Sqrt(bSquareSum) + Math.Sqrt(aSquareSum)));

        }

        public static float SimpleDTW(float[] a, float[] b ,Func<float, float, float> func )
        {
            var count = a.Length;


            if (a.Length != b.Length)
            {
                throw new Exception("ab 长度不一致");
            }

            a = NormalizeData(a, -1, 1);

            b = NormalizeData(b, -1, 1);

            //距离矩阵
            var distanceMatrix = new float[a.Length,b.Length];

            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {  
                    ///填充 y轴
                    distanceMatrix[i, 0] = func(a[i], b[i]);

                    ///填充 x轴
                   // distanceMatrix[0, i] = func(a[i], b[i]);

                }
                else
                {   
                    ///填充 y轴
                    distanceMatrix[i, 0] = func(a[i], b[i]) + distanceMatrix[i -1, 0];

                    ///填充 x轴
                    distanceMatrix[ 0, i] = func(a[i], b[i]) + distanceMatrix[ 0, i -1];

                }
            }

            ///填充内部矩阵
            for (int i = 1; i < count; i++)
            {
                for (int j = 1; j < count; j++)
                {
                    distanceMatrix[i, j] = Math.Min(distanceMatrix[i - 1, j], Math.Min(distanceMatrix[i, j - 1], distanceMatrix[i - 1, j - 1]));
                    distanceMatrix[i, j] += func(a[i], b[i]);
                }
            }

            var dis =new  List<float>();

            // var path  = new List<Vector2>();
            //x
            var width = count - 1;
            //y
            var height = width;

            dis.Add(distanceMatrix[width, height]);

            while (width > 0 && height > 0)
            {

                dis.Add(Math.Min(distanceMatrix[height - 1, width], Math.Min(distanceMatrix[height, width - 1], distanceMatrix[height - 1, width - 1])));

                if (dis.Last() == distanceMatrix[height - 1, width])
                {
                    height--;
                }
                else if (dis.Last() == distanceMatrix[height, width - 1])
                {
                    width--;
                }
                else if (dis.Last() == distanceMatrix[height-1, width - 1])
                {
                    width--; 
                    height--;
                }

                //  path.Add(new Vector2(height,width));
            }


            if (width != 0 && height ==0)
            {
                for (int i = width; i >= 0; i--)
                {
                    dis.Add(distanceMatrix[0, i]);
                }
            }
            else if (height != 0 && width ==0)
            {
                for (int i = height; i >= 0; i--)
                {
                    dis.Add(distanceMatrix[i, 0]);
                }
            }

            var average = dis.Sum() / dis.Count;

            return 1 / (average + 1);
        }

        public static float DTWDistance(float a, float b)
        {
            return Math.Abs(a - b);
        }


        /// <summary>
        /// 数字转汉字（支持所有int型数字）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumberToChinese(int number)
        {
            string[] UNITS = { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千" };
            string[] NUMS = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            if (number == 0)
            {
                return NUMS[0];
            }
            string results = "";
            for (int i = number.ToString().Length - 1; i >= 0; i--)
            {
                int r = (int)(number / (Math.Pow(10, i)));
                results += NUMS[r % 10] + UNITS[i];
            }
            results = results.Replace("零十", "零")
                             .Replace("零百", "零")
                             .Replace("零千", "零")
                             .Replace("亿万", "亿");
            results = Regex.Replace(results, "零([万, 亿])", "$1");
            results = Regex.Replace(results, "零+", "零");

            if (results.StartsWith("一十"))
            {
                results = results.Substring(1);
            }
        cutzero:
            if (results.EndsWith("零"))
            {
                results = results.Substring(0, results.Length - 1);
                if (results.EndsWith("零"))
                {
                    goto cutzero;
                }
            }
            return results;
        }

    }

}
