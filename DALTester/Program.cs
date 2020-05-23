using DAL;
using DAL.Concrete;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace DALTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            EFContext ef = new EFContext();
            SqlRepository sr = new SqlRepository(ef);
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\Download\internetmag.xls");
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            int categoryId = 0;
            int subcategoryId = 0;
            string prName = "";
            string code = "";
            string price = "";
            string price2 = "";
            string descr = "";
            string image = "";
            
            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 2; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null
                        && xlRange.Cells[i, j].Value2.ToString() == "Розпродаж")
                    {
                        i = rowCount + 1;
                        j = colCount + 1;
                    }
                    switch (j)
                    {
                        case 2:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null
                                && xlRange.Cells[i, j + 1].Value2 == null)
                            {
                                if (Char.IsUpper(xlRange.Cells[i, j].Value2.ToString()[0]))
                                {
                                    string categName = xlRange.Cells[i, j].Value2.ToString();
                                    Console.Write("Category: " + xlRange.Cells[i, j].Value2.ToString());
                                    //ef.Set<Category>().Add(new Category { Name = categName });
                                    //ef.SaveChanges();
                                    //categoryId = ef.Set<Category>().FirstOrDefault(c => c.Name == categName).Id;
                                }
                                else
                                {
                                    string subcategName = xlRange.Cells[i, j].Value2.ToString();
                                    Console.Write("Subcategory: " + xlRange.Cells[i, j].Value2.ToString());
                                    //ef.Set<Subcategory>().Add(new Subcategory { Name = subcategName, CategoryId = categoryId });
                                    //ef.SaveChanges();
                                    //subcategoryId = ef.Set<Subcategory>().FirstOrDefault(c => c.Name == subcategName).Id;
                                }
                                j = colCount + 1;
                            }
                            else
                            {
                                prName = xlRange.Cells[i, j].Value2.ToString();
                                Console.Write("Name: " + xlRange.Cells[i, j].Value2.ToString());
                            }
                            Console.WriteLine();
                            break;
                        case 3:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                code = xlRange.Cells[i, j].Value2.ToString();
                                Console.Write("Code: " + xlRange.Cells[i, j].Value2.ToString());
                            }
                            Console.WriteLine();
                            break;
                        case 5:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                price = xlRange.Cells[i, j].Value2.ToString().Replace(" ", "").Replace('.', ',');
                                Console.Write("Price: " + xlRange.Cells[i, j].Value2.ToString());
                            }
                            Console.WriteLine();
                            break;
                        case 6:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                price2 = xlRange.Cells[i, j].Value2.ToString().Replace(" ", "").Replace('.', ',');
                                Console.Write("Price #2: " + xlRange.Cells[i, j].Value2.ToString());
                            }
                            Console.WriteLine();
                            break;
                        case 11:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                descr = xlRange.Cells[i, j].Value2.ToString();
                                Console.Write("Description: " + xlRange.Cells[i, j].Value2.ToString());
                                //ef.Set<Product>().Add(new Product
                                //{
                                //    Name = prName,
                                //    Code = code,
                                //    Description = descr,
                                //    Price = decimal.Parse(price),
                                //    PriceCMV = decimal.Parse(price2),
                                //    StockCount = 10,
                                //    SubcategoryId = subcategoryId,
                                //    ManufacturerId = rnd.Next(1, 5)
                                //});
                                //ef.SaveChanges();
                            }
                            Console.WriteLine();
                            break;
                        case 15:
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                image = xlRange.Cells[i, j].Value2.ToString();
                                Console.Write("Image: " + xlRange.Cells[i, j].Value2.ToString());
                                if (image == " " || image == "")
                                {
                                    var product = ef.Set<Product>().FirstOrDefault(p => p.Name == prName);
                                    product.Image = null;
                                    sr.Update<Product>(product);
                                    sr.SaveChanges();
                                }
                            }
                            Console.WriteLine();
                            break;
                    }
                }
            }
            //for (int i = 1; i <= rowCount; i++)
            //{
            //    for (int j = 1; j <= colCount; j++)
            //    {
            //        //new line
            //        if (j == 1)
            //            Console.Write("\r\n");
            //
            //        //write the value to the console
            //        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
            //            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
            //
            //        //add useful things here!   
            //    }
            //}
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            //foreach (var item in ef.Set<UserProfile>())
            //{
            //    Console.WriteLine(item.Id + ""  + item.FirstName);
            //}

            //SqlRepository sr = new SqlRepository();
            //
            //sr.Add(new Country { Name = "Украина" });
            //sr.Add(new Country { Name = "Росия" });
            //sr.Add(new Country { Name = "Китай" });
            //sr.Add(new Country { Name = "США" });
            //
            //foreach (var item in sr.GetAll<Country>())
            //{
            //    Console.WriteLine(item.Name);
            //}
        }
    }
}
