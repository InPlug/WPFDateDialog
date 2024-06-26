﻿using System;
using Vishnu.Interchange;
using ViewModel;
using System.Windows;
using System.ComponentModel;

namespace WPFDateDialog
{
    /// <summary>
    /// Vishnu-Checker (WPF-Dialog) für eine Datumseingabe.
    /// </summary>
    public class WPFDateDialog : INodeChecker
    {
        /// <summary>
        /// Wird ausgelöst, wenn sich der Verarbeitungsfortschritt geändert hat.
        /// </summary>
        public event ProgressChangedEventHandler? NodeProgressChanged;

        /// <summary>
        /// Rückgabe-Objekt des Checkers (zusätzlich zum Check-Result (bool?)).
        /// </summary>
        public object? ReturnObject { get; set; }

        /// <summary>
        /// Startet den Checker - wird von einem Knoten im LogicalTaskTree aufgerufen.
        /// Checker liefern grundsätzlich true oder false zurück. Darüber hinaus können
        /// weiter gehende Informationen über das ReturnObject transportiert werden;
        /// Hier wird DateTime.Now über das ReturnObject zurückgegeben.
        /// </summary>
        /// <param name="checkerParameters">Spezifische Aufrufparameter oder null.</param>
        /// <param name="treeParameters">Für den gesamten Tree gültige Parameter oder null.</param>
        /// <param name="source">Auslösendes TreeEvent oder null.</param>
        /// <returns>True, False oder null</returns>
        public bool? Run(object? checkerParameters, TreeParameters treeParameters, TreeEvent source)
        {
            // Parameterübernahme
            string? callingNodeId = null;
            DateTime? dateAndTime = null;
            callingNodeId = source.NodeName;
            if (source.Results != null && source.Results.Count > 0 && source.Results.ContainsKey(callingNodeId))
            {
                Result? lastResult = source.Results[callingNodeId];
                this.ReturnObject = lastResult?.ReturnObject;
                dateAndTime = this.ReturnObject == null ? DateTime.Now : (this.ReturnObject as DateTime?);
            }
            else
            {
                this.ReturnObject = null;
                dateAndTime = DateTime.Now;
            }

            // Die Haupt-Klasse der Geschäftslogik
            this._mainBusinessLogic = new Model.MainBusinessLogic(callingNodeId, dateAndTime);

            // Das Main-Window
            // Point parentViewAbsoluteScreenPosition = treeParameters.GetParentViewAbsoluteScreenPosition();
            Point parentViewAbsoluteScreenPosition = treeParameters.LastParentViewAbsoluteScreenPosition;
            this._mainWindow = new View.MainWindow(parentViewAbsoluteScreenPosition);
            this._mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;

            // Das MainBusinessLogic-ViewModel
            this._mainBusinessLogicViewModel = new MainBusinessLogicViewModel(this._mainBusinessLogic, this._mainWindow);

            // Das Main-ViewModel
            this._mainWindowViewModel = new MainWindowViewModel(this._mainBusinessLogicViewModel);

            // Verbinden von Main-Window mit Main-ViewModel
            this._mainWindow.DataContext = this._mainWindowViewModel;

            this.OnNodeProgressChanged(0);

            this._mainWindow.ShowDialog();
            if (this._mainBusinessLogic.DialogResult)
            {
                this.ReturnObject = this._mainBusinessLogic.DateAndTime;
            }
            else
            {
                //InfoController.Say("User clicked Cancel");
            }
            this.OnNodeProgressChanged(100);
            //bool? rtn = this._mainBusinessLogic.DialogResult;
            bool? rtn = true;

            this._mainWindow = null;
            this._mainWindowViewModel = null;
            this._mainBusinessLogicViewModel = null;
            this._mainBusinessLogic = null;

            return rtn;
        }

        private View.MainWindow? _mainWindow;
        private Model.MainBusinessLogic? _mainBusinessLogic;
        private ViewModel.MainBusinessLogicViewModel? _mainBusinessLogicViewModel;
        private ViewModel.MainWindowViewModel? _mainWindowViewModel;

        private void OnNodeProgressChanged(int progressPercentage)
        {
            if (NodeProgressChanged != null)
            {
                NodeProgressChanged(null, new ProgressChangedEventArgs(progressPercentage, null));
            }
        }
    }
}
