﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variables;

namespace Interpreter
{
    public class FileInterpreter
    {
        public string FileName { get; set; }
        public List<string> Lines = new List<string>();
        public List<Methods> Methods = new List<Methods>();
        private readonly Interpreter _interpreter = new Interpreter();
        private bool _stop;
        private readonly List<int> _nextBreak = new List<int>();
        private bool _breakMode;

        public FileInterpreter(string fileName)
        {
            fileName = fileName.Replace("'", "");
            FileName = fileName;

            int counter = 0;
            string line;

            var file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                Lines.Add(line);
                counter++;
            }

            file.Close();
        }

        public void LoadAll()
        {
            LoadFunctions();
            ExecuteFromLineToLine(new Tuple<int, int>(-1, Lines.Count),true);
            if (Cache.Instance.EraseVars)
            {
                foreach (var variable in Cache.Instance.Variables.ToList())
                {
                    if (variable.Key.Item2 == FileName || Cache.Instance.LoadFiles.Contains(variable.Key.Item2))
                    {
                        Cache.Instance.Variables.Remove(variable.Key);
                    }
                }
            }        
        }

        private void ExecuteFromLineToLine(Tuple<int, int> fromTo,bool firstLevel)
        {
            for (int i = fromTo.Item1 + 1; i <= fromTo.Item2; i++)
            {
                string operation;

                if (i > Lines.Count - 1)
                {
                   break; 
                }

                if (Lines[i] == "break")
                {
                    if (!firstLevel)
                    {
                        _breakMode = true;
                        return;
                    }
                    return;
                }

                if (Lines[i].StartsWith("func"))
                {
                    //TODO input parameters
                    var func = GetLineToLine(i, "func");
                    i = func.Item2;
                    continue;
                }

                try
                {
                    var temp = Lines[i];
                }
                catch (Exception)
                {
                    return;
                }

                if (Lines[i] == "stopExec")
                {
                    _stop = true;
                    return;
                }  

                if (_stop)
                {
                    return;
                }

                var result = _interpreter.InterpretLine(Lines[i], FileName, out operation);

                if (_interpreter.Clear)
                {
                    _interpreter.Clear = false;
                    Console.Clear();
                }

                if (_interpreter.Evaluator.EvaluateOperation(operation) == OperationTypes.CASE)
                {
                    var LineToLine = GetLineToLine(i, "case");

                    if (bool.Parse(result.Key))
                    {
                        ExecuteFromLineToLine(LineToLine,false);
                        i = LineToLine.Item2;
                    }
                    else
                    {
                        i = LineToLine.Item2;
                    }
                }

                if (_interpreter.Evaluator.EvaluateOperation(operation) == OperationTypes.RUNALA)
                {
                    var lineToLine = GetLineToLine(i, "runala");
                    _nextBreak.Add(lineToLine.Item2);

                    while (bool.Parse(result.Key))
                    {
                        ExecuteFromLineToLine(lineToLine,false);
                        result = _interpreter.InterpretLine(Lines[i], FileName, out operation);

                        if (_stop)
                        {
                            return;
                        }

                        if (_breakMode && !firstLevel)
                        {
                            return;
                        }
                        if (_breakMode && firstLevel)
                        {
                            i = _nextBreak.Last();
                            break;
                        }
                    }
                    i = lineToLine.Item2 + 1;
                }


                if (result.Value && result.Key != string.Empty)
                {
                    Console.WriteLine(result.Key);
                }
            }
        }

        private Tuple<int, int> GetLineToLine(int variable, string lookOut)
        {
            var currentLine = variable;
            var endCase = 0;
            var buffer = 0;

            for (var i = currentLine + 1; i < Lines.Count; i++)
            {
                if (Lines[i].Replace(" ", "").Contains($"{lookOut}[") && Lines[i].Contains("]") && Lines[i] != $"end{lookOut}")
                {
                    buffer++;
                }
                if (Lines[i].Replace(" ", "") == $"end{lookOut}")
                {
                    if (buffer != 0)
                    {
                        buffer--;
                    }
                    else
                    {
                        endCase = i;
                        goto ret;
                    }
                }
            }

            ret:
            return new Tuple<int, int>(currentLine, endCase);
        }

        public void LoadFunctions()
        {
            for (var i = 0; i < Lines.Count; i++)
            {
                if (!Lines[i].StartsWith("func")) continue;
                var func = GetLineToLine(i,"func");
                Methods.Add(new Methods(Lines[i].Split(' ')[1],func));
                i = func.Item2;
            }
        }

        public void LoadReachableVars()
        {
            //throw new NotImplementedException();
        }
    }
}
