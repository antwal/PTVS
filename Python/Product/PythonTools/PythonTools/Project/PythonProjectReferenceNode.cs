// Python Tools for Visual Studio
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    class PythonProjectReferenceNode : ProjectReferenceNode {
        //private readonly FileChangeManager _fileChangeListener;
        //private string _observing;
        //private AssemblyName _asmName;
        //private bool _failedToAnalyze;
        //private ProjectReference _curReference;

        public PythonProjectReferenceNode(ProjectNode root, ProjectElement element)
            : base(root, element) {
            //_fileChangeListener = new FileChangeManager(ProjectMgr.Site);
            Initialize();
        }

        public PythonProjectReferenceNode(ProjectNode project, string referencedProjectName, string projectPath, string projectReference)
            : base(project, referencedProjectName, projectPath, projectReference) {
            //_fileChangeListener = new FileChangeManager(ProjectMgr.Site);
            Initialize();
        }

        private void Initialize() {
            var solutionEvents = ProjectMgr.Site.GetSolutionEvents();
            solutionEvents.ActiveSolutionConfigurationChanged += EventListener_AfterActiveSolutionConfigurationChange;
            solutionEvents.BuildCompleted += EventListener_BuildCompleted;
            //_fileChangeListener.FileChangedOnDisk += FileChangedOnDisk;
            solutionEvents.ProjectLoaded += PythonProjectReferenceNode_ProjectLoaded;
            //InitializeFileChangeListener();
            //AddAnalyzedAssembly(((PythonProjectNode)ProjectMgr).GetAnalyzer()).DoNotWait();
        }

        private void EventListener_BuildCompleted(object sender, EventArgs e) {
            if (ProjectMgr.IsClosing) {
                return;
            }
            //InitializeFileChangeListener();
            //AddAnalyzedAssembly(((PythonProjectNode)ProjectMgr).GetAnalyzer()).DoNotWait();
            ProjectMgr.OnInvalidateItems(Parent);
        }

        private void PythonProjectReferenceNode_ProjectLoaded(object sender, ProjectEventArgs e) {
            if (ProjectMgr.IsClosing) {
                return;
            }
            //InitializeFileChangeListener();
            //AddAnalyzedAssembly(((PythonProjectNode)ProjectMgr).GetAnalyzer()).DoNotWait();
            ProjectMgr.OnInvalidateItems(Parent);
        }

        private void EventListener_AfterActiveSolutionConfigurationChange(object sender, EventArgs e) {
            if (ProjectMgr.IsClosing) {
                return;
            }
            //InitializeFileChangeListener();
            //AddAnalyzedAssembly(((PythonProjectNode)ProjectMgr).GetAnalyzer()).DoNotWait();
            ProjectMgr.OnInvalidateItems(Parent);
        }

        //private async void InitializeFileChangeListener() {
        //    if (_observing != null) {
        //        _fileChangeListener.StopObservingItem(_observing);
        //    }
        //
        //    await Task.Delay(500).ConfigureAwait(true);
        //
        //    _observing = ReferencedProjectOutputPath;
        //    if (_observing != null) {
        //        _fileChangeListener.ObserveItem(_observing);
        //    }
        //}

        //private void FileChangedOnDisk(object sender, FileChangedOnDiskEventArgs e) {
        //    if (ProjectMgr.IsClosing) {
        //        return;
        //    }
        //    var interp = ((PythonProjectNode)ProjectMgr).GetAnalyzer();
        //    // remove the reference to whatever we are currently observing
        //    RemoveAnalyzedAssembly(interp);
        //
        //    if ((e.FileChangeFlag & (VisualStudio.Shell.Interop._VSFILECHANGEFLAGS.VSFILECHG_Add | VisualStudio.Shell.Interop._VSFILECHANGEFLAGS.VSFILECHG_Time | VisualStudio.Shell.Interop._VSFILECHANGEFLAGS.VSFILECHG_Size)) != 0) {
        //        // kick off the analysis of the new assembly...
        //        AddAnalyzedAssembly(interp).DoNotWait();
        //    }
        //}

        //private void RemoveAnalyzedAssembly(VsProjectAnalyzer interp) {
        //    if (interp != null) {
        //        if (_curReference != null) {
        //            interp.RemoveReferenceAsync(_curReference).Wait();
        //            _curReference = null;
        //        }
        //    }
        //}

        //internal async Task AddAnalyzedAssembly(VsProjectAnalyzer interp) {
        //    if (interp != null) {
        //        string asmName;
        //        string outFile;
        //        try {
        //            asmName = AssemblyName;
        //            outFile = ReferencedProjectOutputPath;
        //        } catch (COMException) {
        //            _failedToAnalyze = true;
        //            return;
        //        }
        //        _failedToAnalyze = false;
        //        _curReference = null;
        //    
        //        if (!string.IsNullOrEmpty(asmName)) {
        //            _asmName = new AssemblyName(asmName);
        //            _curReference = new ProjectAssemblyReference(_asmName, ReferencedProjectOutputPath);
        //        } else if (File.Exists(outFile)) {
        //            _asmName = null;
        //            _curReference = new ProjectReference(outFile, ProjectReferenceKind.ExtensionModule);
        //        } else {
        //            if (ReferencedProjectObject == null ||
        //                !Utilities.GuidEquals(PythonConstants.ProjectFactoryGuid, ReferencedProjectObject.Kind)) {
        //                // Only failed if the reference isn't to another Python
        //                // project.
        //                _failedToAnalyze = true;
        //            }
        //        }
        //    
        //        if (_curReference != null) {
        //            try {
        //                await interp.AddReferenceAsync(_curReference);
        //            } catch (Exception) {
        //                _failedToAnalyze = true;
        //            }
        //        }
        //    }
        //}

        //protected override bool CanShowDefaultIcon() {
        //    if (_failedToAnalyze) {
        //        return false;
        //    }
        //
        //    return base.CanShowDefaultIcon();
        //}

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing) {
                var solutionEvents = ProjectMgr.Site.GetSolutionEvents();
                solutionEvents.ActiveSolutionConfigurationChanged -= EventListener_AfterActiveSolutionConfigurationChange;
                solutionEvents.BuildCompleted -= EventListener_BuildCompleted;
                solutionEvents.ProjectLoaded -= PythonProjectReferenceNode_ProjectLoaded;

                //_fileChangeListener.FileChangedOnDisk -= FileChangedOnDisk;
                //_fileChangeListener.Dispose();
            }
        }
    }
}
