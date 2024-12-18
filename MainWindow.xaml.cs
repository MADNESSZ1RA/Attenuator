using System;
using System.Windows;
using ClosedXML.Excel;

namespace Attenuator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                double Z = double.Parse(input_z.Text);    // Импеданс
                double R = double.Parse(input_r.Text);    // Сопротивление R
                //double R1 = double.Parse(input_r1.Text);  // Сопротивление R1 (не используется в расчете L)

                double A = (R-Z)/(R+Z);
                double L = -20 * Math.Log10(A);

                output.Text = $"Вычисленное значение L: {L:F3} дБ";
            }
            catch (Exception ex)
            {
                output.Text = $"Ошибка: {ex.Message}";
            }
        }
        public void SaveToExcel(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Расчеты");
                    worksheet.Cell(1, 1).Value = "Z";
                    worksheet.Cell(1, 2).Value = "R";
                    worksheet.Cell(1, 3).Value = "Результат значения L";

                    int row = 2;

                    for (int Z = 1; Z <= 1000; Z++)
                    {
                        for (int R = 1; R <= 1000; R++)
                        {
                            try
                            {
                                if (R <= Z)
                                {
                                    worksheet.Cell(row, 1).Value = Z;
                                    worksheet.Cell(row, 2).Value = R;
                                    worksheet.Cell(row, 3).Value = "Ошибка: R должно быть больше Z.";
                                    row++;
                                    continue;
                                }

                                double A = (double)(R - Z) / (R + Z);

                                if (A <= 0 || A > 1)
                                {
                                    worksheet.Cell(row, 1).Value = Z;
                                    worksheet.Cell(row, 2).Value = R;
                                    worksheet.Cell(row, 3).Value = "Ошибка: Некорректное значение A.";
                                    row++;
                                    continue;
                                }

                                double L = -20 * Math.Log10(A);

                                worksheet.Cell(row, 1).Value = Z;
                                worksheet.Cell(row, 2).Value = R;
                                worksheet.Cell(row, 3).Value = L.ToString("F3") + " дБ";
                            }
                            catch
                            {
                                worksheet.Cell(row, 1).Value = Z;
                                worksheet.Cell(row, 2).Value = R;
                                worksheet.Cell(row, 3).Value = "Ошибка при расчете.";
                            }
                            row++;
                        }
                    }

                    string filePath = "Расчеты.xlsx";
                    workbook.SaveAs(filePath);

                    output.Text = $"Файл успешно сохранен: {filePath}";
                }
            }
            catch (Exception ex)
            {
                output.Text = $"Ошибка: {ex.Message}";
            }
        }

    }
}
