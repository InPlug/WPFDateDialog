using System;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using NetEti.MVVMini;
using WPFDateDialog.Model;
using System.Threading;
using System.Threading.Tasks;
using Model;

namespace WPFDateDialog.ViewModel
{
    /// <summary>
    /// ViewModel für die TreeView in LogicalTaskTreeControl im ersten Tab des MainWindow.
    /// </summary>
    /// <remarks>
    /// File: LogicalTaskTreeViewModel
    /// Autor: Erik Nagel
    ///
    /// 05.01.2013 Erik Nagel: erstellt
    /// </remarks>
    public class MainBusinessLogicViewModel : ObservableObject, IMinimumDialogServer
    {
        #region public members

        #region IMinimumDialogServer Implementation

        /// <summary>
        /// Schließt die Ui mit einer Verzögerung von millisecondsDelay.
        /// </summary>
        /// <param name="millisecondsDelay">Verzögerung in Millisekunden vor Schließen der Ui.</param>
        /// <param name="dialogResult">DialogResult (True oder False).</param>
        public void WaitAndClose(int millisecondsDelay, bool dialogResult)
        {
            this._canHandleCmdBreak = false;
            if (this._uIMain != null && this._uIMain is Window && !(this._uIMain as Window).DialogResult.HasValue)
            {
                CommandManager.InvalidateRequerySuggested();
                new TaskFactory().StartNew(new Action(() =>
                {
                    Thread.Sleep(millisecondsDelay);
                    if (this.Dispatcher.CheckAccess())
                        (this._uIMain as Window).DialogResult = true;
                    else
                        this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(new Action(() => { (this._uIMain as Window).DialogResult = dialogResult; })));
                }));
            }
        }

        #endregion IMinimumDialogServer Implementation

        #region published members

        /// <summary>
        /// Id des Aufrufenden Knotens im LogicalTaskTree plus Start-Frage.
        /// </summary>
        public string WindowTitle
        {
            get
            {
                return this.CallingNodeId + " - " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            set
            {
                this.RaisePropertyChanged("WindowTitle");
            }
        }

        /// <summary>
        /// Id des Aufrufenden Knotens im LogicalTaskTree.
        /// </summary>
        public string CallingNodeId
        {
            get
            {
                return this._root.CallingNodeId;
            }
            set
            {
                this.RaisePropertyChanged("CallingNodeId");
            }
        }

        /// <summary>
        /// True, false oder null..
        /// </summary>
        public DateTime? DateAndTime
        {
            get
            {
                return this._root.DateAndTime;
            }
            set
            {
                if (this._root.DateAndTime != value)
                {
                    this._root.DateAndTime = value;
                    this.RaisePropertyChanged("DateAndTime");
                }
            }
        }

        /// <summary>
        /// Command für den CmdTrue-Button in der MainBusinessLogic.
        /// </summary>
        public ICommand CmdOk { get { return this._cmdOkMainBusinessLogicRelayCommand; } }

        /// <summary>
        /// Command für den CmdTrue-Button in der MainBusinessLogic.
        /// </summary>
        public ICommand CmdNull { get { return this._cmdNullMainBusinessLogicRelayCommand; } }

        /// <summary>
        /// Command für den Break-Button im der MainBusinessLogic.
        /// </summary>
        public ICommand CmdBreak { get { return this._cmdBreakMainBusinessLogicRelayCommand; } }

        #endregion published members

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainBusinessLogicViewModel(MainBusinessLogic root, FrameworkElement uiMain)
        {
            this._root = root;
            this._uIMain = uiMain;
            this._root.DialogServer = this;
            this._canHandleCmdBreak = true;
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                this._uIDispatcher = this._uIMain.Dispatcher;
            }
            this._cmdOkMainBusinessLogicRelayCommand = new RelayCommand(cmdOkMainBusinessLogicExecute, canCmdOkMainBusinessLogicExecute);
            this._cmdNullMainBusinessLogicRelayCommand = new RelayCommand(cmdNullMainBusinessLogicExecute, canCmdNullMainBusinessLogicExecute);
            this._cmdBreakMainBusinessLogicRelayCommand = new RelayCommand(cmdBreakMainBusinessLogicExecute, canCmdBreakMainBusinessLogicExecute);

            this._root.StateChanged -= this.mainBusinessLogicStateChanged;
            this._root.StateChanged += this.mainBusinessLogicStateChanged;

        }

        #endregion public members

        #region private members

        private MainBusinessLogic _root;
        private RelayCommand _cmdOkMainBusinessLogicRelayCommand;
        private RelayCommand _cmdNullMainBusinessLogicRelayCommand;
        private RelayCommand _cmdBreakMainBusinessLogicRelayCommand;
        private FrameworkElement _uIMain { get; set; }
        private bool _canHandleCmdBreak;
        private System.Windows.Threading.Dispatcher _uIDispatcher { get; set; }

        private MainBusinessLogicViewModel() { }

        private void mainBusinessLogicStateChanged(object sender, State state)
        {
            this.RaisePropertyChanged("DateAndTime");
            this.RaisePropertyChanged("CallingNodeId");
            // Die Buttons müssen zum Update gezwungen werden, da die Verarbeitung in einem
            // anderen Thread läuft:
            this._cmdOkMainBusinessLogicRelayCommand.UpdateCanExecuteState(this.Dispatcher);
            this._cmdNullMainBusinessLogicRelayCommand.UpdateCanExecuteState(this.Dispatcher);
            this._cmdBreakMainBusinessLogicRelayCommand.UpdateCanExecuteState(this.Dispatcher);
        }

        private void cmdOkMainBusinessLogicExecute(object parameter)
        {
            this._root.HandleCmdOk();
        }

        private bool canCmdOkMainBusinessLogicExecute()
        {
            return this._root.CanHandleCmdOk();
        }

        private void cmdNullMainBusinessLogicExecute(object parameter)
        {
            this._root.HandleCmdNull();
        }

        private bool canCmdNullMainBusinessLogicExecute()
        {
            return this._root.CanHandleCmdNull();
        }

        private void cmdBreakMainBusinessLogicExecute(object parameter)
        {
            this._root.HandleCmdBreak();
        }

        private bool canCmdBreakMainBusinessLogicExecute()
        {
            return this._canHandleCmdBreak && this._root.CanHandleCmdBreak();
        }

        #endregion private members

    }
}
