using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOperator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace MobileOperator.ViewModel
{
    class DetailingWindowViewModel : INotifyPropertyChanged
    {
        private int userId, status;
        private ClientModel client;
        Window window;
        private DetailingModel detailing;
        private DateTime from = new DateTime(2021, 01, 01);
        private DateTime till = DateTime.Now;
        public ObservableCollection<CallModel> Calls { get; set; }

        public DetailingWindowViewModel(int userId, int status, Window window)
        {
            this.userId = userId;
            this.status = status;
            this.window = window;
            if (status == 2)
                client = new ULModel(userId);
            else
                client = new FLModel(userId);
            Calls = new ObservableCollection<CallModel> { };
        }

        public DateTime From
        {
            get { return from; }
            set { from = value; OnPropertyChanged("From"); }
        }
        public DateTime Till
        {
            get { return till; }
            set { till = value; OnPropertyChanged("Till"); }
        }

        private RelayCommand getDetailingCommand;
        public RelayCommand GetDetailingCommand
        {
            get
            {
                return getDetailingCommand ??
                  (getDetailingCommand = new RelayCommand(obj =>
                  {
                      if (Calls != null)
                          Calls.Clear();
                      detailing = new DetailingModel(userId, client.Number, from, till);
                      foreach (CallModel call in detailing.AllCalls)
                          Calls.Add(call);
                  }));
            }
            set { }
        }

        private RelayCommand printToExelDetailingCommand;
        public RelayCommand PrintToExelDetailingCommand
        {
            get
            {
                return printToExelDetailingCommand ??
                  (printToExelDetailingCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          Excel.Application ExcelApp = new Excel.Application();
                          ExcelApp.Application.Workbooks.Add(Type.Missing);
                          Excel.Worksheet workSheet = (Excel.Worksheet)ExcelApp.ActiveSheet;
                          workSheet.Columns.ColumnWidth = 20;
                          workSheet.Cells[1, 1] = "Собеседник";
                          workSheet.Cells[1, 2] = "Тип соединения";
                          workSheet.Cells[1, 3] = "Направление";
                          workSheet.Cells[1, 4] = "Списание (руб)";
                          workSheet.Cells[1, 5] = "Время";
                          workSheet.Cells[1, 6] = "Продолжительность";

                          for (int i = 0; i < Calls.Count; i++)
                          {
                              workSheet.Cells[i + 2, 1] = (Calls[i].Number).ToString();
                              workSheet.Cells[i + 2, 2] = (Calls[i].TypeName).ToString();
                              workSheet.Cells[i + 2, 3] = (Calls[i].Direction).ToString();
                              workSheet.Cells[i + 2, 4] = (Calls[i].Cost).ToString();
                              workSheet.Cells[i + 2, 5] = (Calls[i].Time).ToString();
                              workSheet.Cells[i + 2, 6] = (Calls[i].Duration).ToString();
                          }
                          SaveFileDialog svg = new SaveFileDialog();
                          if (svg.ShowDialog() == true)
                          {
                              string pathToXmlFile;
                              pathToXmlFile = svg.FileName + ".xlsx";
                              workSheet.SaveAs(pathToXmlFile);
                              MessageBox.Show("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand printToWordDetailingCommand;
        public RelayCommand PrintToWordDetailingCommand
        {
            get
            {
                return printToWordDetailingCommand ??
                  (printToWordDetailingCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          Word.Application application = new Word.Application();
                          Object missing = Type.Missing;
                          application.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                          Word.Document document = application.ActiveDocument;
                          Word.Range range = application.Selection.Range;
                          Object behiavor = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
                          Object autoFitBehiavor = Word.WdAutoFitBehavior.wdAutoFitFixed;
                          document.Tables.Add(range, Calls.Count, 6, ref behiavor, ref autoFitBehiavor);
                          document.Tables[1].Cell(1, 1).Range.Text = "Собеседник";
                          document.Tables[1].Cell(1, 2).Range.Text = "Тип соединения";
                          document.Tables[1].Cell(1, 3).Range.Text = "Направление";
                          document.Tables[1].Cell(1, 4).Range.Text = "Списание (руб)";
                          document.Tables[1].Cell(1, 5).Range.Text = "Время";
                          document.Tables[1].Cell(1, 6).Range.Text = "Продолжительность";
                          for (int i = 0; i < Calls.Count; i++)
                          {
                              document.Tables[1].Cell(i + 2, 1).Range.Text = (Calls[i].Number).ToString();
                              document.Tables[1].Cell(i + 2, 2).Range.Text = (Calls[i].TypeName).ToString();
                              document.Tables[1].Cell(i + 2, 3).Range.Text = (Calls[i].Direction).ToString();
                              document.Tables[1].Cell(i + 2, 4).Range.Text = (Calls[i].Cost).ToString();
                              document.Tables[1].Cell(i + 2, 5).Range.Text = (Calls[i].Time).ToString();
                              document.Tables[1].Cell(i + 2, 6).Range.Text = (Calls[i].Duration).ToString();
                          }
                          SaveFileDialog svg = new SaveFileDialog();
                          if (svg.ShowDialog() == true)
                          {
                              string pathToDocFile;
                              pathToDocFile = svg.FileName + ".docx";
                              document.SaveAs(pathToDocFile);
                              MessageBox.Show("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand openRates;
        public RelayCommand OpenRates
        {
            get
            {
                return openRates ??
                  (openRates = new RelayCommand(obj =>
                  {
                      RatesWindow rates = new RatesWindow(userId, status);
                      rates.WindowState = window.WindowState;
                      rates.Top = window.Top;
                      rates.Left = window.Left;
                      rates.Height = window.Height;
                      rates.Width = window.Width;
                      rates.Show();
                      window.Close();
                  }));
            }
        }
        private RelayCommand openServices;
        public RelayCommand OpenServices
        {
            get
            {
                return openServices ??
                  (openServices = new RelayCommand(obj =>
                  {
                      ServicesWindow services = new ServicesWindow(userId, status);
                      services.WindowState = window.WindowState;
                      services.Top = window.Top;
                      services.Left = window.Left;
                      services.Height = window.Height;
                      services.Width = window.Width;
                      services.Show();
                      window.Close();
                  }));
            }
        }

        private RelayCommand openMainWindow;
        public RelayCommand OpenMainWindow
        {
            get
            {
                return openMainWindow ??
                  (openMainWindow = new RelayCommand(obj =>
                  {
                      MainWindow main = new MainWindow(userId, status);
                      main.WindowState = window.WindowState;
                      main.Top = window.Top;
                      main.Left = window.Left;
                      main.Height = window.Height;
                      main.Width = window.Width;
                      main.Show();
                      window.Close();
                  }));
            }
        }
        private RelayCommand openDetailing2;
        public RelayCommand OpenDetailing2
        {
            get
            {
                return openDetailing2 ??
                  (openDetailing2 = new RelayCommand(obj =>
                  {
                      DetailingWindow2 detailing = new DetailingWindow2(userId, status);
                      detailing.WindowState = window.WindowState;
                      detailing.Top = window.Top;
                      detailing.Left = window.Left;
                      detailing.Height = window.Height;
                      detailing.Width = window.Width;
                      detailing.Show();
                      window.Close();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
