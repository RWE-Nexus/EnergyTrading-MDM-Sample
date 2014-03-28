﻿namespace MDM.Loader
{
    using System;
    using System.Linq;

    using EnergyTrading.Console;

    public class MDMLoaderCommandLineArgs
    {
        private CommandLineParser commandLineParser;

        [CommandLineSwitch("cd", "Candidate data")]
        public bool CandidateData { get; set; }

        [CommandLineSwitch("entity", "Set the entity")]
        public string EntityName { get; set; }

        [CommandLineSwitch("file", "Set the filePath")]
        public string FilePath { get; set; }

        public bool HasUnhandledParameters
        {
            get
            {
                return this.commandLineParser.Parameters != null && this.commandLineParser.Parameters.Any();
            }
        }

        [CommandLineSwitch("y", "Silent user confirmation")]
        public bool HasUserConfirmed { get; set; }

        [CommandLineSwitch("ui", "Show MDM loader UI")]
        public bool IsInUiMode { get; set; }

        [CommandLineSwitch("mdm", "MDM Service Uri")]
        public string MdmServiceUri { get; set; }

        [CommandLineSwitch("help", "Show help")]
        public bool ShowHelp { get; set; }

        public void Initialize()
        {
            this.commandLineParser = new CommandLineParser(Environment.CommandLine, this);
            this.commandLineParser.Parse();
        }
    }
}