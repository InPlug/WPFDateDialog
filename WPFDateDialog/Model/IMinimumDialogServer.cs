namespace Model
{
    /// <summary>
    /// Stellt der Business-Logic minimale Steuerungsmöglichkeiten zur
    /// Steuerung der konkret geladenen UI über das ViewModel zur Verfügung.
    /// </summary>
    public interface IMinimumDialogServer
    {
        /// <summary>
        /// Schließt die Ui mit einer Verzögerung von millisecondsDelay.
        /// </summary>
        /// <param name="millisecondsDelay">Verzögerung in Millisekunden vor Schließen der Ui.</param>
        /// <param name="dialogResult">DialogResult (True oder False).</param>
        void WaitAndClose(int millisecondsDelay, bool dialogResult);
    }
}
