using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Ck2.Trainer.Processors;

namespace Ck2.Trainer
{
    public class ProcessorsHandler
    {
        private FrmMain _frmMain;

        public ProcessorsHandler(FrmMain frmMain)
        {
            _frmMain = frmMain;
            LoadedProcessors = new Dictionary<string, Type>();
        }

        public Dictionary<string, Type> LoadedProcessors { get; }


        public Type SelectedProcessor
        {
            get
            {
                Type selected;
                if (_frmMain.lbFileProperties != null
                    && LoadedProcessors.TryGetValue(_frmMain.lbProcessors.SelectedItem as string, out selected))
                {
                    return selected;
                }

                throw new InvalidOperationException();
            }
        }

        internal void PopulateProcessorsList()
        {
            var processors = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(ICk2Processor)));

            foreach (var p in processors)
            {
                LoadedProcessors.Add(p.Name, p);
                _frmMain.lbProcessors.Items.Add(p.Name);
            }

            _frmMain.lbProcessors.Refresh();
        }

        internal void ApplySelectedProcessor()
        {
            ((ICk2Processor)Activator.CreateInstance((Type) SelectedProcessor))
                .ApplyToNode(_frmMain.FilesHandler.LoadedSaveFile.RootBlock);
        }

        internal bool ConfirmApplySelectedProcessor()
        {
            var answer = MessageBox.Show(_frmMain,
                $"Confirm applying {SelectedProcessor.Name} processor ?", "Confirm",
                MessageBoxButtons.OKCancel);

            return answer == DialogResult.OK;
        }
    }
}