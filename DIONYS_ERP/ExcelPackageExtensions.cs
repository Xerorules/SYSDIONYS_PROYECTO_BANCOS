using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DIONYS_ERP
{
    public static class ExcelPackageExtensions
    {
        /*>>>>>>>---------------------------------------PLANTILLA EXCEL ---------------------------------------------<<<<<<<*/
        public static DataTable ToDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[15, 1, 15,8])//EL PRIMER Y TERCER PARAMETRO INDICAN EN N° DE LA CELDA VERTICAL LA CABECERA
            {
                table.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 16; rowNumber <= workSheet.Dimension.End.Row;/*FINAL DE LA TABLA*/ rowNumber++)//DONDE EMPIEZA A CONTAR LA TABLA ROWNUMBER
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, 8];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
    }
}