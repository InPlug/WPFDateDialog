using System;
using System.ComponentModel;
using System.Windows;
using Vishnu.Interchange;

namespace WPFDateDialog
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WPFDateDialog wpfDateDialog = new WPFDateDialog();
            wpfDateDialog.NodeProgressChanged += SubNodeProgressChanged;
            wpfDateDialog.Run("xyz", new TreeParameters("MainTree", null), new TreeEvent("DemoTreeEvent", "Demo-Knoten", "Demo-Knoten", "Demo-Knoten",
              @"WPFDateDialog\Demo-Knoten", true, NodeLogicalState.Done,
              new ResultDictionary() { { "Demo-Knoten", new Result("Demo-Knoten", true, NodeState.Finished, NodeLogicalState.Done, DateTime.Now) } },
              new ResultDictionary()));
            MessageBox.Show(String.Format("Result: {0}", ((wpfDateDialog.ReturnObject) ?? "null").ToString()));
        }

        static void SubNodeProgressChanged(object? sender, ProgressChangedEventArgs args)
        {
            Console.WriteLine(args.ProgressPercentage);
        }
    }
}
