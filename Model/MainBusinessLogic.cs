using System;
using NetEti.ApplicationControl;
using Model;

namespace WPFDateDialog.Model
{
    /// <summary>
    /// Verarbeitungszustände einer Applikation.
    /// </summary>
    [Flags]
    public enum State
    {
        /// <summary>Startbereit, Zustand nach Initialisierung.</summary>
        None = 1,
        /// <summary>Beschäftigt, wartet auf Starterlaubnis.</summary>
        Waiting = 2,
        /// <summary>Beschäftigt, arbeitet.</summary>
        Working = 4,
        /// <summary>Startbereit, ist beendet.</summary>
        Done = 8,
        /// <summary>Startbereit, ist abgebrochen.</summary>
        Breaked = 16,
        /// <summary>Kann gestartet werden (None, Done oder Breaked).</summary>
        CanStart = None | Done | Breaked,
        /// <summary>Nicht startbereit, wartet oder arbeitet gerade (Waiting oder Working).</summary>
        Busy = Waiting | Working
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich der Verarbeitungszustand eines Knotens geändert hat.
    /// </summary>
    /// <param name="sender">Die Ereignis-Quelle.</param>
    /// <param name="state">None, Waiting, Working, Finished, Busy (= Waiting | Working) oder CanStart (= None|Finished).</param>
    public delegate void StateChangedEventHandler(MainBusinessLogic sender, State state);

    /// <summary>
    /// Der Haupt-Einstiegspunkt für die Geschäftslogik.
    /// </summary>
    public class MainBusinessLogic
    {
        /// <summary>
        /// Übernimmt minimale Steuerungsmöglichkeiten für die konkret
        /// geladene UI vom ViewModel. Wird vom ViewModel gesetzt.
        /// </summary>
        public IMinimumDialogServer DialogServer { get; set; }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Verarbeitungszustand eines Knotens geändert hat.
        /// </summary>
        public event StateChangedEventHandler StateChanged;

        /// <summary>
        /// Id des aufrufenden Knoten aus Vishnu.
        /// Wird vom Checker gesetzt und vom ViewModel übernommen.
        /// </summary>
        public string CallingNodeId { get; set; }

        /// <summary>
        /// Das Ergebnis (DateTime?) im ReturnObject.
        /// </summary>
        public DateTime? DateAndTime { get; set; }

        /// <summary>
        /// Das DialogResult der Verarbeitung
        /// </summary>
        public bool DialogResult { get; set; }

        /// <summary>
        /// Der aktuelle Verarbeitungszustand der MainBusinessLogic. 
        /// </summary>
        public State ModelState
        {
            get
            {
                return this._modelState;
            }
            set
            {
                if (this._modelState != value)
                {
                    this._modelState = value;
                    this.OnStateChanged();
                }
            }
        }

        /// <summary>
        /// Standard Konstruktor.
        /// </summary>
        public MainBusinessLogic(string callingNodeId, DateTime? dateAndTime)
        {
            this.ModelState = State.None;
            this.CallingNodeId = callingNodeId;
            this.DateAndTime = dateAndTime;
        }

        /// <summary>
        /// Abbrechen der Task.
        /// </summary>
        public void HandleCmdBreak()
        {
            this.ModelState = State.Breaked;
            this.DialogResult = false;
            this.DialogServer.WaitAndClose(500, false); // Das Window ist schon geschlossen!
        }

        /// <summary>
        /// True, wenn das Abbrechen der Task möglich ist.
        /// </summary>
        public bool CanHandleCmdBreak()
        {
            return true;
        }

        /// <summary>
        /// Übernimmt die neuen Werte und beendet die MainBusinessLogic.
        /// </summary>
        public void HandleCmdOk()
        {
            this.ModelState = State.Done;
            this.DialogResult = true;
            this.DialogServer.WaitAndClose(500, false);
        }

        /// <summary>
        /// True, wenn das Abschließen der Task
        /// mit Übernahme der neuen Werte möglich ist.
        /// </summary>
        public bool CanHandleCmdOk()
        {
            return true;
        }

        /// <summary>
        /// Übernimmt Null als neuen Wert und beendet die MainBusinessLogic.
        /// </summary>
        public void HandleCmdNull()
        {
            this.DateAndTime = null;
            this.DialogResult = true;
            this.ModelState = State.Done;
            this.DialogServer.WaitAndClose(500, false);
        }

        /// <summary>
        /// True, wenn das Abschließen der Task
        /// mit Übernahme der neuen Werte möglich ist.
        /// </summary>
        public bool CanHandleCmdNull()
        {
            return true;
        }

        /// <summary>
        /// Löst das NodeStateChanged-Ereignis aus.
        /// </summary>
        internal virtual void OnStateChanged()
        {
            if (StateChanged != null)
            {
                Statistics.Inc("LogicalNode.State.Get from LogicalNote.OnStateChanged");
                StateChanged(this, this.ModelState);
            }
        }

        private State _modelState;

    }
}
