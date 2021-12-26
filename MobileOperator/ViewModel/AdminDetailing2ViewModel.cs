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
    class AdminDetailingWindow2ViewModel : INotifyPropertyChanged
    {
        Window window;
        private Detailing2Model detailing;
        private DateTime from = new DateTime(2021, 01, 01);
        private DateTime till = DateTime.Now;
        private string clientName = "", clientNumber = "";
        public ObservableCollection<RateHistoryModel> Rates { get; set; }
        public ObservableCollection<ServiceHistoryModel> Services { get; set; }

        public AdminDetailingWindow2ViewModel(Window window)
        {
            this.window = window;
            Rates = new ObservableCollection<RateHistoryModel> { };
            Services = new ObservableCollection<ServiceHistoryModel> { };
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
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; OnPropertyChanged("ClientName"); }
        }
        public string ClientNumber
        {
            get { return clientNumber; }
            set { clientNumber = value; OnPropertyChanged("ClientNumber"); }
        }


        private RelayCommand getDetailingCommand;
        public RelayCommand GetDetailingCommand
        {
            get
            {
                return getDetailingCommand ??
                  (getDetailingCommand = new RelayCommand(obj =>
                  {
                      if (Rates != null)
                          Rates.Clear();
                      if (Services != null)
                          Services.Clear();
                      detailing = new Detailing2Model(clientName, clientNumber, from, till);
                      foreach (RateHistoryModel rate in detailing.AllRates)
                          Rates.Add(rate);
                      foreach (ServiceHistoryModel service in detailing.AllServices)
                          Services.Add(service);
                  }));
            }
        }

        private RelayCommand openAdminMainWindow;
        public RelayCommand OpenAdminMainWindow
        {
            get
            {
                return openAdminMainWindow ??
                  (openAdminMainWindow = new RelayCommand(obj =>
                  {
                      AdminMainWindow main = new AdminMainWindow();
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
        private RelayCommand openAdminRatesWindow;
        public RelayCommand OpenAdminRatesWindow
        {
            get
            {
                return openAdminRatesWindow ??
                  (openAdminRatesWindow = new RelayCommand(obj =>
                  {
                      AdminRatesWindow rates = new AdminRatesWindow();
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
        private RelayCommand openAdminServicesWindow;
        public RelayCommand OpenAdminServicesWindow
        {
            get
            {
                return openAdminServicesWindow ??
                  (openAdminServicesWindow = new RelayCommand(obj =>
                  {
                      AdminServicesWindow services = new AdminServicesWindow();
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
                          workSheet.Columns.ColumnWidth = 25;
                          workSheet.Cells[1, 1] = "Наименование тарифа";
                          workSheet.Cells[1, 2] = "Дата подключения тарифа";
                          workSheet.Cells[1, 3] = "Дата отключения тарифа";
                          workSheet.Cells[1, 4] = " ";
                          workSheet.Cells[1, 5] = "Наименование услуги";
                          workSheet.Cells[1, 6] = "Дата подключения услуги";
                          workSheet.Cells[1, 7] = "Дата отключения услуги";

                          for (int i = 0; i < Rates.Count; i++)
                          {
                              workSheet.Cells[i + 2, 1] = (Rates[i].RateName).ToString();
                              workSheet.Cells[i + 2, 2] = (Rates[i].FromDateToString).ToString();
                              workSheet.Cells[i + 2, 3] = (Rates[i].TillDateToString).ToString();
                          }
                          for (int i = 0; i < Services.Count; i++)
                          {
                              workSheet.Cells[i + 2, 5] = (Services[i].ServiceName).ToString();
                              workSheet.Cells[i + 2, 6] = (Services[i].FromDateToString).ToString();
                              workSheet.Cells[i + 2, 7] = (Services[i].TillDateToString).ToString();
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
                          int count;
                          if (Services.Count > Rates.Count)
                              count = Services.Count;
                          else count = Rates.Count;
                          document.Tables.Add(range, count, 7, ref behiavor, ref autoFitBehiavor);
                          document.Tables[1].Cell(1, 1).Range.Text = "Наименование тарифа";
                          document.Tables[1].Cell(1, 2).Range.Text = "Дата подключения тарифа";
                          document.Tables[1].Cell(1, 3).Range.Text = "Дата отключения тарифа";
                          document.Tables[1].Cell(1, 4).Range.Text = " ";
                          document.Tables[1].Cell(1, 5).Range.Text = "Наименование услуги";
                          document.Tables[1].Cell(1, 6).Range.Text = "Дата подключения услуги";
                          document.Tables[1].Cell(1, 7).Range.Text = "Дата отключения услуги";

                          for (int i = 0; i < Rates.Count; i++)
                          {
                              document.Tables[1].Cell(i + 2, 1).Range.Text = (Rates[i].RateName).ToString();
                              document.Tables[1].Cell(i + 2, 2).Range.Text = (Rates[i].FromDateToString).ToString();
                              document.Tables[1].Cell(i + 2, 3).Range.Text = (Rates[i].TillDateToString).ToString();
                          }
                          for (int i = 0; i < Services.Count; i++)
                          {
                              document.Tables[1].Cell(i + 2, 5).Range.Text = (Services[i].ServiceName).ToString();
                              document.Tables[1].Cell(i + 2, 6).Range.Text = (Services[i].FromDateToString).ToString();
                              document.Tables[1].Cell(i + 2, 7).Range.Text = (Services[i].TillDateToString).ToString();
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
