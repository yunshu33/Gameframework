using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Yun.Tools
{
    public class ExcelTool : MonoBehaviour
    {
        /// <summary>
        /// 读取表数据，生成对应的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">excel文件全路径</param>
        /// <param name="readTables">需要读取多少行</param> 
        /// <returns></returns>
        public static IList<T> CreateItemArrayWithExcel<T>(string filePath, int readTables = 1)
        {
            //获得表数据
            int columnNum = 0, rowNum = 0, tables = 0;

            DataSet result = ReadExcel(filePath);
            tables = result.Tables.Count;
            if (readTables > tables)
            {
                Debug.LogError("该表格只有" + tables + "行");
                readTables = tables;
            }
            //根据excel的定义，第二行开始才是数据
            IList<T> tlist = new List<T>();

            System.Type t = typeof(T);
            Assembly ass = Assembly.GetAssembly(t);//获取泛型的程序集
            PropertyInfo[] pc = t.GetProperties();//获取到泛型所有属性的集合
                                                  //object item = ass.CreateInstance(t.FullName);
            for (int index = 0; index < readTables; index++)
            {
                string tableName = result.Tables[index].TableName;
                //Tables[0] 下标0表示excel文件中第一张表的数据
                columnNum = result.Tables[index].Columns.Count;
                rowNum = result.Tables[index].Rows.Count;

                for (int i = 1; i < rowNum; i++)
                {

                    object _obj = ass.CreateInstance(t.FullName);
                    //泛型实例化
                    //Object obj = Activator.CreateInstance<T>(); 或者这样也可以实例化

                    //第一个 变量赋值表名
                    pc[0].SetValue((T)_obj, tableName, null);


                    for (int j = 0, strint = 1; j < columnNum; j++, strint++)
                    {
                        //if (pi.PropertyType.Equals(typeof(string)))//判断属性的类型是不是String


                        if (pc[strint].PropertyType.Equals(typeof(List<string>)))
                        {
                            //进入List 循环赋值
                            //局部变量切断引用关系
                            List<string> str = new List<string>();

                            for (int k = 0; k < 3; k++)
                            {
                                str.Add(result.Tables[index].Rows[i][j + k].ToString());
                            }
                            pc[strint].SetValue((T)_obj, str, null);
                            j += 2;
                        }
                        else
                        {
                            if (result.Tables[index].Rows[i][j].ToString() == null)
                            {
                                break;
                            }
                            pc[strint].SetValue((T)_obj, result.Tables[index].Rows[i][j].ToString(), null);//给泛型的属性赋值
                        }
                    }

                    tlist.Add((T)_obj);

                    //解析每列的数据
                    //item.itemId = uint.Parse(collect[i][0].ToString());
                    //item.itemName = collect[i][1].ToString();
                    //item.itemPrice = uint.Parse(collect[i][2].ToString());
                    //array[i - 1] = item;
                }
            }

            return tlist;
        }



        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        static DataSet ReadExcel(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet();

            return result;
        }
    }
}

