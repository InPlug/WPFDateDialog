using NetEti.MVVMini;

namespace ViewModel
{
    /// <summary>
    /// ViewModel für das MainWindow.
    /// </summary>
    /// <remarks>
    /// File: MainWindowViewModel
    /// Autor: Erik Nagel
    ///
    /// 09.01.2014 Erik Nagel: erstellt
    /// </remarks>
    public class MainWindowViewModel : ObservableObject
    {
        #region public members

        /// <summary>
        /// ViewModel für den LogicalTaskTree.
        /// </summary>
        public MainBusinessLogicViewModel MainBusinessLogicViewModel_
        {
            get
            {
                return this._mainBusinessLogicViewModel_;
            }
            set
            {
                if (this._mainBusinessLogicViewModel_ != value)
                {
                    this._mainBusinessLogicViewModel_ = value;
                    this.RaisePropertyChanged("MainBusinessLogicViewModel_");
                }
            }
        }


        /// <summary>
        /// Konstruktor - übernimmt das ViewModel für den LogicalTaskTree und eine Methode des
        /// MainWindows zum Restaurieren der Fenstergröße abhängig vom Fensterinhalt.
        /// </summary>
        /// <param name="mainBusinessLogicViewModel">ViewModel für den LogicalTaskTree.</param>
        public MainWindowViewModel(MainBusinessLogicViewModel mainBusinessLogicViewModel)
        {
            this._mainBusinessLogicViewModel_ = mainBusinessLogicViewModel;
        }

        #endregion public members

        #region private members

        private MainBusinessLogicViewModel _mainBusinessLogicViewModel_;

        #endregion private members

    }
}
