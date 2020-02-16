﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using NotepadCore.Exceptions;

namespace NotepadCore
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string documentPath = "contents.txt";
        private int newFileNumber = 1;

        public MainWindow()
        {
            InitializeComponent();

            var userSettings = Settings.Create();

            // if there are files, load them
            if (userSettings.FilePaths.Length != 0)
            {
                foreach (var i in userSettings.FilePaths)
                    try
                    {
                        Tabs.Items.Insert(Tabs.Items.Count - 1, new TabItem
                        {
                            Content = new TextEditor(i),
                            Header = new FileInfo(i).Name
                        });
                    }
                    catch
                    {
                    }

                // Select the tab that was previously selected
                Tabs.SelectedIndex = userSettings.SelectedFileIndex;
            }
            // else insert an empty tab
            else
            {
                Tabs.Items.Insert(Tabs.Items.Count - 1, new TabItem
                {
                    Content = new TextEditor(),
                    Header = $"*new file {newFileNumber++}"
                });
                Tabs.SelectedIndex = 0;
            }

            // Changes the font according to settings
            ChangeFont();
        }

        private TextEditor CurrentTextEditor => Tabs.SelectedContent as TextEditor;

        /// <summary>
        ///     Writes the text from MainTextBox when the window closes
        /// </summary>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var userSettings = Settings.Create();

            // loop through Text Editors and save contents
            for (var i = 0; i < Tabs.Items.Count - 1; i++)
                try
                {
                    // saves the text from the TextEditor
                    ((Tabs.Items[i] as TabItem).Content as TextEditor).SaveFile();
                }
                catch (InvalidSaveLocationException ex)
                {
                    // select the tab without a save location and ask for a save location
                    if (!string.IsNullOrEmpty(((Tabs.Items[i] as TabItem).Content as TextEditor).Text))
                    {
                        Tabs.SelectedIndex = i;
                        FileSave_Click(null, null);
                    }
                }

            // Save the index of a currently selected tab
            userSettings.SelectedFileIndex = Tabs.SelectedIndex;
            userSettings.Save();

            Application.Current.Shutdown();
        }

        /// <summary>
        ///     Creates and opens the file
        /// </summary>
        private void FileNew_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var newDialog = new SaveFileDialog();
            newDialog.ShowDialog();
            documentPath = newDialog.FileName;

            if (!string.IsNullOrEmpty(documentPath))
            {
                // Adds new file path
                userSettings.AddFiles(documentPath);

                Tabs.Items.Insert(Tabs.Items.Count - 1, new TabItem
                {
                    Content = new TextEditor(documentPath),
                    Header = new FileInfo(documentPath).Name
                });
            }

            userSettings.Save();
        }

        /// <summary>
        ///     Opens an existing file
        /// </summary>
        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var openDialog = new OpenFileDialog();
            openDialog.ShowDialog();
            documentPath = openDialog.FileName;

            if (!string.IsNullOrEmpty(documentPath))
                // writes the new file path to the constant Path.Document location
                userSettings.AddFiles(documentPath);

            if (string.IsNullOrEmpty(CurrentTextEditor.Text))
            {
                // if the selected TextEditor is empty, open a new one on the same spot
                var item = Tabs.SelectedItem as TabItem;
                item.Content = new TextEditor(documentPath);
                item.Header = new FileInfo(documentPath).Name;
            }
            else
            {
                // else insert a new TextEditor on the end
                Tabs.Items.Insert(Tabs.Items.Count - 1, new TabItem
                {
                    Content = new TextEditor(documentPath),
                    Header = new FileInfo(documentPath).Name
                });

                Tabs.SelectedIndex = Tabs.Items.Count - 2;
            }

            userSettings.Save();
        }

        private void FileClose_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var files = userSettings.FilePaths.ToList();
            if (CurrentTextEditor.HasSaveLocation)
                CurrentTextEditor.SaveFile();


            if (Tabs.SelectedIndex == 0)
            {
                Tabs.Items.RemoveAt(0);
                files.RemoveAt(0);
            }
            else
            {
                Tabs.SelectedIndex--; // 
                Tabs.Items.RemoveAt(Tabs.SelectedIndex + 1);
                files.RemoveAt(Tabs.SelectedIndex + 1);
            }

            userSettings.FilePaths = files.ToArray();
            userSettings.Save();
        }

        /// <summary>
        ///     Saves the current file
        /// </summary>
        private void FileSave_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var textEdit = Tabs.SelectedContent as TextEditor;
            if (textEdit.HasSaveLocation)
            {
                textEdit.SaveFile();
            }
            else
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.ShowDialog();

                if (string.IsNullOrEmpty(saveDialog.FileName))
                    return;
                textEdit.DocumentPath = saveDialog.FileName; // sets the document path to that one in save file dialog
                var paths = userSettings.FilePaths.ToList();
                paths.Insert(Tabs.SelectedIndex, textEdit.DocumentPath);
                userSettings.FilePaths = paths.ToArray();
                (Tabs.Items[Tabs.SelectedIndex] as TabItem).Header = textEdit.FileName;

                userSettings.Save();
            }
        }

        /// <summary>
        ///     Saves the content of the current file to a new file
        /// </summary>
        private void FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var textEditor = Tabs.SelectedContent as TextEditor;
            if (textEditor.HasSaveLocation) textEditor.SaveFile();

            var textEditorText = textEditor.Text;

            var saveDialog = new SaveFileDialog();
            saveDialog.ShowDialog();
            textEditor.DocumentPath = saveDialog.FileName;
            textEditor.Text = textEditorText;

            // change file paths
            var files = userSettings.FilePaths.ToList();
            files.RemoveAt(Tabs.SelectedIndex);
            userSettings.FilePaths = files.ToArray();

            userSettings.Save();

            (Tabs.SelectedItem as TabItem).Header = textEditor.FileName;
        }


        /// <summary>
        ///     Opens the ChangeFontDialog
        /// </summary>
        private void ChangeFontDialog_Click(object sender, RoutedEventArgs e)
        {
            var userSettings = Settings.Create();

            var fontDialog = new FontWindow();
            fontDialog.ShowDialog();

            userSettings.EditorFontFamily = fontDialog.fontFamily;
            userSettings.EditorFontSize = fontDialog.fontSize;
            userSettings.Save();

            ChangeFont();
        }


        /// <summary>
        ///     Changes the font of the two textboxes according to passed values
        /// </summary>
        public void ChangeFont()
        {
            var userSettings = Settings.Create();

            foreach (var textEdit in GetTextEditors())
            {
                //change font of main textbox and line textbox
                textEdit.MainTextBox.FontFamily = new FontFamily(userSettings.EditorFontFamily);
                textEdit.MainTextBox.FontSize = userSettings.EditorFontSize;
                textEdit.LineTextBox.FontFamily = new FontFamily(userSettings.EditorFontFamily);
                textEdit.LineTextBox.FontSize = userSettings.EditorFontSize;
            }
        }

        /// <summary>
        ///     Opens the find/replace dialog. Work in progress
        /// </summary>
        private void FindReplace_Click(object sender, RoutedEventArgs e)
        {
            // TODO: add tab support
            new Find().Show();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            //var textEdit = Tabs.SelectedContent as TextEditor;
            //Clipboard.SetDataObject(textEdit.MainTextBox.SelectedText); // copies the text to clipboard
            //textEdit.MainTextBox.Text = textEdit.MainTextBox.Text.Remove(textEdit.MainTextBox.SelectionStart, textEdit.MainTextBox.SelectionLength); // deletes the text

            var textSelection = CurrentTextEditor.MainTextBox.Selection;
            Clipboard.SetDataObject(textSelection.Text);
            textSelection.ClearAllProperties();
            textSelection.Text = "";
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            //var textEdit = Tabs.SelectedContent as TextEditor;
            //Clipboard.SetDataObject(textEdit.MainTextBox.SelectedText); // copies the text to clipboard
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            //var textEdit = Tabs.SelectedContent as TextEditor;
            //int caretIndex = textEdit.MainTextBox.CaretIndex;
            //textEdit.MainTextBox.Text = textEdit.MainTextBox.Text.Insert(textEdit.MainTextBox.CaretIndex, Clipboard.GetText()); // puts the text from the clipboard
            //textEdit.MainTextBox.CaretIndex = caretIndex + Clipboard.GetText().Length;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // show SettingsWindow
            var settingsWindow = new SettingsWindow();

            settingsWindow.ShowDialog();
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if + tab is selected add a new tab
            if (Tabs.SelectedItem == TabAdd)
            {
                Tabs.Items.Insert(Tabs.Items.Count - 1, new TabItem
                {
                    Content = new TextEditor(),
                    Header = $"*new file {newFileNumber++}"
                });
                Tabs.SelectedIndex--;
            }
        }

        public List<TextEditor> GetTextEditors()
        {
            return (from object i in Tabs.Items
                where (i as TabItem).Content is TextEditor
                select (i as TabItem).Content as TextEditor).ToList();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Tabs.Focus();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            (Tabs.SelectedContent as TextEditor).MainTextBox.Focus();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
            // Icon made by https://www.flaticon.com/authors/smashicons from www.flaticon.com
        }
    }
}