using System.Windows;

namespace View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region public members

        /// <summary>
        /// Konstruktor des Haupt-Fensters.
        /// </summary>
        public MainWindow()
        {
            /* Der nachfolgende Lock wurde erforderlich, da ansonsten (bei sehr großen Jobs mit sehr vielen Controls) folgender Fehler auftreten kann:
             * Ietztes Ergebnis: Node334: System.NullReferenceException: Der Objektverweis wurde nicht auf eine Objektinstanz festgelegt.
             * bei System.lO.Packaging.PackagePart.CleanUpRequestedStreamsList()
             * bei System.IO.Packaging.PackagePart.GetStream(FileMode mode, FileAccess access)
             * bei System.Windows.Application.LoadComponent(Object component, Uri resourceLocator)
             * bei WPFDialogChecker.View.MainWindow.lnitializeComponent()
             * bei WPFDialogChecker.View.MainWindow..ctor()
             * bei WPFDialogChecker.WPFDialogChecker.Run(Object checkerParameters, TreeParameters treeParameters, TreeEvent source)
             * bei LogicalTaskTree.CheckerShell.runlt(Object checkerParameters, TreeParameters treeParameters, TreeEvent source) in C:\Users\micro\Documents\private4\WPF\Vishnu_Root\VishnuHome\Vishnu\LogicalTaskTree\CheckerShell.cs:Zeile 444.
             * bei LogicalTaskTree.CheckerShell.Run(Object checkerParameters, TreeParameters treeParameters, TreeEvent source) in C:\Users\micro\Documents\private4\WPF\Vishnu_Root\VishnuHome\Vishnu\LogicalTaskTree\CheckerShell.cs:Zeile 74.
             * bei LogicalTaskTree.SingleNode.DoRun(TreeEvent source) in C:\Users\micro\Documents\private4\WPF\Vishnu_Root\VishnuHome\Vishnu\LogicalTaskTree\SingleNode.cs:Zeile 538.
             * Weiterer Hinweis: LockHelper muss zwingend eine Klasse dieser Assembly sein, Auslagerung in NetEti.Globals führt bei großen Jobs zu Ladefehlern!
            */
            lock (LockHelper.Instance)
                InitializeComponent();
        }

        #endregion public members

        #region private members

        #endregion private members

    }
}
