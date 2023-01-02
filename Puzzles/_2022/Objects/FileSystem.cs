﻿using APIAdventOfCode.Puzzles._2022.Objects.Enums;

namespace APIAdventOfCode.Puzzles._2022.Objects
{
    public class FileSystem
    {
        private readonly int _directorySizeCutoff = 100000;
        private Directory _baseDirectory;
        private Directory? _currentFocus;

        public FileSystem(IEnumerable<string> consoleInput) {
            _baseDirectory = new Directory("", null);
            _currentFocus = _baseDirectory;

            foreach (string line in consoleInput)
            {
                if (line.StartsWith('$'))
                {
                    HandleCommand(line);
                } else
                {
                    HandleContent(line);
                }
            }
        }

        public int GetTotalDirectorySizeUnderCutoff()
        {
            var totalSize = 0;
            RecursiveCheckTotalSizeUnderCutoff(_baseDirectory, ref totalSize);

            return totalSize;
        }

        private void RecursiveCheckTotalSizeUnderCutoff(Directory dir, ref int totalSize)
        {
            int currentDirSize = dir.FindDirectorySize();
            if (currentDirSize <= _directorySizeCutoff)
            {
                totalSize += currentDirSize;
            }

            foreach (Directory childDir in dir.ChildDirectories)
            {
                RecursiveCheckTotalSizeUnderCutoff(childDir, ref totalSize);
            }
        }

        private void HandleContent(string line)
        {
            IEnumerable<string> lineParts = line.Split(' ');
            if (lineParts.First() == "dir")
            {
                CreateNewFolder(lineParts.Last());
            } else
            {
                CreateNewFile(lineParts.First(), lineParts.Last());
            }
        }

        private void CreateNewFolder(string name)
        {
            Directory newDirectory = new Directory(name, _currentFocus);
            _currentFocus.AddDirectory(newDirectory);
        }

        private void CreateNewFile(string size, string name)
        {
            int intSize;
            Int32.TryParse(size, out intSize);
            File newFile = new File(name, intSize);
            _currentFocus.AddFile(newFile);
        }

        private void HandleCommand(string line)
        {
            Command command = new Command(line);

            switch (command.Type) {
                case CommandTypeEnum.CD:
                    HandleCD(command); 
                    break;
                case CommandTypeEnum.LS:
                    HandleLS(command);
                    break;
            }
        }

        private void HandleCD(Command cdCommand)
        {
            switch (cdCommand.Direction)
            {
                case CommandDirectionEnum.Base:
                    break;
                case CommandDirectionEnum.Deeper:
                    HandleCDDeeper(cdCommand);
                    break;
                case CommandDirectionEnum.Shallower:
                    HandleCDShallower(cdCommand);
                    break;
            }
        }

        private void HandleCDDeeper(Command cdCommand)
        {
            IEnumerable<Directory> newFocus = _currentFocus.ChildDirectories
                .Where(x => x.Name == cdCommand.DirectoryName);

            if (newFocus.Count() != 1)
            {
                throw new Exception(string.Format("Expected {0} to contain 1 directory with path {1}; found {2}",
                    _currentFocus.Name,
                    cdCommand.DirectoryName,
                    newFocus.Count()));
            }

            _currentFocus = newFocus.Single();
        }

        private void HandleCDShallower(Command cdCommand)
        {
            if (_currentFocus.ParentDirectory == null)
            {
                throw new Exception(string.Format("Expected {0} to have a parent", 
                    _currentFocus.Name));
            }

            _currentFocus = _currentFocus.ParentDirectory;
        }

        private void HandleLS(Command lsCommand)
        {
            // We don't actually need to do anything if the command is LS
            return;
        }
    }

    public class Command
    {
        public CommandTypeEnum Type { get; }
        public CommandDirectionEnum? Direction { get; }
        public string? DirectoryName { get; }

        public Command(string line)
        {
            var lineParts = line.Split(' ');
            Type = GetType(lineParts[1]);

            if (Type == CommandTypeEnum.CD)
            {
                Direction = GetDirection(lineParts[2]);

                if (Direction == CommandDirectionEnum.Deeper)
                {
                    DirectoryName = lineParts[2];
                }
            }
        }

        private CommandTypeEnum GetType(string commandPart)
        {
            switch (commandPart) {
                case "cd":
                    return CommandTypeEnum.CD;
                case "ls":
                    return CommandTypeEnum.LS;
                default:
                    throw new Exception("Unknown command: " + commandPart);
            }
        }

        private CommandDirectionEnum GetDirection(string directionPart)
        {
            switch (directionPart)
            {
                case "/":
                    return CommandDirectionEnum.Base;
                case "..":
                    return CommandDirectionEnum.Shallower;
                default:
                    return CommandDirectionEnum.Deeper;
            }
        }
    }
}
