using System;
using System.Diagnostics;

namespace TrainingIS.DAL.Properties
{

    public sealed partial class Settings
    {
        public Settings()
        {
            // Each method corrsponds to a build version. We call all four methods, because
            // the conditional compilation will only compile the one indicated:
            this.SetDebugApplicationSettings();
            this.SetTestApplicationSettings();
            this.SetDeveloppementApplicationSettings();
            this.SetReleaseApplicationSettings();
            this.SetTestDataApplicationSettings();
        }

        [Conditional("Debug")]
        private void SetDebugApplicationSettings()
        {
            // Set the two Settings values to use the resource files designated
            // for the DEBUG version of the app:
            this["CompileConfiguration"] = "Debug";
           
        }

        [Conditional("TestData")]
        private void SetTestDataApplicationSettings()
        {
            this["CompileConfiguration"] = "TestData";
        }

        [Conditional("Test")]
        private void SetTestApplicationSettings()
        {
            this["CompileConfiguration"] = "Test";
        }

        [Conditional("Developpement")]
        private void SetDeveloppementApplicationSettings()
        {
            this["CompileConfiguration"] = "Developpement";
        }

        [Conditional("Release")]
        private void SetReleaseApplicationSettings()
        {
            this["CompileConfiguration"] = "Release";
        }
 
    }
}