using NetEti.ApplicationEnvironment;

namespace WPFDateDialog
{
    /// <summary>
    /// Holt Applikationseinstellungen aus verschiedenen Quellen:
    /// Kommandozeile, app.config, Environment, Registry und (zur Demo) aus einer test.ini.
    /// - anwendungsspezifisch -
    /// Erbt allgemeingültige Einstellungen von BasicAppSettings oder davon abgeleiteten
    /// Klassen und fügt Anwendungsspezifische Properties hinzu.<br></br>
    /// <seealso cref="BasicAppSettings"/>
    /// </summary>
    /// <remarks>
    /// File: AppSettings.cs<br></br>
    /// Autor: Erik Nagel, NetEti<br></br>
    ///<br></br>
    /// 11.10.2013 Erik Nagel: erstellt<br></br>
    /// </remarks>
    public sealed class AppSettings : BasicAppSettings
    {
        #region public members

        #region Properties (alphabetic)

        #endregion Properties (alphabetic)

        #endregion public members

        #region private members

        /// <summary>
        /// Private Konstruktor, wird ggf. über Reflection vom externen statischen
        /// GenericSingletonProvider über GetInstance() aufgerufen.
        /// Holt alle Infos und stellt sie als Properties zur Verfügung.
        /// </summary>
        private AppSettings()
          : base()
        {
            this.WorkingDirectoryCreated = false;
        }

        #endregion private members

    } // public sealed class AppSettings: BasicAppSettings
}
